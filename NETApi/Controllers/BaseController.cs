using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NETApi.Controllers
{
    public class BaseController : ApiController
    {
        protected OperContext oc = OperContext.CurrentContext;
        #region 把ajax返回值封装成json格式返回
        /**
          参考：
       * PackagingAjaxmsg(AjaxStatu.err, "登录名|密码|验证码没有传入后台！");
       */
        /// <summary>
        /// 把ajax返回值封装成json格式返回
        /// </summary>
        /// <param name="statu">Ajax 状态</param>
        /// <param name="msg">Ajax 信息</param>
        /// <param name="data">Ajax 数据</param>
        /// <param name="backurl">调用后的链接</param>
        /// <returns>json格式的Ajax数据</returns>
        public ActionResult PackagingAjaxmsg(AjaxStatu statu, string msg,
            object data = null, string backurl = "")
        {
            AjaxMsgModel amm = new AjaxMsgModel
            {
                BackUrl = backurl,
                Data = data,
                Msg = msg,
                Statu = statu

            };
            JsonResult ajaxRes = new JsonResult();
            ajaxRes.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            ajaxRes.Data = amm;
            return ajaxRes;

        }
        #endregion

        #region 把ajax返回值封装成json格式返回
        /**
         * 参考
           AjaxMsgModel amm = Model_Sys_Department.AddDept(dept);
            return PackagingAjaxmsg(amm);
         */
        /// <summary>
        /// 把ajax返回值封装成json格式返回
        /// </summary>
        /// <param name="statu">Ajax 状态</param>
        /// <param name="msg">Ajax 信息</param>
        /// <param name="data">Ajax 数据</param>
        /// <param name="backurl">调用后的链接</param>
        /// <returns>json格式的Ajax数据</returns>
        public ActionResult PackagingAjaxmsg(AjaxMsgModel amm)
        {

            JsonResult ajaxRes = new JsonResult();
            ajaxRes.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            ajaxRes.Data = amm;
            return ajaxRes;


        }
        #endregion

        #region 重定向方法(ajax和link)
        /// <summary>
        /// 重定向方法(ajax和link)
        /// </summary>
        /// <param name="url">重定向的url</param>
        /// <param name="action">产生重定向的action方法</param>
        /// <returns>JSONRESULT REDIRECTRESULT</returns>
        public ActionResult Redirect(string url, ActionDescriptor action,
                                     AjaxStatu ajaxStatu = AjaxStatu.noperm)
        {
            if (action.IsDefined(typeof(Common.Attributes.AjaxRequestAttribute), false)
             || action.ControllerDescriptor.IsDefined(typeof(Common.Attributes.AjaxRequestAttribute), false))
            {
                if (ajaxStatu == AjaxStatu.nologin)
                {
                    return PackagingAjaxmsg(AjaxStatu.nologin, "您还没有登录或登录已超时，请重新登录！", null, url);
                }
                else
                {
                    string strAction = action.GetDescription();
                    string strController = action.ControllerDescriptor.GetDescription();
                    string msg = string.Format("您没有<br/>【{0}】<br/>的权限！请联系管理员！", strAction);
                    return PackagingAjaxmsg(AjaxStatu.noperm, msg, null, null);
                }
            }
            else
            {
                return new RedirectResult(url);
            }
            //return PackagingAjaxmsg(AjaxStatu.noperm, msg, null, null);
        }
        #endregion

        #region 向Action加异常处理
        /// <summary>
        /// 验证控制器的Action方法
        /// </summary>
        /// <param name="controller">控件器</param>
        /// <param name="onAction">Action方法</param>
        protected void SaveToAction(Controller controller, Action onAction)
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    onAction.Invoke();
                    controller.ViewBag.SuccessMsg = "操作成功！";
                }
                catch (Exception ex)
                {

                    controller.ViewBag.ErrorMsg = "操作失败！" + ex.Message;
                }
            }
            else
            {
                controller.ViewBag.ErrorMsg = "数据验证失败！";
            }
        }
        #endregion

    }
}
