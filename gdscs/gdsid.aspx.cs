using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class LocalGdsDescPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mnuTop MnuTop1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");
            MnuTop1.SetSelectedIndex(1);
            MnuBottom1.SetSelectedIndex(1);
            PanelHtml1.PanelId = 4;
        }
    }
}