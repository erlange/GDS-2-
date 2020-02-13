using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelGenericText : System.Web.UI.UserControl
    {
        private int intPanelId ;
        public int PanelId
        {
            get { return intPanelId; }
            set { intPanelId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdminRole();   
            gdsCmsPanel gdsCms = new gdsCmsPanel();
            gdsCms.GetGenericContentById(intPanelId, commonModule.IsEnglish());
            lblGenericText.Text = gdsCms.Content;
        }

        void CheckAdminRole()
        {
            if (commonModule.IsInAdminsRole())
            {
                btnEdit.Visible = true;
                btnEdit.NavigateUrl = "AdminEditGenericText.aspx?id=" + intPanelId.ToString();
                lblBr.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                lblBr.Visible = false;
            }

        }
    }
}