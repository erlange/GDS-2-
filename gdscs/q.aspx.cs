using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class SearchResultPage : System.Web.UI.Page
    {
        protected bool bEn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterHiddenField("__EVENTTARGET", "btnOK"); // tombol enter default 
            bEn = commonModule.IsEnglish();
            lblSearch.Text = bEn ? "Search keyword" : "Cari";
            this.lblHeader.Text = bEn ? "Search" : "Pencarian";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Params["q"]))
                {
                    if (Request.Params["q"].Length > 0)
                    {
                        string keyword = Server.HtmlEncode(Request.Params["q"]);
                        keyword = keyword.Replace("'", "").Trim();
                        TextBox1.Text = keyword;
                        Search(keyword);
                    }
                }
            }

        }

        public void Search(string keyword)
        {
            var gds2 = new gdsTable();
            DataTable dt;
            DataTable dtSearch = new DataTable();
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr;
            var sb = new StringBuilder();
            dt = gds2.GetGds2Var();
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                sb.AppendFormat("EXEC SearchGds2VariableByKeywords '{0}','{1}','{2}' ; ", keyword, dt.Rows[i]["vartbl"], dt.Rows[i]["gds_id"]);
            cm.Connection = cn;
            cm.CommandText = sb.ToString();
            {
                DataColumnCollection col = dtSearch.Columns;
                col.Add("var", typeof(string));
                col.Add("var_id", typeof(int));
                col.Add("desc", typeof(string));
                col.Add("desc_en", typeof(string));
                col.Add("q", typeof(string));
                col.Add("q_en", typeof(string));
                col.Add("gds_id", typeof(int));
            }

            cn.Open();
            dr = cm.ExecuteReader();
            do
            {
                while (dr.Read())
                {
                    DataRow drw;
                    drw = dtSearch.NewRow();
                    drw["var"] = dr["var"];
                    drw["var_id"] = dr["var_id"];
                    drw["desc"] = dr["desc"];
                    drw["desc_en"] = dr["desc_en"];
                    drw["q"] = dr["q"];
                    drw["q_en"] = dr["q_en"];
                    drw["gds_id"] = dr["gds_id"];
                    dtSearch.Rows.Add(drw);
                }
            }
            while (dr.NextResult());
            dr.Close();
            cn.Close();
            if (dtSearch.Rows.Count > 0)
            {
                DataGrid1.Visible = true;
                dtSearch.DefaultView.Sort = " q DESC";
                DataGrid1.DataSource = dtSearch.DefaultView;
                DataGrid1.DataBind();
                if (bEn)
                    lblResult.Text = "Your search for <span style=\"background-color='yellow'\">&quot;" + keyword + "&quot;</span> matches " + dtSearch.Rows.Count + " records in " + DataGrid1.PageCount + " pages.";
                else
                    lblResult.Text = "Pencarian untuk <span style=\"background-color='yellow'\">&quot;" + keyword + "&quot;</span> mendapatkan " + dtSearch.Rows.Count + " hasil pencarian dalam " + DataGrid1.PageCount + " halaman.";
            }
            else
            {
                DataGrid1.Visible = false;
                if (bEn)
                    lblResult.Text = "Your search for <span style=\"background-color='yellow'\"><i>&quot;" + keyword + "&quot;</i></span> matches no records.";
                else
                    lblResult.Text = "Tidak ada hasil pencarian untuk <span style=\"background-color='yellow'\"><i>&quot;" + keyword + "&quot;</i></span>.";
            }
        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DataRowView drw = (DataRowView)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink lblDesc = (HyperLink)e.Item.FindControl("lblDesc");
                HyperLink lblDescEn = (HyperLink)e.Item.FindControl("lblDescEn");
                HyperLink lblVarId = (HyperLink)e.Item.FindControl("lblVarId");
                Label lblQ = (Label)e.Item.FindControl("lblQ");
                Label lblQEn = (Label)e.Item.FindControl("lblQEn");
                string url;
                string keyword;
                keyword = Request.Params["q"];
                keyword = Server.HtmlDecode(keyword);
                url = string.Format("pvOut.aspx?v={0}&ds={1}&r=all&d=Natl&cp1=1&ch=1", drw["var_id"], drw["gds_id"]);
                lblDesc.NavigateUrl = url;
                lblDescEn.NavigateUrl = url;
                lblVarId.Text = url;
                lblVarId.NavigateUrl = url;
                if (bEn)
                {
                    lblDesc.Visible = false;
                    lblQ.Visible = false;
                    lblDescEn.Visible = true;
                    lblQEn.Visible = true;
                    lblDescEn.Text = lblDescEn.Text.Replace(Request.Params["q"], "<span class=\"srch\">" + Request.Params["q"] + "</span>");
                    lblQEn.Text = lblQEn.Text.Replace(Request.Params["q"], "<span class=\"srch\">" + Request.Params["q"] + "</span>");
                }
                else
                {
                    lblDesc.Visible = true;
                    lblQ.Visible = true;
                    lblDescEn.Visible = false;
                    lblQEn.Visible = false;
                    lblDesc.Text = lblDesc.Text.Replace(Request.Params["q"], "<span class=\"srch\">" + Request.Params["q"] + "</span>");
                    lblQ.Text = lblQ.Text.Replace(Request.Params["q"], "<span class=\"srch\">" + Request.Params["q"] + "</span>");
                }
            }
        }

        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            string keyword;
            keyword = Request.Params["q"];
            keyword = keyword.Replace("'", "''");
            keyword = Server.HtmlDecode(keyword);
            Search(keyword);
        }

        protected void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBox1.Text.Length > 0)
                Response.Redirect("q.aspx?q=" + TextBox1.Text, true);
            else
            {
                DataGrid1.Visible = false;
                lblResult.Text = "";
            }
        }

    }
}