using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace gds
{
    public partial class panelCopyright : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ShowData();
        }

        void ShowData()
        {
            DataSet ds;
            ds = new DataSet();
            ds.ReadXml(Server.MapPath("panelCopyright.xml"));
            this.lblCopyright.Text = ds.Tables[0].Rows[0][0].ToString();
        }
    }
}