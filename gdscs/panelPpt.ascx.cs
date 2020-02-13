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
                if (bEn)
                {
                    this.lblTitle.Text = "Papers and Presentations";
                    this.lblGoToDoc.Text = "Papers and Presentations...";
                }
                else
                {
                    this.lblTitle.Text = "Paper dan Presentasi";
                    this.lblGoToDoc.Text = "Paper dan Presentasi...";
                }
            }
            public void CheckAdminRole()
            {
                if (commonModule.IsInAdminsRole())
                    this.btnEditRecords.Visible = true;
                else
                    this.btnEditRecords.Visible = false;
            }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }
    }
}