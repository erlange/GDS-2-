using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace gds
{
    public partial class treeScore : System.Web.UI.UserControl
    {
        private string sCn, gdsvar, sOut, s, sErr;
        private short i;
        private bool bErr;
        private int iDs;
        private gdsTable oGt;
        private gdsGeo oGg;
        private string sProv, sKabu;
        private bool bEn;
        protected string sDs;

        protected void Page_Load(object sender, EventArgs e)
        {
            Main();
        }

        private string BuildSqlScore()
        {
            var sb = new StringBuilder();
            // .Append("	SELECT var_id, var_parent, lvl, ord, [var], [desc],[desc_en],isContVar ,isReversed")
            sb.AppendLine("	SELECT var_id, var_parent, lvl, ord, [var], [desc],[desc_en],isContVar ");
            sb.AppendLine("	FROM " + oGt.VarTable);
            sb.AppendLine("	WHERE isContVar > 0 AND isVisible=1");
            sb.AppendLine("	ORDER BY ord ");
            return sb.ToString();
        }

        public void GetRequest()
        {
            if (Request.Params["ds"] != null)
                iDs = Convert.ToInt32(Request.Params["ds"]);
            else
                iDs = 11;

            sProv = Request.Params["r"] != "" ? Request.Params["r"] : "11";
            sKabu = Request.Params["d"] != "" ? Request.Params["d"] : "11";
            bEn = commonModule.IsEnglish();
            oGt = new gdsTable(iDs);
            oGg = new gdsGeo(sProv, sKabu);
        }

        private string WriteOpt(string Name, DataTable tbl)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<SELECT NAME=\"" + Name + "\" CLASS=\"i4\" STYLE=\"background:white;width:500px;\" onchange=\"vl('" + Name + "');\"> ");
            if (bEn)
                sb.AppendLine("<OPTION VALUE=\"0\">-- Please select a variable below --");
            else
                sb.AppendLine("<OPTION VALUE=\"0\">-- Pilih satu variabel dari daftar di bawah ini --");

            sb.AppendLine();
            for (i = 0; i < tbl.Rows.Count ; i++)
            {
                string desc;
                if (bEn)
                {
                    if (tbl.Rows[i].IsNull("desc_en"))
                        desc = tbl.Rows[i]["desc"].ToString();
                    else
                        desc = tbl.Rows[i]["desc_en"].ToString();
                }
                else
                    desc = tbl.Rows[i]["desc"].ToString();

                switch (tbl.Rows[i]["lvl"].ToString())
                {
                    case "2":
                        {
                            sb.AppendFormat("<OPTION VALUE=\"0\"  CLASS=\"i0\">{0}", desc);
                            break;
                        }

                    case "1":
                        {
                            sb.AppendFormat("<OPTION VALUE=\"0\"  CLASS=\"i1\">&nbsp;&nbsp;&nbsp;{0}", desc);
                            break;
                        }

                    case "0":
                        {
                            if (!(Request.Params[Name] == null))
                            {
                                if (Request.Params[Name].Split('~')[0] == tbl.Rows[i]["var_id"].ToString())
                                    sb.AppendFormat("<OPTION SELECTED VALUE=\"{0}~{1}~{0}~{3}\" CLASS=\"i4\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{2}", new object[] { tbl.Rows[i]["var_id"], 0, desc, 0 });
                                else
                                    sb.AppendFormat("<OPTION VALUE=\"{0}~{1}~{0}~{3}\" CLASS=\"i4\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{2}", new object[] { tbl.Rows[i]["var_id"], 0, desc, 0 });
                            }
                            else
                                sb.AppendFormat("<OPTION VALUE=\"{0}~{1}~{0}~{3}\" CLASS=\"i4\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{2}", new object[] { tbl.Rows[i]["var_id"], 0, desc, 0 });

                            break;
                        }
                }
                sb.AppendLine();
            }

            sb.AppendLine("</SELECT>");
            sb.AppendLine();
            return sb.ToString();
        }
        private void Main()
        {
            var sb = new StringBuilder();
            // Try
            // SniffBrowser(Request.ServerVariables("HTTP_USER_AGENT"))
            // SelDS(Request.QueryString("ds"))
            GetRequest();
            DataTable dt = commonModule.GetData(commonModule.GetConnString(), BuildSqlScore());

            // If Not bIE Then
            // s = sIE
            // End If
            // .Append("<FORM NAME=""frmVar"" METHOD=""GET""> ")
            sb.Append("<TR><TD CLASS=\"pnlVar\">");
            if (bEn)
                sb.AppendFormat("<B>{0} Variable Selection</B>", oGt.Desc_En);
            else
                sb.AppendFormat("<B>Pilihan Variabel {0}</B>", oGt.Desc);

            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("&nbsp;");
            sb.Append("</TD><TD>");

            sb.Append(bEn ? "<B>Weighting Value</B>" : "<B>Nilai Bobot</B>");
            
            sb.Append("</TD></TR>");
            sb.Append("<TR><TD CLASS=\"pnlVar\">");
            sb.Append(this.WriteOpt("v0", dt));
            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errv0\"></DIV>");
            sb.Append("</TD><TD>");

            if (Request.Params["vt0"] != null)
                sb.AppendFormat("<INPUT TYPE=\"text\" NAME=\"vt0\"  VALUE=\"{0}\" TABINDEX=\"10\" onchange=\"vt('vt0');\" SIZE=\"2\" MAXLENGTH=\"4\">", Request.Params["vt0"].ToString());
            else
                sb.Append("<INPUT TYPE=\"text\" NAME=\"vt0\"  VALUE=\"0.00\" TABINDEX=\"10\" onchange=\"vt('vt0');\" SIZE=\"2\" MAXLENGTH=\"4\">");

            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errvt0\"></DIV>");
            sb.Append("</TD></TR>");
            sb.Append("<TR><TD CLASS=\"pnlVar\">");
            sb.Append(this.WriteOpt("v1", dt));
            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errv1\"></DIV>");
            sb.Append("</TD><TD>");
            if (Request.Params["vt1"] != null)
                sb.AppendFormat("<INPUT TYPE=\"text\" NAME=\"vt1\"  VALUE=\"{0}\" TABINDEX=\"10\" onchange=\"vt('vt1');\" SIZE=\"2\" MAXLENGTH=\"4\">", Request.Params["vt1"].ToString());
            else
                sb.Append("<INPUT TYPE=\"text\" NAME=\"vt1\"  VALUE=\"0.00\" TABINDEX=\"10\" onchange=\"vt('vt1');\" SIZE=\"2\" MAXLENGTH=\"4\">");

            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errvt1\"></DIV>");
            sb.Append("</TD></TR>");
            sb.Append("<TR><TD CLASS=\"pnlVar\">");
            sb.Append(this.WriteOpt("v2", dt));
            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errv2\"></DIV>");
            sb.Append("</TD><TD>");
            if (Request.Params["vt2"] != null)
                sb.AppendFormat("<INPUT TYPE=\"text\" NAME=\"vt2\"  VALUE=\"{0}\" TABINDEX=\"10\" onchange=\"vt('vt2');\" SIZE=\"2\" MAXLENGTH=\"4\">", Request.Params["vt2"].ToString());
            else
                sb.Append("<INPUT TYPE=\"text\" NAME=\"vt2\"  VALUE=\"0.00\" TABINDEX=\"10\" onchange=\"vt('vt2');\" SIZE=\"2\" MAXLENGTH=\"4\">");

            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errvt2\"></DIV>");
            sb.Append("</TD></TR>");
            sb.Append("<TR><TD CLASS=\"pnlVar\">");
            sb.Append(this.WriteOpt("v3", dt));
            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errv3\"></DIV>");
            sb.Append("</TD><TD>");
            if (Request.Params["vt3"] != null)
                sb.AppendFormat("<INPUT TYPE=\"text\" NAME=\"vt3\"  VALUE=\"{0}\" TABINDEX=\"10\" onchange=\"vt('vt3');\" SIZE=\"2\" MAXLENGTH=\"4\">", Request.Params["vt3"].ToString());
            else
                sb.Append("<INPUT TYPE=\"text\" NAME=\"vt3\"  VALUE=\"0.00\" TABINDEX=\"10\" onchange=\"vt('vt3');\" SIZE=\"2\" MAXLENGTH=\"4\">");

            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errvt3\"></DIV>");
            sb.Append("</TD></TR>");
            sb.Append("<TR><TD CLASS=\"pnlVar\">");
            sb.Append(this.WriteOpt("v4", dt));
            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errv4\"></DIV>");
            sb.Append("</TD><TD>");
            if (Request.Params["vt4"] != null)
                sb.AppendFormat("<INPUT TYPE=\"text\" NAME=\"vt4\"  VALUE=\"{0}\" TABINDEX=\"10\" onchange=\"vt('vt4');\" SIZE=\"2\" MAXLENGTH=\"4\">", Request.Params["vt3"].ToString());
            else
                sb.Append("<INPUT TYPE=\"text\" NAME=\"vt4\"  VALUE=\"0.00\" TABINDEX=\"10\" onchange=\"vt('vt4');\" SIZE=\"2\" MAXLENGTH=\"4\">");

            sb.Append("</TD><TD CLASS=\"pnlVar\">");
            sb.Append("<DIV CLASS=\"err\" ID=\"errvt4\"></DIV>");
            sb.Append("</TD></TR>");
            sb.Append("<TR BGCOLOR=\"#EEEEEE\"><TD ALIGN=\"right\" CLASS=\"pnlVar\" STYLE=\"text-align:right;color:#660000;\">");
            if (bEn)
                sb.Append("Total (For best result, this value should be max 10)");
            else
                sb.Append("Total (Nilai ini sebaiknya bernilai sampai 10)");

            sb.Append("</TD><TD>");
            sb.Append("&nbsp;</TD><TD CLASS=\"pnlVar\">");
            if (Request.Params["vt10"] != null)
                sb.AppendFormat("<INPUT TYPE=\"text\" NAME=\"vt10\"  READONLY VALUE=\"{0}\" TABINDEX=\"12\" SIZE=\"2\" MAXLENGTH=\"4\" STYLE=\"background:#ffffcc;border:black 1px inset;\">", Request.Params["vt10"].ToString());
            else
                sb.Append("<INPUT TYPE=\"text\" NAME=\"vt10\"  READONLY VALUE=\"0.00\" TABINDEX=\"12\" SIZE=\"2\" MAXLENGTH=\"4\" STYLE=\"background:#ffffcc;border:black 1px inset;\">");

            sb.Append("</TD><TD>");
            sb.Append("<DIV CLASS=\"err\" ID=\"errvt10\"></DIV>");
            sb.Append("</TD></TR>");
            sb.Append("<INPUT TYPE=\"hidden\" NAME=\"en\" VALUE=\"" + (bEn ? 1 : 0) + "\"></INPUT> ");
            sb.Append("<INPUT TYPE=\"hidden\" NAME=\"d\" VALUE=\"" + sKabu + "\"></INPUT> ");
            sb.Append("<INPUT TYPE=\"hidden\" NAME=\"r\" VALUE=\"" + sProv + "\"></INPUT> ");
            sb.Append("<INPUT TYPE=\"hidden\" NAME=\"ds\" VALUE=\"" + iDs + "\"></INPUT> ");

            if (bEn)
                sb.Append("<TR BGCOLOR=\"#DDDDDD\"><TD COLSPAN=\"4\" ALIGN=\"CENTER\"><INPUT CLASS=\"hnd\" TYPE=\"checkbox\" NAME=\"ch\" ID=\"ch\" VALUE=\"1\" CHECKED><LABEL FOR=\"ch\">Plot with chart?</LABEL></INPUT></TD></TR>");
            else
                sb.Append("<TR BGCOLOR=\"#DDDDDD\"><TD COLSPAN=\"4\" ALIGN=\"CENTER\"><INPUT CLASS=\"hnd\" TYPE=\"checkbox\" NAME=\"ch\" ID=\"ch\" VALUE=\"1\" CHECKED><LABEL FOR=\"ch\">Tampilkan chart?</LABEL></INPUT></TD></TR>");

            sb.Append("<TR BGCOLOR=\"#EEEEEE\"><TD COLSPAN=\"4\" ALIGN=\"CENTER\">");
            if (bEn)
            {
                sb.Append("<INPUT CLASS=\"hnd\" TYPE=\"Button\" VALUE=\"<< Back\" ONCLICK=\"history.go(-1);\">");
                sb.Append("&nbsp;&nbsp;");
                sb.Append("<INPUT CLASS=\"hnd\" TYPE=\"SUBMIT\" VALUE=\"Next >>\">");
            }
            else
            {
                sb.Append("<INPUT CLASS=\"hnd\" TYPE=\"Button\" VALUE=\"<< Kembali\" ONCLICK=\"history.go(-1);\">");
                sb.Append("&nbsp;&nbsp;");
                sb.Append("<INPUT CLASS=\"hnd\" TYPE=\"SUBMIT\" VALUE=\"Lanjut >>\">");
            }
            // .Append("</FORM>")

            sb.Append("</TD></TR>");
            this.Literal2.Text += sb.ToString();
        }


    }
}