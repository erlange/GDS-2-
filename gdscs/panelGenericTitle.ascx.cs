using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelGenericTitle : System.Web.UI.UserControl
    {

        int intPanelId = 0;
        public int PanelId
        {
            get { return intPanelId; }
            set { intPanelId = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdminRole();
            gdsCmsPanel gdsCms = new gdsCmsPanel();
            if (gdsCms.GetGenericTitleById(intPanelId, commonModule.IsEnglish()) == 1)
                lblEdit.Text = "Edit Title";
            else
                lblEdit.Text = "Add Title";

            lblGenericTitle.Text = gdsCms.Content;
        }


        void CheckAdminRole()
        {
            if (commonModule.IsInAdminsRole())
            {
                btnEdit.Visible = true;
                btnEdit.NavigateUrl = "AdminEditGenericTitle.aspx?id=" + intPanelId.ToString();
            }
            else
                btnEdit.Visible = false;
        }
    }
}