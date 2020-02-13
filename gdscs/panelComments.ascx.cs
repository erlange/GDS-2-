using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace gds
{
    public partial class panelComments : System.Web.UI.UserControl
    {

        private int intPanelId;
        private Boolean bEn;
        public int PanelId
        {
            get { return intPanelId; }
            set { intPanelId = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdminRole();
            if (commonModule.IsEnglish())
                GetPanelContent(intPanelId, Language.English);
            else
                GetPanelContent(intPanelId, Language.Indonesian);
            
            ShowData();
        }
        public void ShowData()
        {
            var gdsPanel = new gdsCmsPanel();
            this.DataList1.DataSource = gdsPanel.GetMaxComments(int.Parse(ConfigurationManager.AppSettings["panelCommentMaxRecords"]));
            this.DataList1.DataBind();
        }
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDate = (Label)e.Item.FindControl("lblDate");
                Label lblFrom = (Label)e.Item.FindControl("lblFrom");
                Label lblComment = (Label)e.Item.FindControl("lblComment");
                HyperLink lblEmail = (HyperLink)e.Item.FindControl("lblEmail");
                // Dim lblUrl As HyperLink = e.Item.FindControl("lblUrl")
                Label lblBR1 = (Label)e.Item.FindControl("lblBR1");
                // Dim lblBR2 As Label = e.Item.FindControl("lblBR2")

                if (commonModule.IsEnglish())
                {
                    lblFrom.Text = "From:";
                }
                else
                {
                    lblFrom.Text = "Dari:";
                }

                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (Convert.IsDBNull(drv["email"]))
                    lblEmail.NavigateUrl = "";

                if (Convert.IsDBNull(drv["url"]))
                    lblBR1.Visible = false;
            }

        }

        public void CheckAdminRole()
        {
            if (commonModule.IsInAdminsRole())
            {
                this.btnEdit.Visible = true;
                this.btnEditRecords.Visible = true;
                this.btnEdit.NavigateUrl = "AdminEditComment.aspx?id=" + intPanelId;
            }
            else
            {
                this.btnEdit.Visible = false;
                this.btnEditRecords.Visible = false;
                this.btnEdit.NavigateUrl = "AdminEditCommentData.aspx?id=" + intPanelId;
            }
        }

        public void GetPanelContent(int panelId, Language language)
        {
            var gdsPanel = new gdsCmsPanel(panelId, language);
            lblTitle.Text = gdsPanel.Title;
            lblContent.Text = gdsPanel.Content;
        }

    }
}