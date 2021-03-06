﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class ScoreVarPage : System.Web.UI.Page
    {
        protected int iDs;
        protected bool bEn;
        protected bool isSingleVar;

        protected void Page_Load(object sender, EventArgs e)
        {
            mnuTop MnuTop1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");

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

            int c = Convert.ToInt32(Request.Params["c"] == "1" ? 1 : 0);
            int l = Convert.ToInt32(bEn ? 1 : 0);
            string submitString;
            string actionString;
            string chString;
            if (isSingleVar)
            {
                submitString = string.Format("return dist3('frmV',{0});", l);
                actionString = "pvOut.aspx";
            }
            else
            {
                submitString = string.Format("return mz('frmV',{0},{1})", c, l);
                actionString = "tw.aspx";
            }

            if (bEn)
                chString = "Show chart";
            else
                chString = "Tampilkan dengan chart";

        }
    }
}