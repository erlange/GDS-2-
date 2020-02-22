using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class contacts_old : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MnuTop1.SetSelectedIndex(4);
            MnuBottom1.SetSelectedIndex(4);
            PanelHtml1.PanelId = 5;
        }
    }
}