using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class PptPage : System.Web.UI.Page
    {
        bool bEn;
        protected void Page_Load(object sender, EventArgs e)
        {
            bEn = commonModule.IsEnglish();

            mnuTop MnuTop1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");

            MnuTop1.SetSelectedIndex(3);
            MnuBottom1.SetSelectedIndex(3);

            if (!IsPostBack)
            {
                ViewState["sortExp"] = "";
                ViewState["filterExp"] = "";
                ShowData(ViewState["filterExp"].ToString(), ViewState["sortExp"].ToString());

            }
        }
        

        public void ShowData(string filterExp, string sortExp)
        {
            gdsDocuments gdsDoc = new gdsDocuments();
            gdsDocumentCategories gdsCat = new gdsDocumentCategories();
            int categoryid;
            DataTable dt;
            if (!string.IsNullOrEmpty(Request.Params["id"]))
            {
                if (char.IsNumber(Request.Params["id"], 0))
                {
                    categoryid = int.Parse(Request.Params["id"]);
                    gdsCat.GetCategoryById(categoryid);
                    dt = gdsDoc.GetDocumentsByCategoryId(categoryid);
                    lblTitle.Text = bEn ? gdsCat.DescEn : gdsCat.Desc;
                }
                else
                {
                    dt = gdsDoc.GetDocuments();
                    lblTitle.Text = bEn ? "All Categories" : "Semua Kategori";
                }
            }
            else
            {
                dt = gdsDoc.GetDocuments();
                lblTitle.Text = bEn ? "All Categories" : "Semua Kategori";
            }

            dt.DefaultView.Sort = sortExp;
            DataGrid1.DataSource = dt.DefaultView;
            DataGrid1.DataBind();

            btnAdd.Visible = bEn;
            DataGrid1.Columns[0].Visible = bEn;
            DataGrid1.Columns[1].Visible = bEn;

        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnDelete;
                Label lblNo;
                HyperLink lblUrl;
                Label lblDesc;
                HyperLink lblDocTitle;
                Label lblSize;
                Label lblType;
                Image imgType;
                string extension;
                string url;
                string filename;
                Image imgVisible;
                DataRowView drv;
                drv = (DataRowView)e.Item.DataItem;
                btnDelete = (LinkButton)e.Item.Cells[1].FindControl("btnDelete");
                lblNo = (Label)e.Item.Cells[3].FindControl("lblNo");
                lblUrl = (HyperLink)e.Item.Cells[5].FindControl("lblUrl");
                lblDocTitle = (HyperLink)e.Item.Cells[4].FindControl("lblDocTitle");
                lblDesc = (Label)e.Item.Cells[6].FindControl("lblDesc");
                lblSize = (Label)e.Item.Cells[7].FindControl("lblSize");
                lblType = (Label)e.Item.Cells[8].FindControl("lblType");
                imgType = (Image)e.Item.Cells[8].FindControl("imgType");
                imgVisible = (Image)e.Item.Cells[9].FindControl("imgVisible");
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this?')");
                url = drv["url"].ToString();
                filename = url.Split('/', '\\')[url.Split('/', '\\').Length - 1];
                extension = filename.Split('.')[filename.Split('.').Length - 1];
                lblNo.Text = (int.Parse(lblNo.Text) + 1).ToString();
                lblUrl.Text = filename;
                lblType.Text = extension;
                lblSize.Text = commonModule.ConvertBytes(Convert.ToInt64(drv["size"]));
                if (System.IO.File.Exists(Server.MapPath("imgedit/" + extension + ".gif")))
                    imgType.ImageUrl = "imgedit/" + extension + ".gif";
                else
                    imgType.ImageUrl = "imgedit/file.gif";

                if (Convert.ToInt32(drv["isVisible"]) == 1)
                    imgVisible.ImageUrl = "imgEdit/checked.gif";
                else
                    imgVisible.ImageUrl = "imgEdit/unchecked.gif";

                if (bEn)
                {
                    lblDocTitle.Text = Convert.IsDBNull(drv["title_en"])? "": drv["title_en"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc_en"])? "": drv["desc_en"].ToString();
                }
                else
                {
                    lblDocTitle.Text = Convert.IsDBNull(drv["title"])? "":drv["title"].ToString();
                    lblDesc.Text = Convert.IsDBNull(drv["desc"]) ? "" : drv["desc"].ToString();
                }

            if (e.Item.ItemType == ListItemType.Header)
            {
                LinkButton btnFilename = (LinkButton)e.Item.Cells[5].Controls[0];
                LinkButton btnTitle = (LinkButton)e.Item.Cells[4].Controls[0];
                LinkButton btnDesc = (LinkButton)e.Item.Cells[6].Controls[0];
                if (!bEn)
                    btnFilename.Text = "Nama File";
                    btnTitle.Text = "Judul";
                    btnDesc.Text = "Keterangan";
                    e.Item.Cells[7].Text = "Ukuran";
                    e.Item.Cells[8].Text = "Tipe";
                }
            }
        }

    }
}
