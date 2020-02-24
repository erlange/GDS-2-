using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
namespace gds
{
    public partial class panelPpt : System.Web.UI.UserControl
    {
        Boolean bEn = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdminRole();
            bEn = commonModule.IsEnglish();
            ShowData();
        }

            public void ShowData()
            {
                gdsDocuments doc = new gdsDocuments();
                DataTable dt;
                DataView dv;
                int maxRecordsToShow;
                maxRecordsToShow = Convert.ToInt32(ConfigurationManager.AppSettings["panelPPTMaxRecords"].ToString());
                dt = doc.GetMaxDocuments(maxRecordsToShow, 10, true);
                dv = dt.DefaultView;
                dv.Sort = "submitDate DESC";
                DataList1.DataSource = dv;
                DataList1.DataBind();
                lblTitle.Text = bEn ? "Papers and Presentations" : "Paper dan Presentasi";
                lblGoToDoc.Text = bEn ? "Papers and Presentations..." : "Paper dan Presentasi...";
                
            }
            public void CheckAdminRole()
            {
                btnEditRecords.Visible = commonModule.IsInAdminsRole();
            }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDate = (Label)e.Item.FindControl("lblDate");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");
                HyperLink lblUrl = (HyperLink)e.Item.FindControl("lblUrl");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string filename = drv["url"].ToString().Split('/')[drv["url"].ToString().Split('/').Length - 1];

                if (bEn)
                {
                    lblUrl.Text = Convert.IsDBNull(drv["title_en"]) ? filename : drv["title_en"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc_en"]) ? "" : drv["desc_en"].ToString();
                }
                else
                {
                    lblUrl.Text = Convert.IsDBNull(drv["title"]) ? filename : drv["title"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc"]) ? "" : drv["desc"].ToString();
                }

                lblUrl.NavigateUrl = "g.aspx?f=" + drv["url"].ToString();
            }
        }
    }
}