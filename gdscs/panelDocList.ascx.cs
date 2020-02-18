using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace gds
{
    public partial class panelDocList : System.Web.UI.UserControl
    {
        protected bool bEn;
        private int _CategoryId;


        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdminRole();
            bEn = commonModule.IsEnglish();
            ShowData();

        }

        protected bool isDemoMode()
        {
           return Convert.ToBoolean( ConfigurationManager.AppSettings["demo"]);
        }

        public void ShowData()
        {
            var doc = new gdsDocuments();
            var cat = new gdsDocumentCategories();
            DataTable dt;
            DataView dv;
            int maxRecordsToShow;
            dt = commonModule.IsInAdminsRole() ? doc.GetDocumentsByCategoryId(_CategoryId) : doc.GetVisibleDocumentsByCategoryId(_CategoryId);
            

            dv = dt.DefaultView;
            dv.Sort = "submitDate DESC";
            this.DataList1.DataSource = dv;
            this.DataList1.DataBind();
            if (dt.Rows.Count > 0)
                this.lblNoData.Visible = false;
            else
                lblNoData.Text = bEn ? "Not available in off-line version." : "Tidak tersedia dalam versi off-line.";

            if (cat.GetCategoryById(_CategoryId) == 1)
                lblTitle.Text = bEn ? cat.DescEn : cat.Desc;

            btnEditRecords.NavigateUrl = "doc.aspx?id=" + _CategoryId;
        }


        public void CheckAdminRole()
        {
            btnEditRecords.Visible = commonModule.IsInAdminsRole();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDate = (Label)e.Item.FindControl("lblDate");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");
                Label lblDocTitle = (Label)e.Item.FindControl("lblDocTitle");
                Label lblSize = (Label)e.Item.FindControl("lblSize");
                Image Image1 = (Image)e.Item.FindControl("Image1");
                Label lblSubmitDate = (Label)e.Item.FindControl("lblSubmitDate");
                HyperLink lblUrl = (HyperLink)e.Item.FindControl("lblUrl");
                HyperLink lblFilename = (HyperLink)e.Item.FindControl("lblFilename");
                // Dim lblBR1 As Label = e.Item.FindControl("lblBR1")

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string filename;
                string extension;
                filename = drv["url"].ToString().Split('/')[drv["url"].ToString().Split('/').Length - 1];
                extension = filename.Split('.')[filename.Split('.').Length - 1];
                if (bEn)
                {
                    lblUrl.Text = Convert.IsDBNull(drv["title_en"]) ? filename : drv["title_en"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc_en"])? "- No description available -":drv["desc_en"].ToString();
                    lblSubmitDate.Text = "Submitted Date";
                    lblDocTitle.Text = "Title";
                }
                else
                {
                    lblUrl.Text = Convert.IsDBNull(drv["title"])? filename:drv["title"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc"])? "- Tanpa keterangan -": drv["desc"].ToString();
                    lblSubmitDate.Text = "Tanggal";
                    lblDocTitle.Text = "Judul";
                }

                lblUrl.NavigateUrl = drv["url"].ToString();
                lblFilename.Text = filename;
                lblFilename.NavigateUrl = drv["url"].ToString();
                lblSize.Text = commonModule.ConvertBytes(Convert.ToInt64( drv["size"]));
                if (System.IO.File.Exists(Server.MapPath("imgedit/" + extension + ".gif")))
                {
                    Image1.ImageUrl = "imgedit/" + extension + ".gif";
                }
                else
                {
                    Image1.ImageUrl = "imgedit/file.gif";
                }

                if (isDemoMode())
                {
                    lblFilename.NavigateUrl = "g.aspx?f=" + drv["url"].ToString();
                    lblUrl.NavigateUrl = "g.aspx?f=" + drv["url"].ToString();
                }

            }
        }
    }
}