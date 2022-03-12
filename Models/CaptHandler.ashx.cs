using MKForum.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKForum.Models
{
    /// <summary>
    /// CaptHandler 的摘要描述
    /// </summary>
    public class CaptHandler : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {
        private AccountManager _mga = new AccountManager();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            MemberRegister memberRegister = new MemberRegister();


            memberRegister.Captcha = _mga.CreateValidateCode(4);



            context.Session["MemberRegister"] = memberRegister.Captcha;
            _mga.CreateValidateImg(memberRegister.Captcha, context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}