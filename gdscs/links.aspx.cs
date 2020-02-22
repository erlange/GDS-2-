using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class LinksPage : System.Web.UI.Page
    {
        bool bEn;
        protected void Page_Load(object sender, EventArgs e)
        {
            mnuTop MnuTop1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");
            MnuTop1.SetSelectedIndex(5);
            MnuBottom1.SetSelectedIndex(5);
            CheckIsAdmins();
            bEn = commonModule.IsEnglish();
            ShowData();
        }

        void ShowData()
        {
            gdsLinks l = new gdsLinks();
            DataTable dt = l.GetVisibleLinks();
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }

        void CheckIsAdmins()
        {
            btnEditRecords.Visible = commonModule.IsInAdminsRole();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDate = (Label)e.Item.FindControl("lblDate");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");
                Label lblSubmitDate = (Label)e.Item.FindControl("lblSubmitDate");
                HyperLink lblUrl = (HyperLink)e.Item.FindControl("lblUrl");
                HyperLink lblTitle = (HyperLink)e.Item.FindControl("lblTitle");
                DataRowView drv = (DataRowView)e.Item.DataItem;
                lblUrl.Text = Convert.IsDBNull(drv["url"]) ? "" : drv["url"].ToString();
                if (bEn)
                {
                    lblTitle.Text = Convert.IsDBNull(drv["title_en"]) ? "" : drv["title_en"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc_en"]) ? "- No description available -" : drv["desc_en"].ToString();
                    lblSubmitDate.Text = "Submitted Date";
                }
                else
                {
                    lblTitle.Text = Convert.IsDBNull(drv["title"]) ? "" : drv["title"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc"]) ? "- Tanpa Keterangan -" : drv["desc_en"].ToString();
                    lblSubmitDate.Text = "Submitted Date";
                }
            }
        }
    }
}