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
            btnEdit.Visible = commonModule.IsInAdminsRole();
            lblBr.Visible = commonModule.IsInAdminsRole();
            if (commonModule.IsInAdminsRole())
                btnEdit.NavigateUrl = "AdminEditGenericText.aspx?id=" + intPanelId.ToString();

        }
    }
}