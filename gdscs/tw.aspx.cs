﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class ScatterPage : System.Web.UI.Page
    {
        protected int iDs;
        protected bool bEn;

        protected panelGenericTitle pTitleTw;

        protected void Page_Load(object sender, EventArgs e)
        {
            mnuTop MnuTop1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");
            //panelGenericTitle pTitleTw = (panelGenericTitle)

            MnuBottom1.SetSelectedIndex(2);
            MnuTop1.SetSelectedIndex(2);

            GetRequest();
            pTitleTw.PanelId = 9;

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
            if (Request.Params["c"] == "2")
                Response.Redirect(string.Format("sc.aspx?ds={0}", iDs));
        }

    }
}