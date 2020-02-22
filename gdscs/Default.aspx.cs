using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //PanelComments1.PanelId = 2;

            mnuTop TopMenu1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");

            pTitleSurveyData.PanelId = 2;
            pTextSurveyData.PanelId = 2;

            //pTitleHighlight.PanelId = 3;

            pTitleWelcome.PanelId = 1;
            pTextWelcome.PanelId = 1;

            TopMenu1.SetSelectedIndex(0);
            MnuBottom1.SetSelectedIndex(0);

        }
    }
}