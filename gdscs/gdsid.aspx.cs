using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class gdsid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MnuTop1.SetSelectedIndex(1);
            MnuBottom1.SetSelectedIndex(1);
            PanelHtml1.PanelId = 4;
        }
    }
}