using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class GalleryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mnuTop MnuTop1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");

            MnuTop1.SetSelectedIndex(-1);
            MnuBottom1.SetSelectedIndex(-1);
            if (!IsPostBack)
                ShowData();
        }

        void ShowData()
        {
            DataTable dt;
            gdsDocuments gdsDoc = new gdsDocuments();
            dt = commonModule.IsInAdminsRole() ? gdsDoc.GetDocumentsByCategoryId(11) : gdsDoc.GetVisibleDocumentsByCategoryId(11);
            Album1.ImageTable = dt;
            Album1.RepeatColumns = Convert.ToInt32(ConfigurationManager.AppSettings["galleryRepeatColumns"]);

        }
    }
}