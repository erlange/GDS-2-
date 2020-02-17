using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelAnalysis : System.Web.UI.UserControl
    {
        protected bool bEn;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            getDsProv();
            if (bEn)
            {
                lblAdv.Text = "Advanced Analysis";
                lblBasic.Text = "Basic Analysis";
                lblDual.Text = "Dual-Variable Analysis";
                lblHeader.Text = "GDS2 Analysis Modes";
                lblMulti.Text = "Multi-Variable Analysis";
            }
            else
            {
                lblAdv.Text = "Analisis Lanjut";
                lblBasic.Text = "Analisis Dasar";
                lblDual.Text = "Analisis Variabel Ganda ";
                lblHeader.Text = "Mode Analisis GDS2";
                lblMulti.Text = "Analisis Multi-Variabel";
            }
            // Put user code to initialize the page here
            btn1.Attributes.Add("onclick", string.Format("document.forms['frm1'].ds.value=document.getElementById('{0}').value;document.forms['frm1'].r.value=document.getElementById('{1}').value;document.forms['frm1'].submit();", lstds.ClientID, lstr.ClientID));
            btn2.Attributes.Add("onclick", string.Format("document.forms['frm2'].ds.value=document.getElementById('{0}').value;if(c[0].checked) {1}document.forms['frm2'].c.value=1;{2} else {1}document.forms['frm2'].c.value=2;{2}document.forms['frm2'].submit();", lstds2.ClientID, "{", "}"));
            setHelp();
        }

        public void setHelp()
        {
            if (bEn)
            {
                imgHelpAdv.Attributes.Add("onmouseover", "ci('ttip','This type of analysis allows you to determine correlation between two variables, and create score for all provinces')");
            }
            else
            {
                imgHelpAdv.Attributes.Add("onmouseover", "ci('ttip','Anda dapat menggunakan tipe analisis ini untuk menentukan korelasi antara dua variabel. Anda juga dapat membuat score hasil survei untuk seluruh propinsi berdasarkan variabel-variabel tertentu.')");
            }

            imgHelpAdv.Attributes.Add("onmousemove", "cm('ttip')");
            imgHelpAdv.Attributes.Add("onmouseout", "ch('ttip')");
            if (bEn)
            {
                imgHelpBasic.Attributes.Add("onmouseover", "ci('ttip','Choose this to analyze single variable.')");
            }
            else
            {
                imgHelpBasic.Attributes.Add("onmouseover", "ci('ttip','Analisis jenis ini untuk menampilkan hasil dari satu variabel dari data survei GDS2.')");
            }

            imgHelpBasic.Attributes.Add("onmousemove", "cm('ttip')");
            imgHelpBasic.Attributes.Add("onmouseout", "ch('ttip')");
        }

        public void getDsProv()
        {
            bEn = commonModule.IsEnglish();
            var cn = new SqlConnection(commonModule.GetConnString());
            string sql;
            var iR = default(int);
            lstds.Items.Clear();
            lstds2.Items.Clear();
            lstr.Items.Clear();
            if (char.IsNumber(Request.Params["r"], 0))
            {
                iR = Convert.ToInt32(Request.Params["r"]);
            }
            //if (Request.Params["r"] != null)
            //{
            //    iR = Convert.ToInt32(Request.Params["r"]);
            //}

            if (bEn)
            {
                sql = "SELECT gds_id,desc_en FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                lstds.Items.Add(new ListItem("- Select a Survey Dataset -", "11"));
                lstds2.Items.Add(new ListItem("- Select a Survey Dataset -", "11"));
                lstr.Items.Add(new ListItem("- Select a Province -", "All"));
            }
            else
            {
                sql = "SELECT gds_id,[desc] FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                lstds.Items.Add(new ListItem("- Pilih Dataset -", "11"));
                lstds2.Items.Add(new ListItem("- Pilih Dataset -", "11"));
                lstr.Items.Add(new ListItem("- Pilih Provinsi -", "All"));
            }

            var cm = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                var li = new ListItem(dr[1].ToString(), dr[0].ToString());
                lstds.Items.Add(li);
                lstds2.Items.Add(li);
                if (Request.Params["ds"] != null)
                {
                    if (Request.Params["ds"].ToString() == li.Value)
                    {
                        li.Selected = true;
                    }
                }
            }

            dr.NextResult();
            while (dr.Read())
            {
                var li = new ListItem(dr[1].ToString(), dr[0].ToString());
                lstr.Items.Add(li);
                if (iR.ToString() == li.Value)
                {
                    li.Selected = true;
                }
            }

            dr.Close();
            cn.Close();
        }
    }
}