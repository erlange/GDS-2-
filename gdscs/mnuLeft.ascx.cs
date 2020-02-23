using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;

namespace gds
{
    public partial class mnuLeft : System.Web.UI.UserControl
    {
        ArrayList sl;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Literal1.Text = BuildMenuRoot(commonModule.IsEnglish());

            if (commonModule.IsInAdminsRole())
                Literal2.Text = BuildAdminMenuRoot(commonModule.IsEnglish());

            Literal2.Visible = commonModule.IsInAdminsRole();
        
        }

        public string BuildMenuRoot(bool isEnglish)
        {
            DataSet ds;
            var fsOut = new StringBuilder(string.Empty);
            sl = new ArrayList();
            if (Cache["mnuLeft"] == null)
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("mnuLeft.xml"));
            }
            else
                ds = (DataSet)Cache["mnuLeft"];

            fsOut.Append(BuildMenuChildren(ds.Tables[0], "menuparentid IS NULL AND isvisible= '1'", isEnglish));
            return fsOut.ToString();
        }
        public string BuildMenuChildren(DataTable table, string where, bool isEnglish)
        {
            string desc;
            var fsOut = new StringBuilder(string.Empty);
            DataRow[] dRw = table.Select(where);
            for (int i = 0, loopTo = dRw.Length - 1; i <= loopTo; i++)
            {
                if (!sl.Contains(dRw[i]["menuid"].ToString()))
                {
                    if (isEnglish)
                        desc = (dRw[i].IsNull("menutitleen") ? dRw[i]["menutitle"].ToString() : dRw[i]["menutitleen"].ToString());
                    else
                        desc = (dRw[i].IsNull("menutitle") ? dRw[i]["menutitle"].ToString() : dRw[i]["menutitle"].ToString());

                    sl.Add(dRw[i]["menuid"]);
                    if (dRw[i]["isleaf"].ToString() == "0") // bukan leaf-level
                    {
                        fsOut.AppendLine("<div class=\"mnuLeft\" onmouseover=\"chs(this,'mnuLeftHover')\" onmouseout=\"chs(this,'mnuLeft')\">");
                        fsOut.AppendFormat("<div onclick=\"sh0('d{0}')\">", dRw[i]["menuid"].ToString());
                        fsOut.AppendLine();
                        if (dRw[i]["ismemberexpanded"] == "0")
                            fsOut.AppendFormat("<img id=\"d{0}img\" src=\"images/plus.gif\" />&nbsp;", dRw[i]["menuid"].ToString());
                        else if (dRw[i]["ismemberexpanded"] == "1")
                            fsOut.AppendFormat("<img id=\"d{0}img\" src=\"images/minus.gif\" />&nbsp;", dRw[i]["menuid"]);

                        fsOut.AppendLine(desc);
                        fsOut.AppendLine("</div>");
                        if (dRw[i]["ismemberexpanded"] == "0")
                            fsOut.AppendFormat("<div id=\"d{0}\" style=\"padding:0px 0px 0px 15px;display:none;\">", dRw[i]["menuid"]);
                        else if (dRw[i]["ismemberexpanded"] == "1")
                            fsOut.AppendFormat("<div id=\"d{0}\" style=\"padding:0px 0px 0px 15px;display:block;\">", dRw[i]["menuid"]);

                        fsOut.AppendLine();
                        fsOut.Append(BuildMenuChildren(table, string.Format(" menuparentid = '{0}' AND isvisible = '1'", dRw[i]["menuid"]), isEnglish));
                        fsOut.AppendLine("</div>");
                        fsOut.AppendLine("</div>");
                    }
                    else // Leaf
                    {
                        fsOut.AppendFormat("<div class=\"mnuLeftItem\">");
                        fsOut.AppendFormat("<a href=\"{0}\" class=\"mnuLeftLink\">", dRw[i]["href"]);
                        fsOut.AppendFormat("<img  border=\"0\" src=\"{0}\" />&nbsp;", dRw[i]["imageUrl"]);
                        fsOut.Append(desc);
                        fsOut.AppendFormat("</a>");
                        fsOut.AppendLine("</div>");
                    } // dRw(i)("isleaf") <> 1

                    if (dRw[i]["isleaf"] == "0")
                    {
                    } // Not dRw(i)("isleaf") = 1
                } // Not sl.Contains(dRw(i)("menuid"))
            }



            return fsOut.ToString();
        }

        public string BuildAdminMenuRoot(bool isEnglish)
        {
            var fsOut = new StringBuilder(string.Empty);
            sl = new ArrayList();
            var ds = new DataSet();
            ds.ReadXml(Server.MapPath("mnuAdmin.xml"));
            fsOut.Append(BuildAdminMenuChildren(ds.Tables[0], "menuparentid IS NULL And isVisible = '1'", isEnglish));
            return fsOut.ToString();
        }

        public string BuildAdminMenuChildren(DataTable table, string where, bool isEnglish)
        {
            string desc = "";
            var fsOut = new StringBuilder(string.Empty);
            DataRow[] dRw = table.Select(where);
            for (int i = 0, loopTo = dRw.Length - 1; i <= loopTo; i++)
            {
                if (!sl.Contains(dRw[i]["menuid"].ToString()))
                {
                    if (isEnglish)
                        desc = dRw[i].IsNull("menutitleen") ? dRw[i]["menutitle"].ToString() : dRw[i]["menutitleen"].ToString();
                    else
                        desc = dRw[i].IsNull("menutitle") ? dRw[i]["menutitle"].ToString() : dRw[i]["menutitle"].ToString();

                    sl.Add(dRw[i]["menuid"]);
                    if (dRw[i]["isleaf"] == "0") // bukan leaf-level
                    {
                        fsOut.AppendLine("<div class=\"mnuLeft\" onmouseover=\"chs(this,'mnuLeftHover')\" onmouseout=\"chs(this,'mnuLeft')\">");
                        fsOut.AppendFormat("<div onclick=\"sh0('ad{0}')\">", dRw[i]["menuid"]);
                        if (dRw[i]["ismemberexpanded"] == "0")
                            fsOut.AppendFormat("<img id=\"ad{0}img\" src=\"images/plus.gif\" />&nbsp;", dRw[i]["menuid"]);
                        else if (dRw[i]["ismemberexpanded"] == "1")
                            fsOut.AppendFormat("<img id=\"ad{0}img\" src=\"images/minus.gif\" />&nbsp;", dRw[i]["menuid"]);

                        fsOut.Append(desc);
                        fsOut.AppendLine("</div>");
                        if (dRw[i]["ismemberexpanded"] == "0")
                            fsOut.AppendFormat("<div id=\"ad{0}\" style=\"padding:0px 0px 0px 20px;display:none;\">", dRw[i]["menuid"]);
                        else if (dRw[i]["ismemberexpanded"] == "1")
                            fsOut.AppendFormat("<div id=\"ad{0}\" style=\"padding:0px 0px 0px 20px;display:block;\">", dRw[i]["menuid"]);

                        fsOut.AppendLine();
                        fsOut.AppendLine(BuildMenuChildren(table, string.Format(" menuparentid = '{0}'  And isVisible = '1'", dRw[i]["menuid"].ToString()), isEnglish));
                        fsOut.AppendLine("</div>");
                        fsOut.AppendLine("</div>");
                    }
                    else // Leaf
                    {
                        fsOut.AppendLine("<div class=\"mnuLeftItem\">");
                        fsOut.AppendFormat("<a href=\"{0}\" class=\"mnuLeftLink\">", dRw[i]["href"]);
                        fsOut.Append("<img  border=\"0\" src=\"images/bullet2.gif\" />&nbsp;");
                        fsOut.Append(desc);
                        fsOut.Append("</a>");
                        fsOut.AppendLine("</div>");
                    } // dRw(i)("isleaf") <> 1

                    if (dRw[i]["isleaf"] == "0")
                    {
                    } // Not dRw(i)("isleaf") = 1
                } // Not sl.Contains(dRw(i)("menuid"))
            }

            return fsOut.ToString();
        }

    }
}