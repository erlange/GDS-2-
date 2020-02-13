using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class imgd : System.Web.UI.Page
    {


        protected Boolean bEn;
        protected const int THUMBWIDTH = 400;
        protected const int THUMBHEIGHT = 400;
        protected void Page_Load(object sender, EventArgs e)
        {
            MnuTop1.SetSelectedIndex(-1);
            MnuBottom1.SetSelectedIndex(-1);
            bEn = commonModule.IsEnglish();
            if (!IsPostBack)
                ShowData(Int32.Parse(Request.Params["id"]));
        }

        public void ShowData(int id)
        {
            var img = new gdsDocuments();
            if (img.GetDocumentById(id) == 1)
            {
                int w = img.W;
                int h = img.H;
                int wT, hT;
                if (w > h)
                {
                    // wT = w / (w / c)
                    // hT = h / (w / c)
                    wT = w / (w / THUMBWIDTH);
                    hT = h / (w / THUMBWIDTH);
                }
                else
                {
                    // wT = w / (h / c)
                    // hT = h / (h / c)
                    wT = w / (h / THUMBHEIGHT);
                    hT = h / (h / THUMBHEIGHT);
                }

                // Me.img1.Width = System.Web.UI.WebControls.Unit.Pixel(wT)
                // Me.img1.Height = System.Web.UI.WebControls.Unit.Pixel(hT)

                this.ImageButton1.Width = System.Web.UI.WebControls.Unit.Pixel(wT);
                this.ImageButton1.Height = System.Web.UI.WebControls.Unit.Pixel(hT);
                this.ImageButton1.ImageUrl = string.Format("ShowImg.aspx?id={0}", img.DocumentId);
                this.lblSize.Text = img.W.ToString() + " x " + img.H.ToString();
                // Me.img1.ImageUrl = String.Format("ShowImg.aspx?id={0}", img.DocumentId)
                // Me.img1.NavigateUrl = String.Format("ShowImg.aspx?id={0}", img.DocumentId)
                this.lblFilename.Text = img.Url.Split('/')[img.Url.Split('/').Length - 1];
                
                this.lblFilename.NavigateUrl = string.Format("ShowImg.aspx?id={0}", img.DocumentId);
                if (bEn)
                {
                    // Me.img1.Text = img.Title
                    this.lblTitle.Text = img.TitleEn;
                    this.lblDesc.Text = img.DescEn;
                    this.lblClick.Text = "(Click to show picture in original size)";
                    this.lblHeader.Text = "Picture Details";
                    this.HyperLink1.Text = "Go to Image Gallery";
                }
                else
                {
                    // Me.img1.Text = img.TitleEn
                    this.lblTitle.Text = img.Title;
                    this.lblDesc.Text = img.Desc;
                    this.lblClick.Text = "(Klik pada gambar untuk ukuran aslinya)";
                    this.lblHeader.Text = "Detil Gambar";
                    this.HyperLink1.Text = "Lihat Galery";
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ShowImg.aspx?id=" + Request.Params["id"]);
        }

    }
}