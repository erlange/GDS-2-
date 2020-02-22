using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelSearchHtml : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblSearch.Text = commonModule.IsEnglish() ? "Search keyword" : "Pencarian";
        }
    }
}