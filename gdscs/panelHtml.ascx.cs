using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelHtml : System.Web.UI.UserControl
    {
        private int intPanelId;
        public int PanelId
        {
            get { return intPanelId; }
            set { intPanelId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdminRole();
            if (commonModule.IsEnglish())
            {
                GetPanelContent(intPanelId, Language.English);
            }
            else
            {
                GetPanelContent(intPanelId, Language.Indonesian);
            }
        }

        void GetPanelContent(int panelId, Language language)
        {
            gdsCmsPanel gdsPanel = new gdsCmsPanel(panelId, language);
            lblTitle.Text = gdsPanel.Title;
            lblContent.Text = gdsPanel.Content;
        }
        void CheckAdminRole()
        {
            if (commonModule.IsInAdminsRole())
            {
                btnEdit.Visible = true;
                btnEdit.NavigateUrl = "AdminEditContent.aspx?id=" + intPanelId.ToString();
            }
            else
                btnEdit.Visible = false;

        }
    }
}