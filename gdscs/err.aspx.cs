using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace gds
{
    public partial class err : System.Web.UI.Page
    {
        protected bool bEn;
        protected void Page_Load(object sender, EventArgs e)
        {
            bEn = commonModule.IsEnglish();
            MnuTop1.SetSelectedIndex(-1);
            MnuBottom1.SetSelectedIndex(-1);
            string filename;
            try
            {
                filename = Server.MapPath("err.txt");
                if (Session["errMsg"] != null)
                {
                    var sw = new StreamWriter(new FileStream(filename, FileMode.Append, FileAccess.Write));
                    sw.WriteLine(Session["errMsg"].ToString());
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}