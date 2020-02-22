using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class SwitchLangPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer.ToString() != "")
            {
                if (Request.Params["en"] == "1")
                     Session["en"] = 1;
                else
                    Session["en"] = 0;
                Response.Redirect(Request.UrlReferrer.ToString(), true);
            }
            else
                Response.Redirect("default.aspx", true);
        }
    }
}