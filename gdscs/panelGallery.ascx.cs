using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;

namespace gds
{
    public partial class panelGallery : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pTitleGallery.PanelId = 4;
            if (!IsPostBack)
                ShowData();
        }
        public void ShowData()
        {
            var gdsDoc = new gdsDocuments();
            int maxRecords;
            // Dim dt As DataTable = gdsDoc.GetTop3VisibleDocumentsByCategoryId(11) 'image. Lihat tbl Categories
            maxRecords = int.Parse(ConfigurationManager.AppSettings["panelGalleryMaxRecords"]);
            DataTable dt = gdsDoc.GetMaxDocuments(maxRecords, 11, true);  // image. Lihat tbl Categories
            Album1.ImageTable = dt;

            this.lblFooter.Text = commonModule.IsEnglish() ? "See Gallery..." : "Lihat Gallery..";
            
        }

    }
}