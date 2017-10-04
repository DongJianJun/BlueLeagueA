using Common;
using Common.Attributes;
using Models;
using NETApi.Areas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETApi.Controllers
{
    public class AdminLoginController : BaseController
    {
        //初始化用户集合
        public static List<UserModel> allUserList = new List<UserModel> 
        {
            new UserModel{ ID=1,UserName="大师兄",Gender="boy"},
            new UserModel{ ID=2,UserName="二师兄",Gender="boy"},
            new UserModel{ ID=3,UserName="三师兄",Gender="boy"}
        };
        /// <summary>
        /// 用户实体类
        /// </summary>
        public class UserModel
        {
            /// <summary>
            /// 用户编号
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// 用户名称
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string Gender { get; set; }
        }
        //public ActionResult LoginIn(string p_loginName,string p_pwd) {
        //    ActionResult ar = null;
        //    Action onAction = () =>
        //    {
        //        if (p_loginName == "" || p_pwd == "")
        //        {
        //            ar = PackagingAjaxmsg(AjaxStatu.err, "登录名|密码|验证码没有传入后台！");
        //        }
        //        AjaxMsgModel amm = Model_Sys_UserInfo.LoginIn(p_loginName,p_pwd);
        //        ar = PackagingAjaxmsg(amm);

        //    };
            
        //    return ar;
        
        //}
        //只想见二师兄
        //url:/api/Users/2 GET
        public UserModel GetOne(int id)
        {
            return allUserList.Find(u => u.ID == id);
        }
    }
}
