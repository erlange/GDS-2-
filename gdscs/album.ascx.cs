using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace gds
{
    public partial class album : System.Web.UI.UserControl
    {
        private DataTable _ImageTable;
        private int myVar;
        public DataTable ImageTable
        {
            get { return _ImageTable; }
            set { _ImageTable = value; }
        }

        public int RepeatColumns
        {
            get { return DataList1.RepeatColumns; }
            set { DataList1.RepeatColumns = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ShowData();
        }

        void ShowData()
        {
            DataList1.DataSource = _ImageTable;
            DataList1.DataBind();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink btnEdit;
                LinkButton btnDelete;
                HyperLink lblUrl;
                HyperLink lblImg;
                Image img1;
                Label lblTitle;
                btnEdit = (HyperLink)e.Item.FindControl("btnEdit");
                lblUrl = (HyperLink)e.Item.FindControl("lblUrl");
                lblImg = (HyperLink)e.Item.FindControl("lblImg");
                img1 = (Image)e.Item.FindControl("img1");
                lblTitle = (Label)e.Item.FindControl("lblTitle");
                btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                lblUrl.Text = lblUrl.Text.Trim().Replace(commonModule.IMGDESTVIRTUALPATH, "");
                if (commonModule.IsEnglish())
                {
                    lblUrl.ToolTip = Convert.IsDBNull(drv["titlealt_en"]) ? "" : drv["titlealt_en"].ToString();
                    lblImg.ToolTip = Convert.IsDBNull(drv["titlealt_en"]) ? "" : drv["titlealt_en"].ToString();
                    img1.ToolTip = Convert.IsDBNull(drv["titlealt_en"]) ? "" : drv["titlealt_en"].ToString();
                    lblTitle.Text = Convert.IsDBNull(drv["title_en"]) ? "" : drv["title_en"].ToString();
                }
                else
                {
                    lblUrl.ToolTip = Convert.IsDBNull(drv["titlealt"]) ? "" : drv["titlealt"].ToString();
                    lblImg.ToolTip = Convert.IsDBNull(drv["titlealt"]) ? "" : drv["titlealt"].ToString();
                    img1.ToolTip = Convert.IsDBNull(drv["titlealt"]) ? "" : drv["titlealt"].ToString();
                    lblTitle.Text = Convert.IsDBNull(drv["title"]) ? "" : drv["title"].ToString();
                }

                btnDelete.CommandArgument = drv["documentid"].ToString();
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this?');");
                if (commonModule.IsInAdminsRole())
                {
                    btnEdit.Visible = true;
                }
                // btnDelete.Visible = True
                else
                {
                    btnEdit.Visible = false;
                    // btnDelete.Visible = False
                }
            }

        }

        protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            if (!(Request.UrlReferrer == null))
            {
                // Try
                if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv;
                    Response.Write(e.Item.DataItem.ToString());
                    drv = (DataRowView)e.Item.DataItem;
                    var documentid = default(int); // = drv("documentid")
                    var gdsDoc = new gdsDocuments();
                    if (gdsDoc.DeleteDocument(documentid) == 1)
                    {
                        System.IO.File.Delete(Server.MapPath(gdsDoc.Url));
                    }
                    // Response.Redirect(Request.UrlReferrer.ToString())
                }
                // Catch ex As Exception
                ShowData();

                // End Try

            }


        }
    }
}