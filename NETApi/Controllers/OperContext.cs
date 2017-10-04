using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Attributes;
using System.Web.SessionState;
using System.Runtime.Remoting.Messaging;

namespace NETApi.Controllers
{
    public class OperContext
    {
        #region 名称常量
        /// <summary>
        /// 验证码的Session名称
        /// </summary>
        public const string Admin_ValidateCode = "ValidateCode";
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public const string Admin_User = "Admin_User";
        /// <summary>
        /// 保存当前用户信息的CookieID
        /// </summary>
        public const string Admin_Cookie_UserLoginName = "Cookie_Login_Name";
        /// <summary>
        /// cookie所存储的位置
        /// </summary>
        public const string Admin_Cookie_Path = "/admin/";
        /// <summary>
        /// 保存当前用户权限信息
        /// </summary>
        public const string Admin_PermissionKey = "APermission";
        #endregion

        #region 使用线程优化业务层仓储
        public IBLL.IBLLSession BllSession;
        private OperContext()
        {
            BllSession = DI.SpringHelper.GetObject<IBLL.IBLLSession>("BLLSession");
        }
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static OperContext CurrentContext
        {
            get
            {
                OperContext opContext = CallContext.GetData(typeof(OperContext).Name) as OperContext;
                if (opContext == null)
                {
                    opContext = new OperContext();
                    CallContext.SetData(typeof(OperContext).Name, opContext);

                }
                return opContext;
            }
        }
        #endregion

        #region 封装HTTP对象
        HttpContext ContextHttp
        {
            get
            {
                return HttpContext.Current;
            }
        }
        public HttpRequest Request
        {
            get
            {
                return ContextHttp.Request;
            }
        }
        public HttpResponse Response
        {
            get
            {
                return ContextHttp.Response;
            }
        }
        /**
         * 参考示例
         *  return Session[Admin_PermissionKey] as List<Sys_Permission>;
         * **/
        /// <summary>
        /// 获得一个Session对象
        /// </summary>
        public HttpSessionState Session
        {
            get
            {
                return ContextHttp.Session;
            }
        }

        #endregion

        #region 当前用户
        /**
         *  使用示例 
         *  ViewBag.uLoginName = oc.CurrentUser.uLoginName;
         * **/
        /// <summary>
        /// 当前用户
        /// </summary>
        public Sys_UserInfo CurrentUserInfo
        {
            get
            {
                return Session[Admin_User] as Sys_UserInfo;
            }
            set
            {
                Session[Admin_User] = value;
            }
        }
        #endregion

        #region 获得当前用户验证码
        /// <summary>
        /// 获得当前用户验证码
        /// </summary>
        public string CurrentUserVcode
        {
            get { return Session[Admin_ValidateCode] as string; }
            set { Session[Admin_ValidateCode] = value; }

        }
        #endregion

        #region 当前用户权限
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<Sys_Modulel> UserModels
        {
            get {
                return Session[Admin_PermissionKey] as List<Sys_Modulel>;
            }
            set
            {
                Session[Admin_PermissionKey] = value;
            }

        }
        #endregion

        #region 保存当前登录用户的用户名
        HttpCookie cookie;
        /// <summary>
        /// 保存当前登录用户的用户名
        /// </summary>
        public string CurrentLoginName
        {
            set
            {
                value = Common.SecurityHelper.Encrypt(value.ToString());
                cookie = new HttpCookie(Admin_Cookie_UserLoginName, value);
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Path = Admin_Cookie_Path;
                Response.Cookies.Add(cookie);
            }
            get
            {
                if (Request.Cookies[Admin_Cookie_UserLoginName] == null)
                {
                    return "";
                }
                else
                {
                    string strLoginName = Request.Cookies[Admin_Cookie_UserLoginName].Value;
                    strLoginName = Common.SecurityHelper.DeEncrypt(strLoginName);
                    return strLoginName;
                }
            }

        }
        #endregion
    }
}