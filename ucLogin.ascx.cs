using MKForum.Managers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MKForum
{
    public partial class ucLogin : System.Web.UI.UserControl
    {
        private AccountManager _mga = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this._mga.IsRegister())
                {
                    this.plcInfo.Visible = true;
                    this.plcReg.Visible = false;
                    Member account = this._mga.GetCurrentUser();
                    this.ltlAccount.Text = account.Account + "成功登入";

                    this.Response.Redirect("HomePage.aspx", true);
                }
                else
                {
                    this.plcInfo.Visible = false;
                    this.plcReg.Visible = true;
                }
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string account = this.txtAcc.Text.Trim();
            string password = this.txtPwd.Text.Trim();
            if (this._mga.TryLogin(account, password))
            {
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                this.ltlMessage.Text = "登入失敗，請檢查帳號密碼。";
            }
        }
    }
}