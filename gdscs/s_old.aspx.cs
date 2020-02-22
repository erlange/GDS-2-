using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class gds2_s_old : System.Web.UI.Page
    {
        protected bool bEn;
        //protected mnuBottom MnuBottom1;
        //protected mnuTop MnuTop1;
        //protected panelGenericTitle pTitle1;
        //protected panelGenericText pText1;
        //protected panelGenericText pText2;
        //protected panelGenericText pText3;
        string sButtonValue = "Lanjut &gt;&gt;";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetRequest();
                SetUi();
                SetContent();
                SetHelp();
                getDsProv();
            }
            catch (SqlException ex)
            {
                Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
                Response.Redirect("err.aspx");
            }
            catch (Exception ex)
            {
                Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
                Response.Redirect("err.aspx");
            }
        }

        public void SetHelp()
        {
            if (bEn)
            {
                imgDs1.Attributes.Add("onmouseover", "ci('tip','GDS2 survey dataset list. Choose one from the list, then select a province from the province list below, then click on Next button')");
                imgDs2.Attributes.Add("onmouseover", "ci('tip','GDS2 survey dataset list. Choose one from the list, then click on Next button')");
                imgDs3.Attributes.Add("onmouseover", "ci('tip','GDS2 survey dataset list. Choose one from the list, then click on Next button')");
                imgProv.Attributes.Add("onmouseover", "ci('tip','Province selection list. Choose one province from the list, then click on Next button')");
            }
            else
            {
                imgDs1.Attributes.Add("onmouseover", "ci('tip','Daftar dataset survey GDS2. Pilihlah salah satu dataset, lalu pilihlah satu propinsi, kemudian klik tombol Lanjut')");
                imgDs2.Attributes.Add("onmouseover", "ci('tip','Daftar dataset survey GDS2. Pilihlah salah satu dataset, kemudian klik tombol Lanjut')");
                imgDs3.Attributes.Add("onmouseover", "ci('tip','Daftar dataset survey GDS2. Pilihlah salah satu dataset, kemudian klik tombol Lanjut')");
                imgProv.Attributes.Add("onmouseover", "ci('tip','Daftar propinsi survey GDS2. Pilihlah salah satu propinsi, kemudian klik tombol Lanjut')");
            }

            imgDs1.Attributes.Add("onmousemove", "cm('tip')");
            imgDs1.Attributes.Add("onmouseout", "ch('tip')");
            imgDs2.Attributes.Add("onmousemove", "cm('tip')");
            imgDs2.Attributes.Add("onmouseout", "ch('tip')");
            imgDs3.Attributes.Add("onmousemove", "cm('tip')");
            imgDs3.Attributes.Add("onmouseout", "ch('tip')");
            imgProv.Attributes.Add("onmousemove", "cm('tip')");
            imgProv.Attributes.Add("onmouseout", "ch('tip')");
        }
        public void SetContent()
        {
            pTitle1.PanelId = 7;
            pText1.PanelId = 3;
            pText2.PanelId = 4;
            pText3.PanelId = 5;
        }

        public void GetRequest()
        {
            bEn = commonModule.IsEnglish();
            MnuBottom1.SetSelectedIndex(2);
            MnuTop1.SetSelectedIndex(2);
        }

        public void SetUi()
        {
            if (bEn)
            {
                sButtonValue = Server.HtmlDecode("Next &gt;&gt;");
            }
            else
            {
                sButtonValue = Server.HtmlDecode("Lanjut &gt;&gt;");
            }

            btn1.Value = sButtonValue;
            btn2.Value = sButtonValue;
            btn3.Value = sButtonValue;
        }

        public void getDsProv()
        {
            var cn = new SqlConnection(commonModule.GetConnString());
            string sql;
            lstds.Items.Clear();
            lstds2.Items.Clear();
            lstr.Items.Clear();
            if (bEn)
            {
                sql = "SELECT gds_id,desc_en FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                this.lstds.Items.Add(new ListItem("- Select a Survey Dataset -", "11"));
                this.lstds2.Items.Add(new ListItem("- Select a Survey Dataset -", "11"));
                this.lstr.Items.Add(new ListItem("- Select a Province -", "All"));
            }
            else
            {
                sql = "SELECT gds_id,[desc] FROM gds2 WHERE isVisible=1;SELECT prov,provnm FROM t01prov";
                this.lstds.Items.Add(new ListItem("- Pilih Dataset -", "11"));
                this.lstds2.Items.Add(new ListItem("- Pilih Dataset -", "11"));
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
                    this.lstds2.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                    this.lstds3.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
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