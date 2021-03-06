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
    public partial class WebForm1 : System.Web.UI.Page
    {
        PostManager _pmgr = new PostManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            string cboards = this.Request.QueryString["Cboard"];
            int cboard;

            if (int.TryParse(cboards, out cboard))
                this.lblMsg.Text = "查無此子版";
            else
            {
                cboard = 2;
                // 先測試 假設有cboardid
                this.DisplayPost(cboard);
            }
               
        }

        protected void btnPostEdit_Click(object sender, EventArgs e)
        {

        }
        private void DisplayPost(int cboard)
        {
            List<Post> postList = this._pmgr.GetPostList(cboard);
            this.rptcBtoP.DataSource = postList;
            this.rptcBtoP.DataBind();
        }
    }
}