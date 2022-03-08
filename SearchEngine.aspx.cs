using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKForum.Managers;


namespace MKForum
{
    public partial class SearchEngine : System.Web.UI.Page
    {

        //我忘了怎麼把頁首的搜尋列結果傳到主內容頁面裡面，所以先把功能做出來放在同一頁

        // 從Session取得當前子板塊ID，先暫訂是2
        int cboardid = 2;
        //int cboardid = this.Session["CboradID"] as int;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //使用者搜尋的關鍵字(srchText)
            string srchText = this.SearchBox.Text;

            List<SearchResult> srchList;

            //根據選取的搜尋範圍走不同的方法，取得搜尋結果的List
            if (this.srchAreaList.SelectedValue == "srchAllSite")
            {
                srchList = SearchManager.getAllSrchList(srchText, cboardid);
            }

            else if (this.srchAreaList.SelectedValue == "srchPosts")
            {
                srchList = SearchManager.getCboardSrchList(srchText, cboardid);
            }

            else
            {
                 srchList = SearchManager.getWriterSrchList(srchText, cboardid);
            }


            //如果沒有資料，顯示無資料
            if (srchList.Count != 0)
            {
                this.Repeater1.DataSource = srchList;
                this.Repeater1.DataBind();
            }
            else
            {
                this.Repeater1.Visible = false;
                this.plcEmpty.Visible = true;
            }
        }
    }
}