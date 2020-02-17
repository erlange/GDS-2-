using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class tw : System.Web.UI.Page
    {
        protected int iDs;
        protected bool bEn;

        protected void Page_Load(object sender, EventArgs e)
        {
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