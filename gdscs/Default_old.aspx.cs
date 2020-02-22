using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class gds2_default_old : System.Web.UI.Page
    {
        bool bEn;

        protected panelComments PanelComments1;
        protected panelGenericTitle pTitleWelcome;
        protected panelGenericText pTextWelcome;
        protected panelGenericTitle pTitleSurveyData;
        protected panelGenericText pTextSurveyData;
        
        protected panelGenericTitle pTitleHighlight;
        protected mnuTop TopMenu1;
        protected mnuBottom MnuBottom1;

        protected void Page_Load(object sender, EventArgs e)
        {
            PanelComments1.PanelId = 2;

            pTitleSurveyData.PanelId = 2;
            pTextSurveyData.PanelId = 2;

            pTitleHighlight.PanelId = 3;

            pTitleWelcome.PanelId = 1;
            pTextWelcome.PanelId = 1;

            TopMenu1.SetSelectedIndex(0);
            MnuBottom1.SetSelectedIndex(0);
        }


    }
}