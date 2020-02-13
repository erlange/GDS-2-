using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class langBar1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["en"] == "1")
            {
                CheckAdminRole("Welcome, ");
                btnIna.Attributes["class"] = "langMnu";
                btnIna.HRef = "setLang.aspx";
                btnEn.Attributes["class"] = "langMnuDis";
                btnEn.HRef = "javascript:void(null);";
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            }
            else
            {
                CheckAdminRole("Selamat datang, ");
                btnIna.Attributes["class"] = "langMnuDis";
                btnIna.HRef = "javascript:void(null);";
                btnEn.Attributes["class"] = "langMnu";
                btnEn.HRef = "setLang.aspx?en=1";
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("id-ID");
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            }
        }

        public void CheckAdminRole(string greetingString)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                this.lblUser.Visible = true;
                this.lblUser.Text = greetingString + HttpContext.Current.User.Identity.Name;
                this.btnLogout.Visible = true;
            }
            else
            {
                this.lblUser.Visible = false;
                this.btnLogout.InnerHtml = "&nbsp;&nbsp;&nbsp;";
                this.btnLogout.HRef = "AdminLogin.aspx";
                this.btnLogout.Title = "Login";
            }
        }

    }
}