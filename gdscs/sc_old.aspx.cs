using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class sc_old : System.Web.UI.Page
    {
        protected int iDs;
        protected bool bEn;
        protected bool isSingleVar;

        protected void Page_Load(object sender, EventArgs e)
        {
            MnuBottom1.SetSelectedIndex(2);
            MnuTop1.SetSelectedIndex(2);
            GetRequest();
            SetUI();
        }

        public void GetRequest()
        {
            if (Request.Params["ds"] != null)
            {
                if (Request.Params["ds"] != "")
                    iDs = Convert.ToInt32(Request.Params["ds"]);
            }
            else
                iDs = 11;

            bEn = commonModule.IsEnglish();
        }

        public void SetUI()
        {
            // If bEn Then
            // Me.btnNext.Value = commonModule.NEXTSTRINGEN
            // Me.btnPrev.Value = commonModule.PREVSTRINGEN
            // Else
            // Me.btnNext.Value = commonModule.NEXTSTRING
            // Me.btnPrev.Value = commonModule.PREVSTRING
            // End If
            pTitleSc.PanelId = 10;
            if (Request.Params["c"] != null)
            {
                if (char.IsNumber(Request.Params["c"], 0))
                {
                    if (Convert.ToInt32(Request.Params["c"]) == 1)
                        isSingleVar = false;
                    else
                        isSingleVar = true;
                }
                else
                    isSingleVar = true;
            }
            else
                isSingleVar = true;
        }
    }
}