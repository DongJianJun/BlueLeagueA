using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Common;
namespace NETApi.Areas.Models
{
    public partial class Model_Sys_UserInfo
    {
        public static AjaxMsgModel LoginIn(string strLoginName
            , string strLoginPwd) 
        {

                var users = oc.BllSession.ISys_UserInfoBLL.Entities.Where(u => u.UserName == strLoginName).Select(u => new
                    {
                        u.UserID,
                        u.UserName,
                        u.password,
                        u.EmployeeID

                    }).ToList();

                if (users.Count>0)
                {
                    var cUser = users.First();
                    if (cUser!=null)
                    {
                        oc.CurrentUserInfo = new Sys_UserInfo
                        {
                            UserID = cUser.UserID,
                            UserName = cUser.UserName,
                            password = cUser.password
                        };
                        return new AjaxMsgModel
                        {
                            Statu=AjaxStatu.ok,
                            Msg="登录成功！",
                            Data=null
                        };
                    }
                    else
                    {
                        return new AjaxMsgModel
                        {
                            Statu = AjaxStatu.err,
                            Msg = "密码不正确!",
                            Data = null


                        };
                    }

                }
                else
                {
                    return new AjaxMsgModel
                    {
                        Statu = AjaxStatu.err,
                        Msg = "用户名不存在！",
                        Data = null
                    };
                }
               
        
        }

    }
}