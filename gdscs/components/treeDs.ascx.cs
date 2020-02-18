using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace gds
{
    public partial class treeDs : System.Web.UI.UserControl
    {
        bool bEn;
        int iDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            {
                GetRequest();
                GetDs();
            }
            //catch (SqlException ex)
            //{
            //    commonModule.RedirectError(ex);

            //    //Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
            //    //Response.Redirect("err.aspx");
            //}
            //catch (Exception ex)
            //{
            //    commonModule.RedirectError(ex);

            //    //Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
            //    //System.Web.HttpContext.Current.Response.Redirect("err.aspx");
            //}
        }

        public void GetRequest()
        {
            bEn = commonModule.IsEnglish();
            if (Request.Params["ds"] != null)
                iDs = int.Parse(Request.Params["ds"]);
            else
                iDs = 11;
        }

        public void GetDs()
        {
            var cn = new SqlConnection(commonModule.GetConnString());
            string sql;
            ds.Items.Clear();
            if (bEn)
            {
                sql = "SELECT gds_id,desc_en FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                ds.Items.Add(new ListItem("- Select a Survey Dataset -", "11"));
            }
            else
            {
                sql = "SELECT  gds_id,[desc] FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                ds.Items.Add(new ListItem("- Pilih Dataset -", "11"));
            }

            var cm = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                var li = new ListItem(dr[1].ToString(), dr[0].ToString());
                if (li.Value == iDs.ToString())
                {
                    li.Selected = true;
                }

                ds.Items.Add(li);
            }

            dr.Close();
            cn.Close();
        }
    }
}