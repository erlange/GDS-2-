using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.RegisterHiddenField("__EVENTTARGET", btnQ.ClientID);
            btnQ.Attributes.Add("onclick", "if(document.getElementById('" + txtQ.ClientID + "').value.length==0){return false}");

            lblSearch.Text = commonModule.IsEnglish() ? "Search keyword" : "Pencarian";
        }

        protected void btnQ_Click(object sender, ImageClickEventArgs e)
        {
            if (txtQ.Text.Length > 0)
                Response.Redirect("q.aspx?q=" + txtQ.Text, true);
        }
    }
}