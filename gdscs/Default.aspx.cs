using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        protected bool bEn;

        protected void Page_Load(object sender, EventArgs e)
        {
            //PanelComments1.PanelId = 2;

            mnuTop TopMenu1 = (mnuTop)Master.Master.FindControl("TopMenu1");
            mnuBottom MnuBottom1 = (mnuBottom)Master.Master.FindControl("MnuBottom1");

            TopMenu1.SetSelectedIndex(0);
            MnuBottom1.SetSelectedIndex(0);

            pTitle1.PanelId = 8;
            pText1.PanelId = 3;

            pTitleSurveyData.PanelId = 2;
            pTextSurveyData.PanelId = 2;

            pTitleHighlight.PanelId = 3;

            pTitleWelcome.PanelId = 1;
            pTextWelcome.PanelId = 1;

            bEn = commonModule.IsEnglish();
            getDsProv();

            string sButtonValue = bEn ? Server.HtmlDecode("Next &gt;&gt;") : Server.HtmlDecode("Lanjut &gt;&gt;");

            btn1.Value = sButtonValue;

            btn1.Visible = false;
            lstr.Visible = false;
            imgDs1.Visible = false;
            imgProv.Visible = false;

        }

        public void getDsProv()
        {
            var cn = new SqlConnection(commonModule.GetConnString());
            string sql;
            lstds.Items.Clear();
            lstr.Items.Clear();
            if (bEn)
            {
                sql = "SELECT gds_id,desc_en FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                this.lstds.Items.Add(new ListItem("- Select a Survey Dataset -", "11"));
                this.lstr.Items.Add(new ListItem("- Select a Province -", "All"));
            }
            else
            {
                sql = "SELECT gds_id,[desc] FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                this.lstds.Items.Add(new ListItem("- Pilih Dataset -", "11"));
                this.lstr.Items.Add(new ListItem("- Pilih Provinsi -", "All"));
            }

            var cm = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    this.lstds.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }

                dr.NextResult();
                while (dr.Read())
                    this.lstr.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                Session["errMsg"] = ex.Message;
                Response.Redirect("err.aspx");
            }
            catch (Exception ex)
            {
                Session["errMsg"] = ex.Message;
                Response.Redirect("err.aspx");
            }
        }

    }
}