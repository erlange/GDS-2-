using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Text;

namespace gds
{
    public partial class mnuAdmin : System.Web.UI.UserControl
    {
        ArrayList sl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (commonModule.IsInAdminsRole())
            {
                this.Literal1.Visible = true;
                this.Literal1.Text = BuildMenuRoot(commonModule.IsEnglish());
            }
            else
                this.Literal1.Visible = false;
        }


        string BuildMenuRoot(bool isEnglish)
        {
            System.Text.StringBuilder fsOut = new System.Text.StringBuilder(String.Empty);
            sl = new ArrayList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("mnuAdmin.xml"));
            // With...
            return fsOut.ToString();
        }

        public string BuildMenuChildren(DataTable table, string where, bool isEnglish)
        {
            string desc;
            var fsOut = new StringBuilder(string.Empty);
            DataRow[] dRw = table.Select(where);
            for (int i = 0, loopTo = dRw.Length - 1; i <= loopTo; i++)
            {
                if (!sl.Contains(dRw[i]["menuid"]))
                {
                    if (isEnglish)
                        desc = (dRw[i].IsNull("menutitleen") ? dRw[i]["menutitle"] : dRw[i]["menutitleen"]).ToString();
                    else
                        desc = (dRw[i].IsNull("menutitle") ? "" : dRw[i]["menutitle"]).ToString();

                    // isEnglish
                    sl.Add(dRw[i]["menuid"]);
                    if (dRw[i]["isleaf"] == "0")
                    {
                        {
                            var withBlock = fsOut;
                            fsOut.AppendFormat("<div class=\"mnuLeft\" onmouseover=\"chs(this,'mnuLeftHover')\" onmouseout=\"chs(this,'mnuLeft')\">");
                            fsOut.AppendFormat("\n");

                            withBlock.AppendFormat("<div onclick=\"sh0('d{0}')\">", dRw[i]["menuid"]);
                            if (dRw[i]["ismemberexpanded"].ToString() == "0")
                                withBlock.AppendFormat("<img id=\"d{0}img\" src=\"images/plus.gif\" />&nbsp;", dRw[i]["menuid"]);
                            else if (dRw[i]["ismemberexpanded"].ToString() == "1")
                                withBlock.AppendFormat("<img id=\"d{0}img\" src=\"images/minus.gif\" />&nbsp;", dRw[i]["menuid"]);
                            withBlock.Append(desc);
                            withBlock.AppendFormat("</div>");
                            withBlock.AppendFormat("\n");

                            if (dRw[i]["ismemberexpanded"] == "0")
                                withBlock.AppendFormat("<div id=\"d{0}\" style=\"padding:0px 0px 0px 20px;display:none;\">", dRw[i]["menuid"]);
                            else if (dRw[i]["ismemberexpanded"] == "1")
                                withBlock.AppendFormat("<div id=\"d{0}\" style=\"padding:0px 0px 0px 20px;display:block;\">", dRw[i]["menuid"]);
                            withBlock.AppendFormat("\n");
                            withBlock.Append(BuildMenuChildren(table, string.Format(" menuparentid = '{0}'  And isVisible = '1'", dRw[i]["menuid"]), isEnglish));
                            withBlock.AppendFormat("\n");
                            withBlock.AppendFormat("</div>");
                            withBlock.AppendFormat("\n");

                            fsOut.AppendFormat("</div>");
                            fsOut.AppendFormat("\n");
                        }
                    }
                    else
                    {
                        var withBlock1 = fsOut;
                        withBlock1.AppendFormat("<div class=\"mnuLeftItem\">");
                        withBlock1.AppendFormat("<a href=\"{0}\" class=\"mnuLeftLink\">", dRw[i]["href"]);
                        withBlock1.AppendFormat("<img  border=\"0\" src=\"images/bullet2.gif\" />&nbsp;");
                        withBlock1.Append(desc);
                        withBlock1.AppendFormat("</a>");
                        withBlock1.AppendFormat("</div>");
                        withBlock1.AppendFormat("\n");
                    } // dRw(i)("isleaf") <> 1
                    if (dRw[i]["isleaf"] == "0")
                    {
                        {
                            var withBlock2 = fsOut;
                        }
                    } // Not dRw(i)("isleaf") = 1
                } // Not sl.Contains(dRw(i)("menuid"))
            }
            return fsOut.ToString();
        }

    }
}