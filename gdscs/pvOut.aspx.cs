using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using ChartDirector;
namespace gds
{
    public partial class pvOut : System.Web.UI.Page
    {
        protected bool bEn;
        protected bool IsSingleVar;

        protected treeComparators TreeComparators1;
        protected treeVars TreeVars1;
        protected treeLocations TreeLocations1;
        private const int INTLEGENDHEIGHT = 16;
        private const int INTBOTTOMTEXTHEIGHT = 20;
        private const int INTBASEHEIGHT = 400;
        private const float INTRATIO = 0.15F;
        private int[] INTGRADBLEU = new[] { 0, 0xFFFFFF, 128, 0x9999CC, 255, 0xFFFFFF };
        private int[] INTGRADBLEU2 = new[] { 0, 0x5555CC, 128, 0xFFFFFF, 255, 0x5555CC };
        private int[] INTGRADROUGE = new[] { 0, 0xFFFFFF, 128, 0xFF66CC, 255, 0xFFFFFF };
        private string sVarTbl;
        private string var_id;
        private string var;
        private string desc;
        private string var_parent_desc;
        private string q;
        private string sDist;
        private string sRegn;
        private string sProvNm;
        private string sKabuNm;
        protected int iDs;
        private gdsTable oGt;
        private gdsVar oGv;
        private gdsGeo oGg;
        private int contflg;
        private bool bIE = true; // TODO: Browser sniffing is important, provide some methods
        private bool isPct = false;
        private bool isChartVisible = false;
        private string[,] sComp;
        private string[] colors = new[] { "#AAFFAA", "#CCCCFF", "#FFFFAA", "#FFCC66", "#00FFFF", "#FF99FF", "#DDFF55", "#FF82BA", "#00B7AA", "#777EFF", "#63E600", "#C0C0C0", "#FF8640", "#10C4E2", "#E9D0A4", "#85B660", "#D2A08D", "#CC91FF", "#D8E2E6", "#00FF00", "#FFFFFF", "#AAFFAA", "#CCCCFF", "#FFFFAA", "#FFCC66", "#00FFFF", "#FF99FF", "#DDFF55", "#FF82BA", "#00B7AA", "#777EFF", "#63E600", "#C0C0C0", "#FF8640", "#10C4E2", "#E9D0A4", "#85B660", "#D2A08D", "#CC91FF", "#D8E2E6", "#00FF00", "#FFFFFF" };
        private int[] iColors = new[] { 0xAAFFAA, 0xCCCCFF, 0xFFFFAA, 0xFFCC66, 0xFFFF, 0xFF99FF, 0xDDFF55, 0xFF82BA, 0xB7AA, 0x777EFF, 0x63E600, 0xC0C0C0, 0xFF8640, 0x10C4E2, 0xE9D0A4, 0x85B660, 0xD2A08D, 0xCC91FF, 0xD8E2E6, 0xFF00, 0xFFFFFF, 0xAAFFAA, 0xCCCCFF, 0xFFFFAA, 0xFFCC66, 0xFFFF, 0xFF99FF, 0xDDFF55, 0xFF82BA, 0xB7AA, 0x777EFF, 0x63E600, 0xC0C0C0, 0xFF8640, 0x10C4E2, 0xE9D0A4, 0x85B660, 0xD2A08D, 0xCC91FF, 0xD8E2E6, 0xFF00, 0xFFFFFF };
        protected HtmlGenericControl Title1;
        protected int iR;
        protected mnuBottom MnuBottom1;
        protected mnuTop MnuTop1;
        protected bool isSingleVar;
        private panelResultToolBar _PanelResultToolBar1;
        protected bool isValidRequest;

        //protected panelResultToolBar PanelResultToolBar1
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _PanelResultToolBar1;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        _PanelResultToolBar1 = value;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        enum BasicCompType : int
        {
            District = 0,
            Province = 1,
            National = 2,
            Geographical = 4, // Other comparators
            OthersCompInDistProv = 8, // Other comparators in prov & kabu
            OthersCompInProv = 16, // Other comparators in prov & kabu
            Other = 32 // Other comparators
        }

        public void IncComp(ref string[,] compStr, ref int counter, string headerStr, int spanCnt)
        {
            var oldCompStr = compStr;
            compStr = new string[2, counter + 1];
            if (oldCompStr != null)
                for (var i = 0; i <= oldCompStr.Length / oldCompStr.GetLength(1) - 1; ++i)
                    Array.Copy(oldCompStr, i * oldCompStr.GetLength(1), compStr, i * compStr.GetLength(1), Math.Min(oldCompStr.GetLength(1), compStr.GetLength(1)));
            compStr[0, counter] = headerStr;
            compStr[1, counter] = spanCnt.ToString();
            counter += 1;
        }

        DataTable GetComparator(string compTbl)
        {
            DataTable dt;
            var sb = new StringBuilder();
            string[] comps = GetRequestComparators();
            if (bEn)
            {
                sb.AppendFormat("SELECT comp_id,[var],[value],desc_en,subgrp_id, subgrp_en FROM [{0}] WHERE subgrp_id IN (", compTbl);
            }
            else
            {
                sb.AppendFormat("SELECT comp_id,[var],[value],[desc],subgrp_id, subgrp FROM [{0}] WHERE subgrp_id IN (", compTbl);
            }

            for (int i = 0, loopTo = comps.Length - 1; i <= loopTo; i++)
            {
                if (Trace.IsEnabled)
                {
                    Trace.Warn("comps(" + i.ToString() + ")", comps[i]);
                }

                sb.Append(comps[i]);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1); // removes trailing ocmmaas (sorry, mustinye commas ;-)
            sb.Append(") ORDER BY comp_id");
            if (Trace.IsEnabled)
            {
                Trace.Warn("GetCompaRator", sb.ToString());
            }

            dt = commonModule.GetData(commonModule.GetConnString(), sb.ToString());
            return dt;
        }
        string[] GetRequestComparators()
        {
            var ss = default(string[]);
            var j = default(int);
            for (int i = 0, loopTo = Request.Params.Keys.Count - 1; i <= loopTo; i++)
            {
                if (Request.Params.Keys[i].IndexOf("cp") == 0) // begons (--maksudnya begins) with cp?
                {
                    var oldSs = ss;
                    ss = new string[j + 1];
                    if (oldSs != null)
                        Array.Copy(oldSs, ss, Math.Min(j + 1, oldSs.Length));
                    ss[j] = Request.Params.Keys[i].Remove(0, 2); // removes cp
                    j += 1;
                }
            }

            return ss;
        }

        string BuildSqlAvDetail(string avTbl, string tblId, string var, string gdsTbl, string aliasFld, string whereKabu, string whereProv, string kabuFld, string provFld, string whereComp, string compFld, BasicCompType compType = BasicCompType.District)
        {
            var sb = new StringBuilder();
            string r = System.Environment.NewLine;
            switch (compType)
            {
                case BasicCompType.District:
                    {
                        sb.Append("EXEC ");
                        if (bEn)
                        {
                            sb.AppendFormat(" GetResultAvEn2 '{0}','{1}','{2}','{3}','[Count{4}]','AND {5}={6} and {7}={8}' ", var, avTbl, gdsTbl, tblId, aliasFld, provFld, whereProv, kabuFld, whereKabu);
                        }
                        else
                        {
                            sb.AppendFormat(" GetResultAv2 '{0}','{1}','{2}','{3}','[Count{4}]','AND {5}={6} and {7}={8}' ", var, avTbl, gdsTbl, tblId, aliasFld, provFld, whereProv, kabuFld, whereKabu);
                        }

                        sb.Append(r);
                        break;
                    }

                case BasicCompType.National:
                    {
                        sb.Append("EXEC ");
                        if (bEn)
                        {
                            sb.AppendFormat(" GetResultAvEn2 '{0}','{1}','{2}','{3}','[Count{4}]','AND 1=1' ", var, avTbl, gdsTbl, tblId, aliasFld);
                        }
                        else
                        {
                            sb.AppendFormat(" GetResultAv2 '{0}','{1}','{2}','{3}','[Count{4}]','AND 1=1' ", var, avTbl, gdsTbl, tblId, aliasFld);
                        }

                        sb.Append(r);
                        break;
                    }

                case BasicCompType.Province:
                    {
                        sb.Append("EXEC ");
                        if (bEn)
                        {
                            sb.AppendFormat(" GetResultAvEn2 '{0}','{1}','{2}','{3}','[Count{4}]','AND {5}={6} ' ", var, avTbl, gdsTbl, tblId, aliasFld, provFld, whereProv);
                        }
                        else
                        {
                            sb.AppendFormat(" GetResultAv2 '{0}','{1}','{2}','{3}','[Count{4}]','AND {5}={6} ' ", var, avTbl, gdsTbl, tblId, aliasFld, provFld, whereProv);
                        }

                        sb.Append(r);
                        break;
                    }

                case BasicCompType.Other:
                    {
                        sb.Append("EXEC ");
                        if (bEn)
                        {
                            sb.AppendFormat(" GetResultAvEn2 '{0}','{1}','{2}','{3}','[Count{4}]','AND {5}={6} ' ", var, avTbl, gdsTbl, tblId, aliasFld, compFld, whereComp);
                        }
                        else
                        {
                            sb.AppendFormat(" GetResultAv2 '{0}','{1}','{2}','{3}','[Count{4}]','AND {5}={6} ' ", var, avTbl, gdsTbl, tblId, aliasFld, compFld, whereComp);
                        }

                        sb.Append(r);
                        break;
                    }
            }

            return sb.ToString();
        }

        string BuildSqlAvFinal()
        {
            var sCompCounter = default(int);
            var sb = new StringBuilder();
            string r = System.Environment.NewLine;
            if (sDist == "All") // All districts in a province
            {
                IncComp(ref sComp, ref sCompCounter, (bEn? "Province": "Propinsi"), 1); // The Prov
                sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, oGg.ProvName, null, oGg.Prov, null, oGt.ProvFld, null, null, BasicCompType.Province));
                sb.Append(r);
                IncComp(ref sComp, ref sCompCounter, (bEn ? "National" : "Nasional"), 1); // All Prov
                sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, (bEn ? "National" : "Nasional"), null, null, null, null, null, null, BasicCompType.National));
                sb.Append(r);
            }
            else if (sDist == "Natl") // All provinces
            {
                IncComp(ref sComp, ref sCompCounter, (bEn? "National": "Nasional"), 1); // All Prov
                sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, (bEn? "National": "Nasional"), null, null, null, null, null, null, BasicCompType.National));
                sb.Append(r);
            }
            else // single district
            {
                IncComp(ref sComp, ref sCompCounter, (bEn? "District": "Kabupaten"), 1); // The Kabu
                sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, oGg.KabuName, oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, BasicCompType.District));
                sb.Append(r);
                IncComp(ref sComp, ref sCompCounter, (bEn ? "Province" : "Propinsi"), 1); // The Prov
                sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, oGg.ProvName, null, oGg.Prov, null, oGt.ProvFld, null, null, BasicCompType.Province));
                sb.Append(r);
                sb.Append(r);
                IncComp(ref sComp, ref sCompCounter, "", 1); // All Prov
                sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, (bEn? "National": "Nasional"), null, null, null, null, null, null, BasicCompType.National));
                sb.Append(r);
            }

            string[] comps = GetRequestComparators();
            if (!(comps == null)) // kalo ada comparator
            {
                DataTable dt;
                dt = GetComparator(oGt.CompTable);
                DataRow[] drw;
                for (int i = 0, loopTo = comps.Length - 1; i <= loopTo; i++)
                {
                    drw = dt.Select("subgrp_id=" + comps[i]);
                    if (bEn) // Jagan (maksudnya: Jangan) pake IIF (soalnya FalsePart-nya tetep di-evaluate)
                    {
                        for (int j = 0, loopTo1 = drw.Length - 1; j <= loopTo1; j++)
                        {
                            if (j == 0) // for the heading we only need the first record
                            {
                                IncComp(ref sComp, ref sCompCounter, drw[j]["subgrp_en"].ToString(), drw.Length);
                            }

                            sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, "cap" + drw[j]["comp_id"].ToString() + drw[j]["desc_en"].ToString(), null, oGg.Prov, null, oGt.ProvFld, drw[j]["value"].ToString(), drw[j]["var"].ToString(), BasicCompType.Other));

                            // note the args supplied for the aliasTbl and aliasFld
                            // these must be unique, otherwise the .NET will get upset,the datatable u know

                            sb.Append(r);
                        }
                    }
                    else
                    {
                        for (int j = 0, loopTo2 = drw.Length - 1; j <= loopTo2; j++)
                        {
                            if (j == 0)
                            {
                                IncComp(ref sComp, ref sCompCounter, drw[j]["subgrp"].ToString(), drw.Length);
                            }

                            sb.Append(BuildSqlAvDetail(oGt.AvTable, oGv.Tbl_Id, oGv.Var, oGv.Tbl, "cap" + drw[j]["comp_id"].ToString() + drw[j]["desc"].ToString(), null, oGg.Prov, null, oGt.ProvFld, drw[j]["value"].ToString(), drw[j]["var"].ToString(), BasicCompType.Other));
                            sb.Append(r);
                        }
                    }
                }
            }

            if (Trace.IsEnabled)
            {
                Trace.Warn("BuildSqlAvFinal()", sb.ToString());
            }

            return sb.ToString();
        }
        string BuildSqlCat(string var, string valueTbl)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM ");
            sb.AppendLine("(");
            if (bEn)
            {
                sb.AppendFormat("	SELECT [value],[desc_en] FROM [{0}]", valueTbl);
            }
            else
            {
                sb.AppendFormat("	SELECT [value],[desc] FROM [{0}]", valueTbl);
            }

            sb.AppendLine();
            sb.AppendFormat("	WHERE [var] = '{0}'", var);
            sb.AppendLine();
            sb.AppendLine(") [cat]");
            return sb.ToString();
        }
        string BuildSqlCatDetail(string valueTbl, string tblId, string var, string aliasTbl, string aliasFld, string whereKabu, string whereProv, string kabuFld, string provFld, string whereComp, string compFld, string whereCriteria, BasicCompType compType = BasicCompType.District)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("LEFT JOIN");
            sb.AppendLine("(");
            sb.AppendFormat("     SELECT [{0}] ", var);
            sb.AppendLine();
            sb.AppendFormat("      ,COUNT ({0}) AS [Count{1}]", tblId, aliasFld);
            sb.AppendLine();
            sb.AppendFormat("     FROM [{0}] ", valueTbl);
            sb.AppendLine();
            switch (compType)
            {
                case BasicCompType.District:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] = '{1}' ", kabuFld, whereKabu);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] = '{1}' ", provFld, whereProv);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }
                        break;
                    }

                case BasicCompType.National:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }
                        break;
                    }

                case BasicCompType.Province:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] = '{1}' ", provFld, whereProv);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }

                        break;
                    }

                case BasicCompType.Geographical:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] = '{1}' ", compFld, whereComp);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }
                        break;
                    }

                case BasicCompType.OthersCompInDistProv:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] = '{1}' ", provFld, whereProv);
                        sb.AppendLine();
                        sb.AppendLine("   AND");
                        sb.AppendFormat("     [{0}] = '{1}' ", kabuFld, whereKabu);
                        sb.AppendLine();
                        sb.AppendLine("   AND");
                        sb.AppendFormat("     [{0}] = '{1}' ", compFld, whereComp);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }

                        break;
                    }

                case BasicCompType.OthersCompInProv:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] = '{1}' ", provFld, whereProv);
                        sb.AppendLine();
                        sb.AppendLine("   AND");
                        sb.AppendFormat("     [{0}] = '{1}' ", compFld, whereComp);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }
                        break;
                    }

                case BasicCompType.Other:
                    {
                        sb.AppendLine(" WHERE ");
                        sb.AppendFormat("     [{0}] = '{1}' ", compFld, whereComp);
                        sb.AppendLine();
                        sb.AppendFormat("     AND [{0}] IS NOT NULL ", var);
                        sb.AppendLine();
                        if (whereCriteria.Length > 0)
                        {
                            sb.AppendFormat("     AND {0}", whereCriteria);
                            sb.AppendLine();
                        }

                        break;
                    }
            }

            sb.AppendLine();
            sb.AppendFormat("     GROUP BY [{0}]", var);
            sb.AppendLine();
            sb.AppendLine(")");
            sb.AppendFormat("[{0}]", aliasTbl);
            sb.AppendLine();
            sb.AppendFormat(" ON Cat.Value=[{0}].[{1}] ", aliasTbl, var);
            sb.AppendLine();
            return sb.ToString();
        }
        string BuildSqlCatFinal()
        {
            var sCompCounter = default(int);
            var sb = new StringBuilder();
            sb.AppendLine(BuildSqlCat(oGv.Var, oGt.ValueTable));
            if (sDist == "All") // All districts in a province
            {
                IncComp(ref sComp, ref sCompCounter, (bEn ? "Province" : "Propinsi"), 1);
                sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, oGg.ProvName, oGg.ProvName, oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, oGv.Criteria, BasicCompType.Province));

                IncComp(ref sComp, ref sCompCounter, (bEn ? "National" : "Nasional"), 1);
                sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, (bEn ? "National" : "Nasional"), (bEn ? "National" : "Nasional"), oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, oGv.Criteria, BasicCompType.National));
            }
            else if (sDist == "Natl") // All provinces
            {
                IncComp(ref sComp, ref sCompCounter, (bEn ? "National" : "Nasional"), 1);
                sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, (bEn ? "National" : "Nasional"), (bEn ? "National" : "Nasional"), oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, oGv.Criteria, BasicCompType.National));
            }
            else // single district
            {
                IncComp(ref sComp, ref sCompCounter, (bEn ? "District" : "Kabupaten"), 1);
                sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, oGg.KabuName, oGg.KabuName, oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, oGv.Criteria, BasicCompType.District));

                IncComp(ref sComp, ref sCompCounter, (bEn ? "Province" : "Propinsi"), 1);
                sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, oGg.ProvName, oGg.ProvName, oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, oGv.Criteria, BasicCompType.Province));

                IncComp(ref sComp, ref sCompCounter, "", 1);
                sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, (bEn ? "National" : "Nasional"), (bEn ? "National" : "Nasional"), oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, null, null, oGv.Criteria, BasicCompType.National));
            }

            string[] comps = GetRequestComparators();
            if (!(comps == null)) // kalo ada comparator
            {
                DataTable dt;
                dt = GetComparator(oGt.CompTable);
                DataRow[] drw;
                for (int i = 0, loopTo = comps.Length - 1; i <= loopTo; i++)
                {
                    drw = dt.Select("subgrp_id=" + comps[i]);
                    if (bEn) // Jagan (maksudnya: Jangan) pake IIF (soalnya FalsePart-nya tetep di-evaluate)
                    {
                        for (int j = 0, loopTo1 = drw.Length - 1; j <= loopTo1; j++)
                        {
                            if (j == 0) // for the heading we only need the first record
                            {
                                IncComp(ref sComp, ref sCompCounter, drw[j]["subgrp_en"].ToString(), drw.Length);
                            }

                            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, drw[j]["desc_en"].ToString() + drw[j]["comp_id"].ToString(), "cap" + drw[j]["comp_id"].ToString() + drw[j]["desc_en"].ToString(), oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, drw[j]["value"].ToString(), drw[j]["var"].ToString(), oGv.Criteria, BasicCompType.Other));

                            // note the args supplied for the aliasTbl and aliasFld
                            // these must be unique, or the sql gets upset
                        }
                    }
                    else
                    {
                        for (int j = 0, loopTo2 = drw.Length - 1; j <= loopTo2; j++)
                        {
                            if (j == 0)
                            {
                                IncComp(ref sComp, ref sCompCounter, drw[j]["subgrp"].ToString(), drw.Length);
                            }
                            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, drw[j]["desc"].ToString() + drw[j]["comp_id"].ToString(), ("cap" + drw[j]["comp_id"].ToString()) + drw[j]["desc"], oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, drw[j]["value"].ToString(), drw[j]["var"].ToString(), oGv.Criteria, BasicCompType.Other));
                        }
                    }
                }
            }

            if (Trace.IsEnabled)
            {
                Trace.Warn("BuildSqlCatFinal()", sb.ToString());
            }

            return sb.ToString();
        }

        string BuildSqlCompGeo()
        {
            var sb = new StringBuilder();
            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, "Sumatra", "Sumatra", oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, "1", "island", BasicCompType.Geographical.ToString(), BasicCompType.District));
            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, "Jawa-Bali", "Jawa-Bali", oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, "2", "island", BasicCompType.Geographical.ToString(), BasicCompType.District));
            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, "Kalimantan", "Kalimantan", oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, "3", "island", BasicCompType.Geographical.ToString(), BasicCompType.District));
            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, "Sulawesi", "Sulawesi", oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, "4", "island", BasicCompType.Geographical.ToString(), BasicCompType.District));
            sb.AppendLine(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, "NTB-NTT", "NTB-NTT", oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, "5", "island", BasicCompType.Geographical.ToString(), BasicCompType.District));
            sb.Append(BuildSqlCatDetail(oGv.Tbl, oGv.Tbl_Id, oGv.Var, "Maluku-Papua", "Maluku-Papua", oGg.Kabu, oGg.Prov, oGt.KabuFld, oGt.ProvFld, "6", "island", BasicCompType.Geographical.ToString(), BasicCompType.District));
            return sb.ToString();
        }
        string BuildSqlCont(string var, string valueTbl, string kabuFld, string whereKabu, string provFld, string whereProv, string kabuNm, string provNm, string compFld, string whereComp, string compHeader, string tblId, BasicCompType compType, string whereCriteria)
        {
            // /// Cara ngetest di Stata pake:
            // /// ci [varName] if [condition]
            var sb = new StringBuilder();
            switch (compType)
            {
                case BasicCompType.District:
                    {
                        sb.AppendFormat("SELECT '{0}' AS [District]", kabuNm);
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.Province:
                    {
                        sb.AppendFormat("SELECT '{0}' AS [District]", provNm);
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.National:
                    {
                        sb.AppendFormat("SELECT '{0}' AS [District]", (bEn ? "National" : "Nasional"));
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.Other:
                    {
                        sb.AppendFormat("SELECT '{0}' AS [District]", compHeader);
                        sb.AppendLine();
                        break;
                    }
            }

            sb.AppendFormat(", AVG( CAST([{0}] AS MONEY) ) AS [Mean]", var);
            sb.AppendLine();
            sb.AppendFormat(", STDEVP(CAST([{0}] AS MONEY)) AS [std]", var);
            sb.AppendLine();
            sb.AppendFormat(", MIN(CAST([{0}] AS MONEY)) AS [minimum]", var);
            sb.AppendLine();
            sb.AppendFormat(", MAX(CAST([{0}] AS MONEY)) AS [maximum]", var);
            sb.AppendLine();
            sb.AppendLine(", 'CIl' = CASE ");
            // .Append(" 			WHEN (AVG(" & gdsvar & ")-2*stdevp(" & gdsvar & ")) < 0 THEN 0 " & vbCrLf)'Sing iki salah
            // .Append(" 			ELSE AVG(" & gdsvar & ")-2*stdevp(" & gdsvar & ") " & vbCrLf)'Sing iki yo salah
            sb.AppendLine();
            sb.AppendFormat(" 			WHEN (AVG(CAST ([{0}] AS MONEY))-1.96*STDEVP(CAST([{0}] AS MONEY))/SQRT(COUNT([{1}]))) < 0 THEN 0 " + System.Environment.NewLine, var, tblId); // iki sing bener
            sb.AppendLine();
            sb.AppendFormat(" 			ELSE AVG(CAST([{0}] AS MONEY))-1.96*STDEVP(CAST([{0}] AS MONEY))/SQRT(COUNT([{1}])) " + System.Environment.NewLine, var, tblId); // iki sing bener
            sb.AppendLine();
            sb.AppendLine(" 		END ");
            sb.AppendLine();
            // .Append(", AVG(" & gdsvar & ")-2*stdevp(" & gdsvar & ") as CIl " & vbCrLf)
            sb.AppendFormat(", AVG(CAST([{0}] AS MONEY))+1.96*STDEVP(CAST([{0}] AS MONEY))/SQRT(COUNT([{1}]))   as CIh " + System.Environment.NewLine, var, tblId);
            sb.AppendLine();
            sb.AppendFormat(" FROM [{0}] ", valueTbl);
            sb.AppendLine();
            sb.AppendFormat("WHERE [{0}]  IS NOT NULL  ", var);
            sb.AppendLine();
            switch (compType)
            {
                case BasicCompType.District:
                    {
                        sb.AppendFormat("AND [{0}] = '{1}'", kabuFld, whereKabu);
                        sb.AppendLine();
                        sb.AppendFormat("AND [{0}] = '{1}'", provFld, whereProv);
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.Province:
                    {
                        sb.AppendFormat("AND [{0}] = '{1}'", provFld, whereProv);
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.National:
                    {
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.OthersCompInDistProv:
                    {
                        sb.AppendFormat("AND [{0}] = '{1}'", kabuFld, whereKabu);
                        sb.AppendLine();
                        sb.AppendFormat("AND [{0}] = '{1}'", provFld, whereProv);
                        sb.AppendLine();
                        sb.AppendFormat("AND [{0}] = '{1}'", compFld, whereComp);
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.OthersCompInProv:
                    {
                        sb.AppendFormat("AND [{0}] = '{1}'", provFld, whereProv);
                        sb.AppendLine();
                        sb.AppendFormat("AND [{0}] = '{1}'", compFld, whereComp);
                        sb.AppendLine();
                        break;
                    }

                case BasicCompType.Other:
                    {
                        sb.AppendFormat("AND [{0}] = '{1}'", compFld, whereComp);
                        sb.AppendLine();
                        break;
                    }
            }

            if (whereCriteria.Length > 0)
            {
                sb.AppendFormat(" AND {0}", whereCriteria);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        void CreateChartCat(DataTable dtTable)
        {
            // Dim iColors() As Integer = {&HAAFFAA, &HCCCCFF, &HFFFFAA, &HFFCC66, &HFFFF, &HFF99FF, &HDDFF55, &HFF82BA, &HB7AA, &H777EFF, &H63E600, &HC0C0C0, &HFF8640, &H10C4E2, &HE9D0A4, &H85B660, &HD2A08D, &HCC91FF, &HD8E2E6, &HFF00, &HFFFFFF}'aslinya
            int intHeight;
            var Labels = new string[dtTable.Columns.Count];
            ChartDirector.TextBox txtLabel;
            string sHover;
            LegendBox legendBox;
            ChartDirector.TextBox tb, tb2;
            short ii;
            string[] aSt;
            string sTitle;
            DataTable dtPlot;
            PlotArea plotArea;
            // Dim gradColor() As Integer = {0, &HFFCCFF, 128, &HFFFFFF, 256, &HFFCCFF}
            var gradColor = new[] { 0, 0xCCCCFF, 128, 0xFFFFFF, 256, 0xCCCCFF };
            string sColName;
            int intWidth;
            for (int i = 1, loopTo = dtTable.Columns.Count - 1; i <= loopTo; i++)
            {
                // sColName = Replace(dtTable.Columns(i).ColumnName, "Count", "")
                sColName = dtTable.Columns[i].Caption.Replace("Count", ""); // this time we make use of caption
                sColName = sColName.Replace("Below7", "0-6~yrs of schooling.");
                sColName = sColName.Replace( "Above7", "7+~yrs of schooling.");
                sColName = sColName.Replace( "Worsened", "Worsened economic condition");
                sColName = sColName.Replace( "Improved", "Improved economic condition");
                sColName = sColName.Replace("_", " ");
                sColName = sColName.Replace(" ", "\n");
                sColName = sColName.Replace( "~", " ");
                Labels[i - 1] = sColName;
            }

            // Dim c As XYChart = New XYChart(250 + (dt.Columns.Count * 45), 520)
            intHeight = INTBASEHEIGHT + dtTable.Rows.Count * INTLEGENDHEIGHT;
            intWidth = 250 + dtTable.Columns.Count * 45;
            var c = new XYChart(intWidth, intHeight);
            c.setBackground(0xFFFFFF, 0xCCCCCC, 1);
            plotArea = c.setPlotArea(60, 80, 150 + dtTable.Columns.Count * 45, 230);
            plotArea.setBackground(c.gradientColor(gradColor, 0));
            legendBox = c.addLegend(30, 348, true, "arial.ttf", 9);
            legendBox.setBackground(Chart.Transparent, Chart.Transparent, 0);
            BarLayer layer;
            layer = c.addBarLayer2(Chart.Percentage);
            layer.set3D(5);
            // c.setWallpaper(Request.PhysicalApplicationPath & "images\graypaper.png")'awas berat >50k
            // aSt = sTemp.Split(" "c)
            // 'aSt = desc.Split(" "c)
            // For i As Integer = 0 To aSt.Length - 1
            // 'If (ii * 10) < (250 + (dtTable.Columns.Count * 30)) Then
            // If (ii * 10) < intWidth Then
            // ii += aSt(i).Length()
            // ii += 1
            // sT &= aSt(i) & " "
            // Else
            // sT &= Chr(10)
            // Exit For
            // End If
            // Next
            // sT &= Right(sTemp, (sTemp.Length + 1) - ii)
            // If sDist = "All" Then
            // If sRegn = "Natl" Then
            // sT &= Chr(10) & IIf(bEn, "National", "Nasional")
            // Else     'All districts in a region
            // sT &= Chr(10) & IIf(bEn, "(Province ", "Propinsi ") & oGg.ProvName & ")"
            // End If
            // Else   'single district
            // sT &= Chr(10) & "( " & oGg.KabuName & Chr(10) & IIf(bEn, "Province ", "Propinsi ") & oGg.ProvName & " )"
            // End If

            if (sDist == "All") // Province
            {
            }
            // sT &= Chr(10) & IIf(bEn, "(Province ", "Propinsi ") & oGg.ProvName & ")"
            else if (sDist == "Natl") // National
            {
            }
            // sT &= Chr(10) & IIf(bEn, "National", "Nasional")
            else
            {
                // sT &= Chr(10) & "( " & oGg.KabuName & Chr(10) & IIf(bEn, "Province ", "Propinsi ") & oGg.ProvName & " )"
            }   // single district

            string sTemp1 = commonModule.WrapString((bEn ? oGv.Desc_En : oGv.Desc), intWidth, 10);
            string sTemp2 = commonModule.WrapString((bEn? oGv.Var_Parent_Desc_En: oGv.Var_Parent_Desc), intWidth, 10);
            tb = c.addTitle(sTemp2 + (char)10 + sTemp1, "arialbd.ttf", 10, 0xFF);

            // tb.setBackground(c.gradientColor(New Integer() {0, &H5555EE, 30, &HDDDDFF, (tb.getHeight / 2), &HFFFFFF, tb.getHeight - 3, &HDDDDFF, tb.getHeight, &H5555EE}, 180, tb.getHeight), -1, 1)
            tb.setBackground(c.gradientColor(Chart.blueMetalGradient, 180, INTRATIO), -1, 0);
            tb.setFontColor(0x0);

            // tb2 = c.addText(0, c.getDrawArea.getHeight() - INTBOTTOMTEXTHEIGHT, "Indonesia - Governance and Decentralization Survey", "arial.ttf", 8, &HFFFF00, Chart.Center)'kuning
            tb2 = c.addText(0, c.getDrawArea().getHeight() - INTBOTTOMTEXTHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0x55, Chart.Center);
            tb2.setSize(250 + dtTable.Columns.Count * 45, INTBOTTOMTEXTHEIGHT);
            // tb2.setBackground(&H191970, -1, 1)
            tb2.setBackground(c.gradientColor(INTGRADBLEU, 0, INTBOTTOMTEXTHEIGHT / 256, 0, INTBOTTOMTEXTHEIGHT), -1, 1);
            // BEGIN OF transpose matrix
            dtPlot = new DataTable();
            for (int i = 0, loopTo1 = dtTable.Rows.Count - 1; i <= loopTo1; i++)
                dtPlot.Columns.Add(dtTable.Rows[i][0].ToString());
            DataRow drPlot;
            for (int i = 1, loopTo2 = dtTable.Columns.Count - 1; i <= loopTo2; i++)
            {
                drPlot = dtPlot.NewRow();
                for (int i2 = 0, loopTo3 = dtPlot.Columns.Count - 1; i2 <= loopTo3; i2++)
                    drPlot[i2] = dtTable.Rows[i2][i].ToString();
                dtPlot.Rows.Add(drPlot);
            }
            // END OF transpose matrix
            var table = new DBTable(dtPlot);
            for (int i = dtPlot.Columns.Count - 1; i >= 0; i += -1)
                layer.addDataSet(table.getCol(i), iColors[i], Server.HtmlDecode(dtTable.Rows[i][0].ToString()));
            layer.setBorderColor(-1, 1);
            layer.setDataLabelStyle().setAlignment(Chart.Center);
            layer.setDataLabelFormat((bEn? "{percent|1,.} %": "{percent|1.,} %")); // Format percentage with 1 decimal digit
            layer.setLegend(Chart.ReverseLegend);
            txtLabel = c.xAxis().setLabels(Labels);
            c.yAxis().setTitle((bEn? "Percentage": "Persentase"), "Arialbd.ttf", 10);
            // txtLabel.setFontAngle(45)
            // Dim WebChartViewer1 As New ChartDirector.WebChartViewer
            c.setAntiAlias(true, 1);
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            if (bIE)
            {
                // sHover = " TITLE='{xLabel} : " & vbCrLf & " - {dataSetName} " & vbCrLf & vbTab & "Count = {value}" & vbCrLf & vbTab & "Percent= {percent} %' "
                if (bEn)
                {
                    sHover = " onmouseover='ci(\"tip\",\"{dataSetName}<BR>&nbsp;&nbsp;&nbsp;Count = {value}<BR>&nbsp;&nbsp;&nbsp;Percent= {percent|1} % <BR>&nbsp;&nbsp;&nbsp;Total= {totalValue} \");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";   // {percent|1} means 1 decimal digit
                }
                else
                {
                    sHover = " onmouseover='ci(\"tip\",\"{dataSetName}<BR>&nbsp;&nbsp;&nbsp;Jumlah = {value}<BR>&nbsp;&nbsp;&nbsp;Persentase= {percent|1} % <BR>&nbsp;&nbsp;&nbsp;Total= {totalValue} \");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";
                }   // {percent|1} means 1 decimal digit

                WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
            }
            // Me.PlaceHolder1.Controls.Add(WebChartViewer1)
        }

        void CreateChartCatSumm(DataTable dtTable)
        {
            const int BASEHEIGHT = 230;
            const int BASEWIDTH = 250;
            const int FOOTERHEIGHT = 20;
            const int FONTHEIGHT = 14;
            var Labels = new string[dtTable.Columns.Count];
            ChartDirector.TextBox txtLabel;
            string sHover;
            LegendBox legendBox;
            ChartDirector.TextBox tb, tb2;
            short ii;
            string[] aSt;
            string sT;
            DataTable dtPlot;
            PlotArea plotArea;
            var gradColor = new[] { 0, 0xCCCCFF, 128, 0xFFFFFF, 256, 0xCCCCFF };
            // Dim gradColor() As Integer = {0, &HFFCCFF, 128, &HFFFFFF, 256, &HFFCCFF}
            string sColName;
            var dtPercent = new DataTable();
            dtPercent = dtTable.Clone();
            for (int i = 1, loopTo = dtPercent.Columns.Count - 1; i <= loopTo; i++)
                dtPercent.Columns[i].DataType = typeof(float);
            for (int iRow = 0, loopTo1 = dtTable.Rows.Count - 1; iRow <= loopTo1; iRow++)
            {
                DataRow dr = dtPercent.NewRow();
                for (int iCol = 0, loopTo2 = dtTable.Columns.Count - 1; iCol <= loopTo2; iCol++)
                {
                    if (iCol > 0)
                    {
                        int iTotal = Convert.ToInt32(dtTable.Compute("SUM ([" + dtTable.Columns[iCol].ColumnName + "])", ""));
                        if (iTotal > 0)
                        {
                            dr[iCol] = Convert.ToInt32(dtTable.Rows[iRow][iCol]) / iTotal;
                        }
                        else
                        {
                            dr[iCol] = 0;
                        }
                    }
                    else
                    {
                        dr[iCol] = dtTable.Rows[iRow][iCol];
                    }
                }

                dtPercent.Rows.Add(dr);
            }

            for (int i = 1, loopTo3 = dtTable.Columns.Count - 1; i <= loopTo3; i++)
            {
                if (dtTable.Columns[i].ColumnName.IndexOf("cap") == 5) // kalau ada nama kolom berawalan "cap", since the query for the comparator results in "Countcap1Jawa", etc
                {
                    sColName = dtTable.Columns[i].ColumnName.Remove(5, 5); // removes "cap<2 digits number> (see comp_id field in the GDS2_11_Comp)"
                }
                else
                {
                    sColName = dtTable.Columns[i].ColumnName;
                }

                sColName = sColName.Replace("Count", "").Replace(" ", "\n");
                Labels[i - 1] = sColName;
            }

            int w = 100 + (dtTable.Columns.Count - 1) * dtTable.Rows.Count * 20;
            int h = dtTable.Rows.Count * FONTHEIGHT + 420;
            var c = new XYChart(w, h);
            c.setBackground(0xFFFFFF, 0xCCCCCC, 1);
            plotArea = c.setPlotArea(60, 80, 10 + (dtTable.Columns.Count - 1) * dtTable.Rows.Count * 20, BASEHEIGHT);
            plotArea.setBackground(c.gradientColor(gradColor, 0));
            legendBox = c.addLegend(30, 354, true, "arial.ttf", 9);
            legendBox.setBackground(Chart.Transparent, Chart.Transparent, 0);
            BarLayer layer;
            // layer = c.addBarLayer2(Chart.Side, dtTable.Columns.Count - 1)
            layer = c.addBarLayer2(Chart.Side, 1);
            layer.setBarGap(0.05, 0);   // 0.05%  in-between distance
            // layer.setBarGap(Chart.TouchBar)
            // layer.setBarWidth((dtTable.Rows.Count * 20) + 10, 20)

            sT = bEn ? oGv.Desc_En : oGv.Desc;
            sT = commonModule.FormatByLength(sT, 60);
            sT = sT.Replace("<B>", "").Replace("</B>", "");
            tb = c.addTitle(sT, "arialbd.ttf", 10, 0xFF);
            tb.setBackground(c.gradientColor(Chart.blueMetalGradient), -1, 1);
            tb.setFontColor(0x0);
            tb2 = c.addText(0, h - FOOTERHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0xFFFF00, Chart.Center);
            tb2.setSize(w, FOOTERHEIGHT);
            tb2.setBackground(0x191970, -1, 1);
            // BEGIN OF transpose matrix
            dtPlot = new DataTable();
            for (int i = 0, loopTo4 = dtTable.Rows.Count - 2; i <= loopTo4; i++)  // exclude the 'Total Respondents' row since it's only the total
                dtPlot.Columns.Add(dtTable.Rows[i][0].ToString());
            DataRow drPlot;
            for (int i = 1, loopTo5 = dtTable.Columns.Count - 1; i <= loopTo5; i++)
            {
                drPlot = dtPlot.NewRow();
                for (int i2 = 0, loopTo6 = dtPlot.Columns.Count - 1; i2 <= loopTo6; i2++)
                    drPlot[i2] = dtPercent.Rows[i2][i];
                dtPlot.Rows.Add(drPlot);
            }
            // END OF transpose matrix
            var table = new DBTable(dtPlot);
            for (int i = dtPlot.Columns.Count - 1; i >= 0; i += -1)
                layer.addDataSet(table.getCol(i), iColors[i], dtTable.Rows[i][0].ToString());
            layer.setBorderColor(-1, 1);
            layer.setDataLabelStyle().setFontAngle(90);
            layer.setDataLabelFormat("{percent|1} %");   // Format percentage with 1 decimal digit
            // layer.setLegend(Chart.ReverseLegend)
            txtLabel = c.xAxis().setLabels(Labels);
            if (bEn)
            {
                c.yAxis().setTitle("Percentage", "Arialbd.ttf", 10);
            }
            else
            {
                c.yAxis().setTitle("Persentase", "Arialbd.ttf", 10);
            }

            c.yAxis().setLabelFormat("{={value}*100} %");
            // txtLabel.setFontAngle(45)
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            // sHover = " TITLE='{xLabel} : " & vbCrLf & " - {dataSetName} " & vbCrLf & vbTab & "Count = {value}" & vbCrLf & vbTab & "Percent= {percent} %' "
            if (bEn)
            {
                sHover = " onmouseover='ci(\"tip\",\"{dataSetName}<BR>&nbsp;&nbsp;&nbsp;Percentage = {percent|1}%\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";   // {percent|1} means 1 decimal digit
            }
            else
            {
                sHover = " onmouseover='ci(\"tip\",\"{dataSetName}<BR>&nbsp;&nbsp;&nbsp;Persentase = {percent|1}%\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";
            }   // {percent|1} means 1 decimal digit

            WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
        }

        void CreateChartCatSumm2(DataTable dtTable, DataTable dtComp)
        {
            const int BASEHEIGHT = 230;
            const int BASEWIDTH = 250;
            const int FOOTERHEIGHT = 20;
            const int FONTHEIGHT = 14;
            var Labels = new string[dtTable.Columns.Count];
            ChartDirector.TextBox txtLabel;
            string sHover;
            LegendBox legendBox;
            ChartDirector.TextBox tb, tb2;
            short ii;
            string[] aSt;
            string sT;
            DataTable dtPlot;
            PlotArea plotArea;
            var gradColor = new[] { 0, 0xCCCCFF, 128, 0xFFFFFF, 256, 0xCCCCFF };
            // Dim gradColor() As Integer = {0, &HFFCCFF, 128, &HFFFFFF, 256, &HFFCCFF}
            string sColName;
            var dtPercent = new DataTable();
            dtPercent = dtTable.Clone();
            for (int i = 1, loopTo = dtPercent.Columns.Count - 1; i <= loopTo; i++)
                dtPercent.Columns[i].DataType = typeof(float);
            for (int iRow = 0, loopTo1 = dtTable.Rows.Count - 1; iRow <= loopTo1; iRow++)
            {
                DataRow dr = dtPercent.NewRow();
                for (int iCol = 0, loopTo2 = dtTable.Columns.Count - 1; iCol <= loopTo2; iCol++)
                {
                    if (iCol > 0)
                    {
                        int iTotal = Convert.ToInt32(dtTable.Compute("SUM ([" + dtTable.Columns[iCol].ColumnName + "])", ""));
                        if (iTotal > 0)
                        {
                            dr[iCol] = Convert.ToInt32(dtTable.Rows[iRow][iCol]) / iTotal;
                        }
                        else
                        {
                            dr[iCol] = 0;
                        }
                    }
                    else
                    {
                        dr[iCol] = dtTable.Rows[iRow][iCol];
                    }
                }

                dtPercent.Rows.Add(dr);
            }

            for (int i = 1, loopTo3 = dtTable.Columns.Count - 1; i <= loopTo3; i++)
            {
                if (dtComp.Columns[i].ColumnName.IndexOf("cap") == 5) // kalau ada nama kolom berawalan "cap", since the query for the comparator results in "Countcap1Jawa", etc
                {
                    sColName = dtComp.Columns[i].ColumnName.Remove(5, 5); // removes "cap<2 digits number> (see comp_id field in the GDS2_11_Comp)"
                }
                else
                {
                    sColName = dtComp.Columns[i].ColumnName;
                }

                sColName = sColName.Replace("Count", "").Replace(" ", "\n");
                Labels[i - 1] = sColName;
            }

            int w = 100 + (dtTable.Columns.Count - 1) * dtTable.Rows.Count * 20;
            int h = dtTable.Rows.Count * FONTHEIGHT + 420;
            var c = new XYChart(w, h);
            c.setBackground(0xFFFFFF, 0xCCCCCC, 1);
            plotArea = c.setPlotArea(60, 80, 10 + (dtTable.Columns.Count - 1) * dtTable.Rows.Count * 20, BASEHEIGHT);
            plotArea.setBackground(c.gradientColor(gradColor, 0));
            legendBox = c.addLegend(30, 354, true, "arial.ttf", 9);
            legendBox.setBackground(Chart.Transparent, Chart.Transparent, 0);
            BarLayer layer;
            // layer = c.addBarLayer2(Chart.Side, dtTable.Columns.Count - 1)
            layer = c.addBarLayer2(Chart.Side, 1);
            layer.setBarGap(0.05, 0);   // 0.05%  in-between distance
            // layer.setBarGap(Chart.TouchBar)
            // layer.setBarWidth((dtTable.Rows.Count * 20) + 10, 20)

            sT = bEn? oGv.Desc_En: oGv.Desc;
            sT = commonModule.FormatByLength(sT, 60);
            sT = sT.Replace("<B>", "").Replace("</B>", "");
            tb = c.addTitle(sT, "arialbd.ttf", 10, 0xFF);
            tb.setBackground(c.gradientColor(Chart.blueMetalGradient), -1, 1);
            tb.setFontColor(0x0);
            tb2 = c.addText(0, h - FOOTERHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0xFFFF00, Chart.Center);
            tb2.setSize(w, FOOTERHEIGHT);
            tb2.setBackground(0x191970, -1, 1);
            // BEGIN OF transpose matrix
            dtPlot = new DataTable();
            for (int i = 0, loopTo4 = dtTable.Rows.Count - 2; i <= loopTo4; i++)  // exclude the 'Total Respondents' row since it's only the total
                dtPlot.Columns.Add(dtTable.Rows[i][0].ToString());
            DataRow drPlot;
            for (int i = 1, loopTo5 = dtTable.Columns.Count - 1; i <= loopTo5; i++)
            {
                drPlot = dtPlot.NewRow();
                for (int i2 = 0, loopTo6 = dtPlot.Columns.Count - 1; i2 <= loopTo6; i2++)
                    drPlot[i2] = dtPercent.Rows[i2][i];
                dtPlot.Rows.Add(drPlot);
            }
            // END OF transpose matrix
            var table = new DBTable(dtPlot);
            for (int i = dtPlot.Columns.Count - 1; i >= 0; i += -1)
                layer.addDataSet(table.getCol(i), iColors[i], dtTable.Rows[i][0].ToString());
            layer.setBorderColor(-1, 1);
            layer.setDataLabelStyle().setFontAngle(90);
            layer.setDataLabelFormat("{percent|1} %");   // Format percentage with 1 decimal digit
            // layer.setLegend(Chart.ReverseLegend)
            txtLabel = c.xAxis().setLabels(Labels);
            if (bEn)
            {
                c.yAxis().setTitle("Percentage", "Arialbd.ttf", 10);
            }
            else
            {
                c.yAxis().setTitle("Persentase", "Arialbd.ttf", 10);
            }

            c.yAxis().setLabelFormat("{={value}*100} %");
            // txtLabel.setFontAngle(45)
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            // sHover = " TITLE='{xLabel} : " & vbCrLf & " - {dataSetName} " & vbCrLf & vbTab & "Count = {value}" & vbCrLf & vbTab & "Percent= {percent} %' "
            if (bEn)
            {
                sHover = " onmouseover='ci(\"tip\",\"{dataSetName}<BR>&nbsp;&nbsp;&nbsp;Percentage = {percent|1}%\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";   // {percent|1} means 1 decimal digit
            }
            else
            {
                sHover = " onmouseover='ci(\"tip\",\"{dataSetName}<BR>&nbsp;&nbsp;&nbsp;Persentase = {percent|1}%\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";
            }   // {percent|1} means 1 decimal digit

            WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
        }


        void CreateChartCont(DataTable dtTable)
        {
            ChartDirector.TextBox tb, tb2;
            short ii;
            string[] aSt;
            string sT, sHover;
            short iRow;
            double dWhiskerMax, dWhiskerMin;
            double dBoxMax, sBoxMin;
            int intWidth;
            iRow = Convert.ToInt16(dtTable.Rows.Count - 1);
            var labels = new string[iRow + 1];
            var Q0Data = new double[iRow + 1];
            var Q1Data = new double[iRow + 1];
            var Q2Data = new double[iRow + 1];
            var Q3Data = new double[iRow + 1];
            var Q4Data = new double[iRow + 1];
            var ArrayList1 = new ArrayList();
            for (int i = 0, loopTo = dtTable.Rows.Count - 1; i <= loopTo; i++)
            {
                // 'Aslinya
                labels[i] = dtTable.Rows[i]["District"].ToString();
                Q0Data[i] = Convert.ToDouble(dtTable.Rows[i]["Mean"]);
                Q1Data[i] = Convert.ToDouble(dtTable.Rows[i]["CIl"]);
                Q2Data[i] = Convert.ToDouble(dtTable.Rows[i]["CIh"]);
                Q3Data[i] = Convert.ToDouble(dtTable.Rows[i]["minimum"]);
                Q4Data[i] = Convert.ToDouble(dtTable.Rows[i]["maximum"]);
                ArrayList1.Add(Q0Data[i]); // Mean
                ArrayList1.Add(Q1Data[i]); // CI-
                ArrayList1.Add(Q2Data[i]); // CI+
                ArrayList1.Add(Q3Data[i]); // Min
                ArrayList1.Add(Q4Data[i]); // Max
            }

            ArrayList1.Sort();
            dWhiskerMin = Convert.ToDouble(ArrayList1[0]);
            dWhiskerMax = Convert.ToDouble(ArrayList1[ArrayList1.Count - 1]);
            intWidth = 280 + dtTable.Rows.Count * 30;
            var c = new XYChart(intWidth, 470);
            c.setBackground(0xFFFFFF, 0xCCCCCC, 1);
            // horizontal and vertical grids by setting their colors to grey (0xc0c0c0)
            PlotArea plotArea;
            plotArea = c.setPlotArea(95, 85, 150 + dtTable.Rows.Count * 30, 250);
            plotArea.setGridColor(0xC0C0C0, 0xC0C0C0);
            // Dim gradColor() As Integer = {0, &HAAAAFF, 128, &HFFFFFF, 256, &HAAAAFF}
            var gradColor = new[] { 0, 0xCCCCFF, 128, 0xFFFFFF, 256, 0xCCCCFF };
            // plotArea.setBackground(c.gradientColor(70, 0, 320, 0, &HAAAAFF, &HFFFFFF))
            plotArea.setBackground(c.gradientColor(gradColor, 0));
            // c.addTitle(gdsvardesc, "arialb.ttf", 10).setBackground(c.gradientColor(Chart.blueMetalGradient))
            c.xAxis().setLabels(labels).setFontStyle("arialbd.ttf");
            // c.yAxis().setLogScale3(      )
            c.xAxis().setLabels(labels).setFontAngle(45);
            c.yAxis().setLabelStyle("arialb.ttf");
            c.yAxis().setLabelFormat(bEn ? "{value|1,.}" : "{value|1.,}");

            // aSt = desc.Split(" "c)
            // For i As Integer = 0 To aSt.Length - 1
            // If (ii * 10) < (350) Then
            // ii += aSt(i).Length()
            // ii += 1
            // sT &= aSt(i) & " "
            // Else
            // sT &= Chr(10)
            // Exit For
            // 'Else
            // 'TODO
            // 'masih salah kalo s.Length-title.length
            // '	sT &= " " & s(i)
            // End If
            // Next
            // sT &= Right(desc, (desc.Length + 1) - ii)
            if (sDist == "All")
            {
                if (sRegn == "Natl")
                {
                }
                // sT &= Chr(10) & IIf(bEn, "( National )", "( Nasional )")
                else
                {
                    // sT &= Chr(10) & IIf(bEn, "Province ", "Propinsi ") & oGg.ProvName
                }     // All districts in a region
            }
            else
            {
                // sT &= Chr(10) & IIf(bEn, "District ", " ") & oGg.KabuName & " )"
            }   // single district

            string sTemp1 =commonModule. WrapString(bEn? oGv.Desc_En: oGv.Desc, intWidth, 10);
            string sTemp2 = commonModule.WrapString(bEn? oGv.Var_Parent_Desc_En: oGv.Var_Parent_Desc, intWidth, 10);
            // Dim sTitle As String = WrapString(sTemp1 & "(" & sTemp2 & ")", intWidth, 10)
            // Dim sTitle As String = WrapString(sTemp1, intWidth, 10)
            tb = c.addTitle(sTemp2 + (char)10 + sTemp1, "arialbd.ttf", 10, 0xFF);
            tb.setBackground(c.gradientColor(Chart.blueMetalGradient, 180, INTRATIO), -1, 0);
            tb.setFontColor(0x0);
            tb2 = c.addText(0, c.getDrawArea().getHeight() - INTBOTTOMTEXTHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0x55, Chart.Center);
            tb2.setSize(intWidth, INTBOTTOMTEXTHEIGHT);
            tb2.setBackground(c.gradientColor(INTGRADBLEU, 0, INTBOTTOMTEXTHEIGHT / 256, 0, INTBOTTOMTEXTHEIGHT), -1, 1);
            // tb2.setBackground(&H191970, -1, 1)

            // Q0Data = "Mean"
            // Q1Data = "CIl"
            // Q2Data = "CIh"
            // Q3Data = "minimum"
            // Q4Data = "maximum"

            // boxTop, boxBottom, maxData, minData, midData
            // c.addBoxWhiskerLayer(Q2Data, Q1Data, Q4Data, Q3Data, Q0Data, &HFFAAFF, &HFF0000).setLineWidth(2)

            // c.yAxis.setLinearScale(dWhiskerMin, dWhiskerMax)
            c.yAxis().setLogScale(dWhiskerMin, dWhiskerMax, Chart.NoValue);
            ChartDirector.BoxWhiskerLayer bwLayer;
            // c.addBoxWhiskerLayer(Q2Data, Q1Data, Q4Data, Q3Data, Q0Data, &HFFAAFF, &HFF0000).setLineWidth(2)
            bwLayer = c.addBoxWhiskerLayer(Q2Data, Q1Data, Q4Data, Q3Data, Q0Data, c.gradientColor2(INTGRADBLEU2, 0, 0.2), 0xAA);
            bwLayer.setLineWidth(2);
            // c.addBoxWhiskerLayer(Q2Data, Q1Data, Q2Data, Q1Data, Q0Data, &HFFAAFF, &HFF0000).setLineWidth(2)

            // c.addBoxWhiskerLayer(Q2Data, Q1Data, Q4Data, Q3Data, Q0Data, &HFF000000, &HFF000000).setLineWidth(2)
            // c.addBoxWhiskerLayer(Q2Data, Q1Data, Q2Data, Q1Data, Q0Data, &HFFAAFF, &HFF0000).setLineWidth(2)

            // c.yAxis.setLinearScale()
            c.setAntiAlias(true, 1);
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            if (bIE)
            {
                // sHover = "TITLE='{xLabel}: " & Chr(10) & _
                // vbTab & "CI(-) = {min} " & Chr(10) & _
                // vbTab & "CI(+) = {max}" & Chr(10) & _
                // vbTab & "Mean = {top}" & Chr(10) & _
                // vbTab & "Std. Dev = {med}'"
                // sHover = " onmouseover=""ci('{xLabel}:<BR>&nbsp;&nbsp;CI(-) = {min|1}<BR>&nbsp;&nbsp;CI(+) = {max|1}<BR>&nbsp;&nbsp;Mean = {top|1}<BR>&nbsp;&nbsp;Std. Dev = {med|1}');"" onmousemove=""cm();"" onmouseout=""ch();""  "

                // sHover = " onmouseover='ci(""tip"",""{xLabel}:<BR>&nbsp;&nbsp;Mean = {med|1}<BR>&nbsp;&nbsp;Max. = {max|1}<BR>&nbsp;&nbsp;Min. = {min|1}<BR>&nbsp;&nbsp;CI+ = {top|1}<BR>&nbsp;&nbsp;CI- = {bottom|1}"");' onmousemove='cm(""tip"");' onmouseout='ch(""tip"");'  "
                sHover = " onmouseover='ci(\"tip\",\"{xLabel}:<BR>&nbsp;&nbsp;Mean = {med|1,.}<BR>&nbsp;&nbsp;Max. = {max|1,.}<BR>&nbsp;&nbsp;Min. = {min|1,.}<BR>&nbsp;&nbsp;CI+ = {top|1,.}<BR>&nbsp;&nbsp;CI- = {bottom|1,.}\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";
                WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
            }
        }

        void CreateChartContBar(DataTable dtTable)
        {
            // Dim dWhiskerMax, dWhiskerMin As Double
            ChartDirector.TextBox tb, tb2;
            short ii;
            string[] aSt;
            string sT, sHover;
            short iRow;
            ChartDirector.Layer BarLayer;
            int intWidth;
            iRow = Convert.ToInt16(dtTable.Rows.Count - 1);
            var labels = new string[iRow + 1];
            var Q0Data = new double[iRow + 1];
            for (int i = 0, loopTo = dtTable.Rows.Count - 1; i <= loopTo; i++)
            {
                labels[i] = dtTable.Rows[i]["District"].ToString();
                Q0Data[i] = Convert.ToDouble(dtTable.Rows[i]["Mean"]);
            }

            intWidth = 280 + dtTable.Rows.Count * 30;
            var c = new XYChart(intWidth, 470);
            c.setBackground(0xFFFFFF, 0xCCCCCC, 1);
            PlotArea plotArea;
            plotArea = c.setPlotArea(95, 80, 150 + dtTable.Rows.Count * 30, 230);
            // plotArea = c.setPlotArea(80, 45, 250, 250)
            plotArea.setGridColor(0xC0C0C0, 0xC0C0C0);
            var gradColor = new[] { 0, 0xAAAAFF, 128, 0xFFFFFF, 256, 0xAAAAFF };
            plotArea.setBackground(c.gradientColor(gradColor, 0));
            BarLayer = c.addBarLayer(Q0Data, iColors);
            BarLayer.setBorderColor(-1, 1);
            if (Convert.ToInt32(dtTable.Compute("AVG(Mean)", "")) < 100000)
            {
                BarLayer.setDataLabelFormat(bEn? "{value|1,.}": "{value|1.,}");     // Format percentage with 1 decimal digit
                BarLayer.setDataLabelStyle().setAlignment(Chart.Center);
                BarLayer.setDataLabelStyle().setFontAngle(90);
            }

            c.xAxis().setLabels(labels).setFontStyle("arialb.ttf");
            c.xAxis().setLabels(labels).setFontAngle(45);

            // c.yAxis().setLabelStyle("arialbd.ttf")
            c.yAxis().setLabelFormat(bEn? "{value|1,.}": "{value|1.,}");
            c.yAxis().setTitle("Mean", "Arialbd.ttf", 10, 0x0);

            // aSt = desc.Split(" "c)
            // For i As Integer = 0 To aSt.Length - 1
            // If (ii * 10) < (350) Then
            // ii += aSt(i).Length()
            // ii += 1
            // sT &= aSt(i) & " "
            // Else
            // sT &= Chr(10)
            // Exit For
            // 'Else
            // 'TODO
            // 'masih salah kalo s.Length-title.length
            // '	sT &= " " & s(i)
            // End If
            // Next
            // sT &= Right(desc, (desc.Length + 1) - ii)
            // If sDist = "All" Then
            // If sRegn = "Natl" Then
            // sT &= Chr(10) & IIf(bEn, "National", "Nasional")
            // Else     'All districts in a region
            // sT &= Chr(10) & IIf(bEn, "Province ", "Propinsi ") & oGg.ProvName
            // End If
            // Else   'single district
            // sT &= Chr(10) & "(" & oGg.KabuName & Chr(10) & IIf(bEn, "Province ", "Propinsi ") & oGg.ProvName & ")"
            // End If
            string sTemp1 = commonModule.WrapString(bEn? oGv.Desc_En: oGv.Desc, intWidth, 10);
            string sTemp2 = commonModule.WrapString(bEn ? oGv.Var_Parent_Desc_En : oGv.Var_Parent_Desc, intWidth, 10);
            // Dim sTitle As String = WrapString(sTemp1 & "(" & sTemp2 & ")", intWidth, 10)
            tb = c.addTitle(sTemp2 + ((char)10 + sTemp1), "arialbd.ttf", 10, 0xFF);

            // tb.setBackground(c.gradientColor(Chart.blueMetalGradient), -1, 1)
            tb.setBackground(c.gradientColor(Chart.blueMetalGradient, 180, INTRATIO), -1, 0);
            tb.setFontColor(0x0);
            tb2 = c.addText(0, c.getDrawArea().getHeight() - INTBOTTOMTEXTHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0x55, Chart.Center);
            tb2.setSize(280 + dtTable.Rows.Count * 30, INTBOTTOMTEXTHEIGHT);
            tb2.setBackground(c.gradientColor(INTGRADBLEU, 0, INTBOTTOMTEXTHEIGHT / 256, 0, INTBOTTOMTEXTHEIGHT), -1, 1);
            // tb2.setBackground(&H191970, -1, 1)

            WebChartViewer2.Visible = true;
            c.yAxis().setAutoScale();
            c.setAntiAlias(true, 1);
            WebChartViewer2.Image = c.makeWebImage(Chart.PNG);
            if (bIE)
            {
                if (bEn)
                {
                    sHover = " onmouseover=\"ci('tip','{xLabel}:<BR>&nbsp;&nbsp;Mean = {value|1,.}');\" onmousemove=\"cm('tip');\" onmouseout=\"ch('tip');\"  ";
                    WebChartViewer2.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
                }
                else
                {
                    sHover = " onmouseover=\"ci('tip','{xLabel}:<BR>&nbsp;&nbsp;Nilai Rata-rata = {value|1.,}');\" onmousemove=\"cm('tip');\" onmouseout=\"ch('tip');\"  ";
                    WebChartViewer2.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
                }
            }
            // dWhiskerMax = c.yAxis.getMaxValue()
            // dWhiskerMin = 0
        }

        string DrawTable(DataTable dtTable, string cssClass)
        {
            short iRow = default(short), iCol;
            StringBuilder s;
            string sColName;
            // Dim colors() As String = {"#AAFFAA", "#CCCCFF", "#FFFFAA", "#FFCC66", "#00FFFF", "#FF99FF", "#DDFF55", "#55FFAA", "#FF3355", "#Ff5500"}

            // BEGIN OF Heading Top ---------------
            s = new StringBuilder("<TABLE BORDER=\"0\" CELLPADDING=\"3\" CELLSPACING=\"1\" CLASS=\"" + cssClass + "\" STYLE=\" border: 2px outset lightgrey; \" >");
            s.Append("<TR ALIGN=\"center\">");
            s.AppendFormat("<TD COLSPAN=\"{0}\">", dtTable.Columns.Count * 2 - 1);
            s.AppendFormat("<DIV CLASS=\"H5\">{0}</DIV>", var_parent_desc);
            s.Append(desc);
            s.AppendFormat("</TD>");
            s.Append("</TR>");
            // END OF Heading Top ---------------

            // BEGIN OF Heading Comparators  ---------------
            s.Append("<TR ALIGN=\"center\" CLASS=\"H5\">");
            // Try
            for (int i = 0, loopTo = dtTable.Columns.Count - 1; i <= loopTo; i++)
            {
                if (i == 0)
                {
                    // s &= "<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">Category</TD>"
                    // If oGv.IsAdvance Then
                    // .Append("<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">&nbsp;</TD>") 'Header Jumlah/% not use here
                    // Else
                    s.Append("<TD BGCOLOR=\"#FFEEFF\" ROWSPAN=\"3\">&nbsp;</TD>");
                }
                // End If
                else
                {
                    // If iRow Mod 2 = 0 Then
                    if (i <= sComp.Length / 2)
                    {
                        string sComp1 = sComp[0, i - 1];
                        if (!(sDist == "All Districts" & sRegn == "National"))
                        {
                            var switchExpr = sComp1;
                            switch (switchExpr)
                            {
                                case "District":
                                case "Province":
                                case "&nbsp;":
                                    {
                                        sComp1 = sComp1;
                                        break;
                                    }

                                case "Java/Off-Java":
                                    {
                                        sComp1 = sComp1;         // & "<SUP>++</SUP>"'uncomment this Kalo ternyata comparator nya per dist/regn
                                        break;
                                    }

                                default:
                                    {
                                        sComp1 = sComp1;
                                        break;
                                    }
                                // sComp1 = sComp1 & "<SUP>+</SUP>"   'uncomment this Kalo ternyata comparator nya per dist/regn
                            }
                        }
                        // red-brick
                        // .Append("<TD COLSPAN=""" & sComp(1, i - 1) * 2 & """ BGCOLOR=""#FFCCCC"">" & sComp1 & "</TD>")
                        // lightblue

                        // If oGv.IsAdvance Then
                        // .Append("<TD COLSPAN=""" & sComp(1, i - 1) * 1 & """ BGCOLOR=""#E6E6FA"" class=""H11vb"">" & sComp1 & "</TD>")
                        // Else
                        s.Append("<TD COLSPAN=\"" + (Convert.ToInt32(sComp[1, i - 1]) * 2) + "\" BGCOLOR=\"#E6E6FA\" class=\"H11vb\">" + sComp1 + "</TD>");
                        // End If
                    }
                    // Else
                    // .Append("<TD COLSPAN=""2"" BGCOLOR=""#CCCCFF"">" & Replace(Replace(dtTable.Columns(i).ColumnName, "Count", ""), "_", " ") & "</TD>")
                    // End If
                }

                iRow += 1;
            }
            // Catch
            // End Try
            s.Append("</TR>");
            // END OF Heading Comparators ---------------

            // BEGIN OF Heading 2 ---------------
            s.Append("<TR ALIGN=\"center\">");
            for (int i = 0, loopTo1 = dtTable.Columns.Count - 1; i <= loopTo1; i++)
            {
                if (i > 0)
                {
                    // s &= "<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">Category</TD>"
                    // .Append("<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">&nbsp;</TD>")
                    // Else	'comm out
                    sColName = dtTable.Columns[i].Caption.Replace("Count", "");
                    sColName = sColName.Replace("Below7", "0-6");
                    sColName = sColName.Replace( "Above7", "7+");
                    sColName = sColName.Replace( "Not_Participate", "No");
                    sColName = sColName.Replace( "Participate", "Yes");
                    sColName = sColName.Replace( "Has_Civil_Servants", "Yes");
                    sColName = sColName.Replace( "No_Civil_Servants", "No");
                    sColName = sColName.Replace( "Has_Links", "Yes");
                    sColName = sColName.Replace( "No_Links", "No");
                    sColName = sColName.Replace( "Not_Aware_Of_Voice", "No");
                    sColName = sColName.Replace( "Aware_Of_Voice", "Yes");
                    sColName = sColName.Replace("_", " ");

                    // Warna selang-seling
                    // If iRow Mod 2 = 0 Then
                    // .Append("<TD COLSPAN=""2"" BGCOLOR=""#FFCCCC"">" & sColName & "</TD>")
                    // Else
                    // .Append("<TD COLSPAN=""2"" BGCOLOR=""#CCCCFF"">" & sColName & "</TD>")
                    // End If

                    // If oGv.IsAdvance Then
                    // .Append("<TD BGCOLOR=""#E6E6FA"" class=""H11b"">" & sColName & "</TD>")
                    // Else
                    s.Append("<TD COLSPAN=\"2\" BGCOLOR=\"#E6E6FA\"class=\"H11b\">" + sColName + "</TD>");
                    // End If
                }

                iRow += 1;
            }

            s.Append("</TR>");
            // END OF Heading 3 ---------------


            // BEGIN OF Heading 3 (Count Percent Header)---------------
            s.Append("<TR ALIGN=\"center\" CLASS=\"H9\" STYLE=\"border-bottom:black 1px solid;\"> ");
            for (int i = 0, loopTo2 = dtTable.Columns.Count - 1; i <= loopTo2; i++)
            {
                if (i > 0)
                {
                    // If Not oGv.IsAdvance Then
                    if (bEn)
                    {
                        s.Append("<TD BGCOLOR=\"#EEEEEE\">Count</TD><TD BGCOLOR=\"#DDDDDD\"><B>%</B></TD>");
                    }
                    else
                    {
                        s.Append("<TD BGCOLOR=\"#EEEEEE\">Jumlah</TD><TD BGCOLOR=\"#DDDDDD\"><B>%</B></TD>");
                    }
                    // End If
                }
            }

            s.Append("</TR>");
            // END OF Heading 3 ---------------
            iRow = 0;
            for (int i = 0, loopTo3 = dtTable.Rows.Count - 1; i <= loopTo3; i++)
            {
                s.Append("<TR ALIGN=\"right\" VALIGN=\"top\"> ");
                iRow += 1;
                for (int i2 = 0, loopTo4 = dtTable.Columns.Count - 1; i2 <= loopTo4; i2++)
                {
                    if (i2 == 0)
                    {
                        if (dtTable.Rows[i].IsNull(i2))
                        {
                            s.Append("<TD BGCOLOR=\"" + colors[i] + "\">&nbsp;</TD>");
                        }
                        else
                        {
                            s.Append(("<TD NOWRAP BGCOLOR=\"" + colors[i] + "\">") + dtTable.Rows[i][i2].ToString() + "</TD>");
                        }
                    }
                    // If iRow Mod 2 = 0 Then
                    // If dtTable.Rows(i).IsNull(i2) Then
                    // s &= "<TD BGCOLOR=""#FFEEFF"">&nbsp;</TD>"
                    // Else
                    // s &= "<TD BGCOLOR=""#FFEEFF"">" & dtTable.Rows(i)(i2) & "</TD>"
                    // End If
                    // Else
                    // If dtTable.Rows(i).IsNull(i2) Then
                    // s &= "<TD>&nbsp;</TD>"
                    // Else
                    // s &= "<TD>" & dtTable.Rows(i)(i2) & "</TD>"
                    // End If
                    // End If
                    else
                    {
                        if (iRow % 2 == 0)
                        {
                            if (dtTable.Rows[i].IsNull(i2))
                            {
                                s.Append("<TD BGCOLOR=\"#FFEEFF\">0</TD>");
                                s.Append("<TD BGCOLOR=\"#FFEEFF\">0</TD>");
                            }
                            else
                            {
                                s.Append("<TD BGCOLOR=\"#FFEEFF\">" + dtTable.Rows[i][i2].ToString() + "</TD>");
                                var switchExpr1 = contflg;
                                switch (switchExpr1)
                                {
                                    case 0:
                                        {
                                            // If Not oGv.IsAdvance Then
                                            s.Append("<TD BGCOLOR=\"#FFEEFF\">" + (Convert.ToDouble(dtTable.Rows[i][i2]) / Convert.ToDouble(dtTable.Compute("SUM ([" + dtTable.Columns[i2].ColumnName + "])", ""))).ToString("#,##0.0%") + "</TD>");           // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                    // End If
                                    // .Append("<TD BGCOLOR=""#FFEEFF"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "#,##0.0%") & "</TD>")           'Percentage Format with 1 decimal digits
                                    // .Append("<TD BGCOLOR=""#FFEEFF"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "p") & "</TD>")		'Percentage Format with 2 decimel digits
                                    case 4:
                                        {
                                            s.Append("<TD BGCOLOR=\"#FFEEFF\">" + (Convert.ToDouble( dtTable.Rows[i][i2] )/Convert.ToDouble( dtTable.Rows[dtTable.Rows.Count - 1][i2])).ToString( "#,##0.0%") + "</TD>");           // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                }
                            }
                        }
                        else
                        {
                            if (dtTable.Rows[i].IsNull(i2))
                            {
                                s.Append("<TD>0</TD>");
                                s.Append("<TD BGCOLOR=\"#EEEEEE\">0</TD>");
                            }
                            else
                            {
                                s.Append("<TD >" + dtTable.Rows[i][i2].ToString() + "</TD>");
                                var switchExpr2 = contflg;
                                switch (switchExpr2)
                                {
                                    case 0:
                                        {
                                            // If Trace.IsEnabled Then
                                            // Trace.Warn(dtTable.Columns(i2).ColumnName, dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", ""))
                                            // End If
                                            // If Not oGv.IsAdvance Then
                                            s.Append("<TD BGCOLOR=\"#EEEEEE\">" + (Convert.ToDouble(dtTable.Rows[i][i2]) / Convert.ToDouble(dtTable.Compute("SUM ([" + dtTable.Columns[i2].ColumnName + "])", ""))).ToString("#,##0.0%") + "</TD>");           // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                    // End If
                                    // .Append("<TD BGCOLOR=""#EEEEEE"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "#,##0.0%") & "</TD>")           'Percentage Format with 1 decimal digits

                                    // .Append("<TD BGCOLOR=""#EEEEEE"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "p") & "</TD>") 'Percentage Format with 2 decimal digits
                                    case 4:
                                        {
                                            s.Append("<TD BGCOLOR=\"#FFEEFF\">" + (Convert.ToDouble(dtTable.Rows[i][i2]) / Convert.ToDouble(dtTable.Rows[dtTable.Rows.Count - 1][i2])).ToString("#,##0.0%") + "</TD>");            // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }

                s.Append("</TR>");
            }

            if (!oGv.IsAdvance)
            {
                if (contflg == 0)
                {
                    s.Append("<TR  BGCOLOR=\"#DDDDDD\" ALIGN=\"right\" VALIGN=\"top\" > ");
                    s.Append("<TD><B>Total</B></TD>");
                    for (int i2 = 0, loopTo5 = dtTable.Columns.Count - 1; i2 <= loopTo5; i2++)
                    {
                        if (i2 > 0)
                        {
                            // .Append("<TD>" & String.Format("#,###,##0", dtTable.Compute("SUM(" & dtTable.Columns(i2).ColumnName & ")", "")) & "</TD>")
                            if (!oGv.IsAdvance)
                            {
                                s.AppendFormat("<TD>{0:#,###,##0}</TD>", dtTable.Compute("SUM([" + dtTable.Columns[i2].ColumnName + "])", ""));
                            }
                            // .AppendFormat("<TD>{0:#,###,##0}</TD>", dtTable.Compute("SUM(" & dtTable.Columns(i2).ColumnName & ")", ""))
                            s.Append("<TD>&nbsp;</TD>");
                        }
                    }
                }
            }

            // BEGIN OF Footer
            if (!oGv.IsAdvance)
            {
            }

            if (!(sDist == "All Districts" & sRegn == "National"))
            {
                s.Append("<TR>");
                s.Append("<TD COLSPAN=\"" + (dtTable.Columns.Count * 2 - 1) + "\">");
                s.AppendFormat("<DIV ALIGN=\"center\" CLASS=\"H9i\">\"{0}\"</DIV>", (bEn? oGv.Q_En: oGv.Q));
                s.AppendFormat("<DIV ALIGN=\"center\" CLASS=\"H8b\">{0}</DIV>", (bEn? oGt.Desc_En + " Survey Data": "Data Survei " + oGt.Desc));
                s.AppendFormat("<DIV ALIGN=\"center\" CLASS=\"H8\">{0}</DIV>", "Governance and Decentralization Survey 2 (GDS2) - Indonesia");
                // .Append("<SUP>++</SUP> Overall (National)")		  'uncomment this Kalo ternyata comparator nya per dist/regn
                s.Append("</TD>");
                s.Append("</TR>");
            }

            // s &= "<TR>"
            // s &= "<TD>&nbsp;</TD>"
            // For i = 1 To dtTable.Columns.Count - 1
            // s &= "<TD>" & dtTable.Compute("SUM (" & dtTable.Columns(i).ColumnName & ")", "") & "</TD>"
            // Next
            // s &= "</TR>"
            // END OF Footer
            // .Append("</TR>")



            // s &= "<DIV ALIGN=""RIGHT""><A HREF=""csv.aspx"" TITLE=""Click here to download Excel CSV"">Download Excel file (.CSV)</A> <A HREF=""csv.aspx""><img border=""0"" align=""absmiddle"" width=""16"" height=""16"" src=""../images/xls.gif""></img></A></DIV>"
            s.Append("</TABLE>");
            return s.ToString();
        }

         string DrawTable2(DataTable dtTable, DataTable dtTablePct, string cssClass)
        {
            short iRow = default(short), iCol;
            StringBuilder s;
            string sColName;
            // Dim colors() As String = {"#AAFFAA", "#CCCCFF", "#FFFFAA", "#FFCC66", "#00FFFF", "#FF99FF", "#DDFF55", "#55FFAA", "#FF3355", "#Ff5500"}

            // BEGIN OF Heading Top ---------------
            s = new StringBuilder("<TABLE BORDER=\"0\" CELLPADDING=\"3\" CELLSPACING=\"1\" CLASS=\"" + cssClass + "\" STYLE=\" border: 2px outset lightgrey; \" >");
            s.Append("<TR ALIGN=\"center\">");
            s.AppendFormat("<TD COLSPAN=\"{0}\">", dtTable.Columns.Count * 2 - 1);
            s.AppendFormat("<DIV CLASS=\"H5\">{0}</DIV>", var_parent_desc);
            s.Append(desc);
            s.AppendFormat("</TD>");
            s.Append("</TR>");
            // END OF Heading Top ---------------

            // BEGIN OF Heading Comparators  ---------------
            s.Append("<TR ALIGN=\"center\" CLASS=\"H5\">");
            // Try
            for (int i = 0, loopTo = dtTable.Columns.Count - 1; i <= loopTo; i++)
            {
                if (i == 0)
                {
                    // s &= "<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">Category</TD>"
                    // If oGv.IsAdvance Then
                    // .Append("<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">&nbsp;</TD>") 'Header Jumlah/% not use here
                    // Else
                    s.Append("<TD BGCOLOR=\"#FFEEFF\" ROWSPAN=\"3\">&nbsp;</TD>");
                }
                // End If
                else
                {
                    // If iRow Mod 2 = 0 Then
                    if (i <= sComp.Length / 2)
                    {
                        string sComp1 = sComp[0, i - 1];
                        if (!(sDist == "All Districts" & sRegn == "National"))
                        {
                            var switchExpr = sComp1;
                            switch (switchExpr)
                            {
                                case "District":
                                case "Province":
                                case "&nbsp;":
                                    {
                                        sComp1 = sComp1;
                                        break;
                                    }

                                case "Java/Off-Java":
                                    {
                                        sComp1 = sComp1;         // & "<SUP>++</SUP>"'uncomment this Kalo ternyata comparator nya per dist/regn
                                        break;
                                    }

                                default:
                                    {
                                        sComp1 = sComp1;
                                        break;
                                    }
                                // sComp1 = sComp1 & "<SUP>+</SUP>"   'uncomment this Kalo ternyata comparator nya per dist/regn
                            }
                        }
                        // red-brick
                        // .Append("<TD COLSPAN=""" & sComp(1, i - 1) * 2 & """ BGCOLOR=""#FFCCCC"">" & sComp1 & "</TD>")
                        // lightblue

                        // If oGv.IsAdvance Then
                        // .Append("<TD COLSPAN=""" & sComp(1, i - 1) * 1 & """ BGCOLOR=""#E6E6FA"" class=""H11vb"">" & sComp1 & "</TD>")
                        // Else
                        s.Append("<TD COLSPAN=\"" + Convert.ToInt32(sComp[1, i - 1]) * 2 + "\" BGCOLOR=\"#E6E6FA\" class=\"H11vb\">" + sComp1 + "</TD>");
                        // End If
                    }
                    // Else
                    // .Append("<TD COLSPAN=""2"" BGCOLOR=""#CCCCFF"">" & Replace(Replace(dtTable.Columns(i).ColumnName, "Count", ""), "_", " ") & "</TD>")
                    // End If
                }

                iRow += 1;
            }
            // Catch
            // End Try
            s.Append("</TR>");
            // END OF Heading Comparators ---------------

            // BEGIN OF Heading 2 ---------------
            s.Append("<TR ALIGN=\"center\">");
            for (int i = 0, loopTo1 = dtTable.Columns.Count - 1; i <= loopTo1; i++)
            {
                if (i > 0)
                {
                    // s &= "<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">Category</TD>"
                    // .Append("<TD BGCOLOR=""#FFEEFF"" ROWSPAN=""2"">&nbsp;</TD>")
                    // Else	'comm out
                    sColName = dtTable.Columns[i].Caption.Replace( "Count", "");
                    sColName = sColName.Replace( "Below7", "0-6");
                    sColName = sColName.Replace( "Above7", "7+");
                    sColName = sColName.Replace( "Not_Participate", "No");
                    sColName = sColName.Replace( "Participate", "Yes");
                    sColName = sColName.Replace( "Has_Civil_Servants", "Yes");
                    sColName = sColName.Replace( "No_Civil_Servants", "No");
                    sColName = sColName.Replace( "Has_Links", "Yes");
                    sColName = sColName.Replace( "No_Links", "No");
                    sColName = sColName.Replace( "Not_Aware_Of_Voice", "No");
                    sColName = sColName.Replace( "Aware_Of_Voice", "Yes");
                    sColName = sColName.Replace("_", " ");

                    // Warna selang-seling
                    // If iRow Mod 2 = 0 Then
                    // .Append("<TD COLSPAN=""2"" BGCOLOR=""#FFCCCC"">" & sColName & "</TD>")
                    // Else
                    // .Append("<TD COLSPAN=""2"" BGCOLOR=""#CCCCFF"">" & sColName & "</TD>")
                    // End If

                    // If oGv.IsAdvance Then
                    // .Append("<TD BGCOLOR=""#E6E6FA"" class=""H11b"">" & sColName & "</TD>")
                    // Else
                    s.Append("<TD COLSPAN=\"2\" BGCOLOR=\"#E6E6FA\"class=\"H11b\">" + sColName + "</TD>");
                    // End If
                }

                iRow += 1;
            }

            s.Append("</TR>");
            // END OF Heading 3 ---------------


            // BEGIN OF Heading 3 (Count Percent Header)---------------
            s.Append("<TR ALIGN=\"center\" CLASS=\"H9\" STYLE=\"border-bottom:black 1px solid;\"> ");
            for (int i = 0, loopTo2 = dtTable.Columns.Count - 1; i <= loopTo2; i++)
            {
                if (i > 0)
                {
                    // If Not oGv.IsAdvance Then
                    if (bEn)
                    {
                        s.Append("<TD BGCOLOR=\"#EEEEEE\">Count</TD><TD BGCOLOR=\"#DDDDDD\"><B>%</B></TD>");
                    }
                    else
                    {
                        s.Append("<TD BGCOLOR=\"#EEEEEE\">Jumlah</TD><TD BGCOLOR=\"#DDDDDD\"><B>%</B></TD>");
                    }
                    // End If
                }
            }

            s.Append("</TR>");
            // END OF Heading 3 ---------------
            iRow = 0;
            for (int i = 0, loopTo3 = dtTable.Rows.Count - 1; i <= loopTo3; i++)
            {
                s.Append("<TR ALIGN=\"right\" VALIGN=\"top\"> ");
                iRow += 1;
                for (int i2 = 0, loopTo4 = dtTable.Columns.Count - 1; i2 <= loopTo4; i2++)
                {
                    if (i2 == 0)
                    {
                        if (dtTable.Rows[i].IsNull(i2))
                        {
                            s.Append("<TD BGCOLOR=\"" + colors[i] + "\">&nbsp;</TD>");
                        }
                        else
                        {
                            s.Append("<TD NOWRAP BGCOLOR=\"" + colors[i] + "\">" + dtTable.Rows[i][i2] + "</TD>");
                        }
                    }
                    // If iRow Mod 2 = 0 Then
                    // If dtTable.Rows(i).IsNull(i2) Then
                    // s &= "<TD BGCOLOR=""#FFEEFF"">&nbsp;</TD>"
                    // Else
                    // s &= "<TD BGCOLOR=""#FFEEFF"">" & dtTable.Rows(i)(i2) & "</TD>"
                    // End If
                    // Else
                    // If dtTable.Rows(i).IsNull(i2) Then
                    // s &= "<TD>&nbsp;</TD>"
                    // Else
                    // s &= "<TD>" & dtTable.Rows(i)(i2) & "</TD>"
                    // End If
                    // End If
                    else
                    {
                        if (iRow % 2 == 0)
                        {
                            if (dtTable.Rows[i].IsNull(i2))
                            {
                                s.Append("<TD BGCOLOR=\"#FFEEFF\">0</TD>");
                                s.Append("<TD BGCOLOR=\"#FFEEFF\">0</TD>");
                            }
                            else
                            {
                                s.Append("<TD BGCOLOR=\"#FFEEFF\">" + dtTable.Rows[i][i2].ToString() + "</TD>");
                                var switchExpr1 = contflg;
                                switch (switchExpr1)
                                {
                                    case 0:
                                        {
                                            // If Not oGv.IsAdvance Then
                                            s.Append("<TD BGCOLOR=\"#FFEEFF\">" + Convert.ToDouble(dtTablePct.Rows[i][i2]).ToString( "#,##0.0%") + "</TD>");            // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                    // End If
                                    // .Append("<TD BGCOLOR=""#FFEEFF"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "#,##0.0%") & "</TD>")           'Percentage Format with 1 decimal digits
                                    // .Append("<TD BGCOLOR=""#FFEEFF"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "p") & "</TD>")		'Percentage Format with 2 decimel digits
                                    case 4:
                                        {
                                            s.Append("<TD BGCOLOR=\"#FFEEFF\">" + (Convert.ToDouble(dtTable.Rows[i][i2]) / Convert.ToDouble(dtTable.Rows[dtTable.Rows.Count - 1][i2])).ToString("#,##0.0%") + "</TD>");           // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                }
                            }
                        }
                        else
                        {
                            if (dtTable.Rows[i].IsNull(i2))
                            {
                                s.Append("<TD>0</TD>");
                                s.Append("<TD BGCOLOR=\"#EEEEEE\">0</TD>");
                            }
                            else
                            {
                                s.Append("<TD >" + dtTable.Rows[i][i2] + "</TD>");
                                var switchExpr2 = contflg;
                                switch (switchExpr2)
                                {
                                    case 0:
                                        {
                                            // If Trace.IsEnabled Then
                                            // Trace.Warn(dtTable.Columns(i2).ColumnName, dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", ""))
                                            // End If
                                            // If Not oGv.IsAdvance Then
                                            s.Append("<TD BGCOLOR=\"#EEEEEE\">" + (Convert.ToDouble(dtTablePct.Rows[i][i2])).ToString( "#,##0.0%") + "</TD>");           // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                    // End If
                                    // .Append("<TD BGCOLOR=""#EEEEEE"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "#,##0.0%") & "</TD>")           'Percentage Format with 1 decimal digits

                                    // .Append("<TD BGCOLOR=""#EEEEEE"">" & Format((dtTable.Rows(i)(i2)) / (dtTable.Compute("SUM (" & dtTable.Columns(i2).ColumnName & ")", "")), "p") & "</TD>") 'Percentage Format with 2 decimal digits
                                    case 4:
                                        {
                                            s.Append("<TD BGCOLOR=\"#FFEEFF\">" + (Convert.ToDouble(dtTable.Rows[i][i2]) / Convert.ToDouble(dtTable.Rows[dtTable.Rows.Count - 1][i2])).ToString( "#,##0.0%") + "</TD>");            // Percentage Format with 1 decimal digits
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }

                s.Append("</TR>");
            }

            if (!oGv.IsAdvance)
            {
                if (contflg == 0)
                {
                    s.Append("<TR  BGCOLOR=\"#DDDDDD\" ALIGN=\"right\" VALIGN=\"top\" > ");
                    s.Append("<TD><B>Total</B></TD>");
                    for (int i2 = 0, loopTo5 = dtTable.Columns.Count - 1; i2 <= loopTo5; i2++)
                    {
                        if (i2 > 0)
                        {
                            // .Append("<TD>" & String.Format("#,###,##0", dtTable.Compute("SUM(" & dtTable.Columns(i2).ColumnName & ")", "")) & "</TD>")
                            if (!oGv.IsAdvance)
                            {
                                s.AppendFormat("<TD>{0:#,###,##0}</TD>", dtTable.Compute("SUM([" + dtTable.Columns[i2].ColumnName + "])", ""));
                            }
                            // .AppendFormat("<TD>{0:#,###,##0}</TD>", dtTable.Compute("SUM(" & dtTable.Columns(i2).ColumnName & ")", ""))
                            s.Append("<TD>&nbsp;</TD>");
                        }
                    }
                }
            }

            // BEGIN OF Footer
            if (!oGv.IsAdvance)
            {
            }

            if (!(sDist == "All Districts" & sRegn == "National"))
            {
                s.Append("<TR>");
                s.Append("<TD COLSPAN=\"" + (dtTable.Columns.Count * 2 - 1) + "\">");
                s.AppendFormat("<DIV ALIGN=\"center\" CLASS=\"H9i\">\"{0}\"</DIV>", (bEn? oGv.Q_En: oGv.Q));
                s.AppendFormat("<DIV ALIGN=\"center\" CLASS=\"H8b\">{0}</DIV>", (bEn? oGt.Desc_En + " Survey Data": "Data Survei " + oGt.Desc));
                s.AppendFormat("<DIV ALIGN=\"center\" CLASS=\"H8\">{0}</DIV>", "Governance and Decentralization Survey 2 (GDS2) - Indonesia");
                // .Append("<SUP>++</SUP> Overall (National)")		  'uncomment this Kalo ternyata comparator nya per dist/regn
                s.Append("</TD>");
                s.Append("</TR>");
            }

            // s &= "<TR>"
            // s &= "<TD>&nbsp;</TD>"
            // For i = 1 To dtTable.Columns.Count - 1
            // s &= "<TD>" & dtTable.Compute("SUM (" & dtTable.Columns(i).ColumnName & ")", "") & "</TD>"
            // Next
            // s &= "</TR>"
            // END OF Footer
            // .Append("</TR>")



            // s &= "<DIV ALIGN=""RIGHT""><A HREF=""csv.aspx"" TITLE=""Click here to download Excel CSV"">Download Excel file (.CSV)</A> <A HREF=""csv.aspx""><img border=""0"" align=""absmiddle"" width=""16"" height=""16"" src=""../images/xls.gif""></img></A></DIV>"
            s.Append("</TABLE>");
            return s.ToString();
        }

         string DrawTableCont(DataTable dtTable)
        {
            short iRow;
            short iCol;
            StringBuilder sb;
            sb = new StringBuilder("<TABLE BORDER=\"0\">");
            sb.Append("<TR><TD>&nbsp;</TD>");
            var loopTo = dtTable.Columns.Count - 1;
            for (iCol = 0; iCol <= loopTo; iCol++)
                sb.Append("<TR>" + dtTable.Columns[iCol].ColumnName + "</TD>");
            sb.Append("</TR>");
            sb.Append("<TR><TD>&nbsp;</TD>");
            sb.Append("</TABLE>");
            return sb.ToString();
        }

         DataTable GetDataAv(string sql)
         {
             var dt = new DataTable(); // this DataTable will make use of column caption
             var cn = new SqlConnection(commonModule.GetConnString());
             var cm = new SqlCommand(sql, cn);
             cn.Open();
             SqlDataReader dr;
             dr = cm.ExecuteReader();
             dt.Columns.Add(dr.GetName(1), dr.GetFieldType(1));
             dt.Columns.Add(dr.GetName(2), dr.GetFieldType(2));
             DataRow drw;
             while (dr.Read())
             {
                 drw = dt.NewRow();
                 drw[0] = dr[1]; // ambil field [desc]
                 drw[1] = dr[2]; // ambil field [value]
                 dt.Rows.Add(drw);
                 if (Trace.IsEnabled)
                 {
                     Trace.Warn("testAv", dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString());
                 }
             }

             int iRowsCount = dt.Rows.Count;
             int iColsCount = dt.Columns.Count;
             if (dr.NextResult())
             {
                 do
                 {
                     dt.Columns.Add(dr.GetName(2), typeof(int));
                     iColsCount += 1;
                     for (int i = 0, loopTo = iRowsCount - 1; i <= loopTo; i++)
                     {
                         dr.Read();
                         dt.Rows[i][iColsCount - 1] = dr[2]; // adds rows (from the field [desc])to the last created columns in the dt
                         if (Trace.IsEnabled)
                         {
                             Trace.Warn("testAv", dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString());
                         }
                     }

                     if (dr.GetName(2).IndexOf("cap") == 5) // kalau ada nama kolom berawalan "cap", since the query for the comparator results in "Countcap1Jawa", etc
                     {
                         dt.Columns[iColsCount - 1].Caption = dr.GetName(2).Remove(5, 5); // removes "cap<2 digits number> (see comp_id field in the GDS2_11_Comp)"
                     }
                     else
                     {
                         dt.Columns[iColsCount - 1].Caption = dr.GetName(2);
                     }
                 }
                 while (dr.NextResult());
             }

             dr.Close();
             cn.Close();
             return dt;
         }

         public DataTable[] GetDataAv2(string sql)
         {
             var dt = new DataTable[2];
             dt[0] = new DataTable(); // this DataTable will make use of column caption
             dt[1] = new DataTable(); // this DataTable will make use of column caption
             var cn = new SqlConnection(commonModule.GetConnString());
             var cm = new SqlCommand(sql, cn);
             cn.Open();
             SqlDataReader dr;
             dr = cm.ExecuteReader();
             dt[0].Columns.Add(dr.GetName(1), dr.GetFieldType(1));
             dt[0].Columns.Add(dr.GetName(2), dr.GetFieldType(2)); // Count Column
             dt[1].Columns.Add(dr.GetName(1), dr.GetFieldType(1));
             dt[1].Columns.Add(dr.GetName(3), dr.GetFieldType(3)); // Pct Column
             DataRow drw;
             DataRow drw1;
             while (dr.Read())
             {
                 drw = dt[0].NewRow();
                 drw1 = dt[1].NewRow();
                 drw[0] = dr[1]; // ambil field [desc]
                 drw[1] = dr[2]; // ambil field [value]
                 drw1[0] = dr[1]; // ambil field [desc]
                 drw1[1] = dr[3]; // ambil field [Pct]
                 dt[0].Rows.Add(drw);
                 dt[1].Rows.Add(drw1);
                 if (Trace.IsEnabled)
                 {
                     Trace.Warn("testAv", dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString());
                 }
             }

             int iRowsCount = dt[0].Rows.Count;
             int iColsCount = dt[0].Columns.Count;
             int iCnt = 0;
             if (dr.NextResult())
             {
                 do
                 {
                     iCnt += 1;
                     dt[0].Columns.Add(dr.GetName(2), typeof(int));
                     dt[1].Columns.Add(dr.GetName(3) + iCnt.ToString(), typeof(float));
                     iColsCount += 1;
                     for (int i = 0, loopTo = iRowsCount - 1; i <= loopTo; i++)
                     {
                         dr.Read();
                         dt[0].Rows[i][iColsCount - 1] = dr[2]; // adds rows (from the field [desc])to the last created columns in the dt
                         dt[1].Rows[i][iColsCount - 1] = dr[3]; // adds rows (from the field [desc])to the last created columns in the dt
                         if (Trace.IsEnabled)
                         {
                             Trace.Warn("testAv", dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString());
                         }
                     }

                     if (dr.GetName(2).IndexOf("cap") == 5) // kalau ada nama kolom berawalan "cap", since the query for the comparator results in "Countcap1Jawa", etc
                     {
                         dt[0].Columns[iColsCount - 1].Caption = dr.GetName(2).Remove(5, 5); // removes "cap<2 digits number> (see comp_id field in the GDS2_11_Comp)"
                     }
                     else
                     {
                         dt[0].Columns[iColsCount - 1].Caption = dr.GetName(2);
                     }
                 }
                 while (dr.NextResult());
             }

             dr.Close();
             cn.Close();
             return dt;
         }
          DataTable GetDataCat(string sql)
         {
             var dt = new DataTable(); // this DataTable will make use of column caption
             var cn = new SqlConnection(commonModule.GetConnString());
             var cm = new SqlCommand(sql, cn);
             cn.Open();
             SqlDataReader dr;
             dr = cm.ExecuteReader();
             for (int i = 1, loopTo = dr.FieldCount - 1; i <= loopTo; i += 2)
             {
                 var dc = new DataColumn();
                 dc.DataType = dr.GetFieldType(i);
                 dc.ColumnName = dr.GetName(i);
                 if (dr.GetName(i).IndexOf("cap") == 5) // kalau ada nama kolom berawalan "cap", since the query for the comparator results in "Countcap1Jawa", etc
                 {
                     dc.Caption = dr.GetName(i).Remove(5, 5); // removes "cap<2 digits number> (see comp_id field in the GDS2_11_Comp)"
                 }
                 else
                 {
                     dc.Caption = dr.GetName(i);
                 }

                 dt.Columns.Add(dc);
             }

             DataRow dRow;
             int i2;
             // Dim dc As DataColumn
             while (dr.Read())
             {
                 dRow = dt.NewRow();
                 i2 = 0;
                 for (int i = 1, loopTo1 = dr.FieldCount - 1; i <= loopTo1; i += 2)
                 {
                     // If Not IsDBNull(dr(i)) Then
                     // dRow(i2) = Server.HtmlEncode(dr(i).ToString)
                     dRow[i2] = Server.HtmlEncode(dr.IsDBNull(i) ? "0" : dr[i].ToString());
                     // Else
                     // dRow(i2) = dr(i).ToString
                     // End If
                     // If Trace.IsEnabled Then
                     // Trace.Warn("dr(" & i & ")", dr(i))
                     // End If
                     i2 += 1;
                 }

                 dt.Rows.Add(dRow);
             }
             // End If

             dr.Close();
             cn.Close();
             return dt;
         }

          DataTable GetDataCont(string sql)
          {
              var dt = new DataTable();
              var cn = new SqlConnection(commonModule.GetConnString());
              var cm = new SqlCommand(sql, cn);
              SqlDataReader dr;
              DataRow dRow;
              cn.Open();
              dr = cm.ExecuteReader();
              for (int i = 0, loopTo = dr.FieldCount - 1; i <= loopTo; i++)
                  dt.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
              do
              {
                  while (dr.Read())
                  {
                      dRow = dt.NewRow();
                      for (int i = 0, loopTo1 = dr.FieldCount - 1; i <= loopTo1; i++)
                          dRow[i] = (dr.IsDBNull(i)? 0: dr[i]);
                      dt.Rows.Add(dRow);
                  }
              }
              while (dr.NextResult());
              // End If

              if (Convert.ToDouble(dt.Compute("AVG (Mean)", "")) <= 1)   // If the cont. variables are percentages (yes-no | not like distance or income)
              {
                  for (int i = 0, loopTo2 = dt.Rows.Count - 1; i <= loopTo2; i++)
                  {
                      dt.Rows[i][1] = Convert.ToDouble(dt.Rows[i][1]) * 100;
                      dt.Rows[i][2] = Convert.ToDouble(dt.Rows[i][2]) * 100;
                      dt.Rows[i][3] = Convert.ToDouble(dt.Rows[i][3]) * 100;
                      dt.Rows[i][4] = Convert.ToDouble(dt.Rows[i][4]) * 100;
                      dt.Rows[i][5] = Convert.ToDouble(dt.Rows[i][5]) * 100;
                      dt.Rows[i][6] = Convert.ToDouble(dt.Rows[i][6]) * 100;
                  }

                  isPct = true;
              }

              return dt;
          }

          public bool GetValidRequest()
          {
              // For i As Integer = 0 To Request.Params.Keys.Count - 1
              // Response.Write(Request.Params.Keys.Item(i))
              // Response.Write(" = ")
              // Response.Write(Request.Params(i))
              // Response.Write("<BR>")
              // Next


              iDs = (Request.Params["ds"] != "" ? Convert.ToInt32(Request.Params["ds"]) : 11); // default to 11 (household)
              bEn = commonModule.IsEnglish();
              isChartVisible = (Request.Params["ch"] == "1"? true: false);
              // Dim astr() As String = Request.Params("v").Split("~")
              // var_id = astr(5)
              // sDist = astr(0)
              // sRegn = astr(1)
              if (Request.Params["d"] != "")
              {
                  sDist = Request.Params["d"];
              }
              else
              {
                  sDist = "Natl";
              }

              if (Request.Params["r"] != "")
              {
                  sRegn = Request.Params["r"];
              }
              else
              {
                  sRegn = "All";
              }

              if (!bEn)
              {
                  System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("id-ID");
              }

              if (Request.Params["v"] != "")
              {
                  var_id = Request.Params["v"];
                  return true;
              }
              else
              {
                  return false;
              }

              // 0 = sDist
              // 1 = sRegn
              // 2 = ""
              // 3 = contflg
              // 4 = exstr
              // 5 = gdsid
              // 6 = ""

          }

          public void InitTrees()
          {
              bool _isEnglish = bEn;
              int _datasetNumber;
              bool _isContVarOnly = false;
              TreeLocations1.SelectedID = Request.Params["r"].ToString() + "~" + Request.Params["d"].ToString();
              TreeVars1.SelectedID = Request.Params["v"];
              if (Request.Params["ds"] != "")
              {
                  _datasetNumber = Convert.ToInt32(Request.QueryString["ds"]);
              }
              else
              {
                  _datasetNumber = 11;
              }

              if (Request.Params["c"] == "1")
              {
                  _isContVarOnly = true;
              }

              {
                  var withBlock = this.TreeLocations1;
                  withBlock.DatasetNumber = _datasetNumber;
                  withBlock.IsEnglish = _isEnglish;
                  withBlock.IsSecondLevelVisible = true;
                  withBlock.DivTitleString = "Pilih Propinsi/Kabupaten";
                  withBlock.DivTitleStringEn = "Province/District Selection";
                  withBlock.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                  withBlock.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                  withBlock.DivTitleAdditionalAttr = "onclick=\"sh0('dlroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

              }

              {
                  var withBlock1 = this.TreeVars1;
                  withBlock1.DatasetNumber = _datasetNumber;
                  withBlock1.GdsTable = oGt;
                  withBlock1.IsEnglish = _isEnglish;
                  withBlock1.DivTitleString = "Pilih dari daftar kuesioner " + oGt.Desc;
                  withBlock1.DivTitleStringEn = oGt.Desc_En + " questionnaires list";
                  withBlock1.IsContVarOnly = _isContVarOnly;
                  withBlock1.IsSecondLevelVisible = true;
                  withBlock1.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                  withBlock1.DivContentStyle = "padding:0px 10px 0px 10px;";
                  withBlock1.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                  withBlock1.DivTitleAdditionalAttr = "onclick=\"sh0('dvroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

              }

              {
                  var withBlock2 = this.TreeComparators1;
                  withBlock2.DatasetNumber = _datasetNumber;
                  withBlock2.DivTitleString = "Pilih Pembanding";
                  withBlock2.DivTitleStringEn = "Comparators Selection";
                  withBlock2.IsEnglish = _isEnglish;
                  withBlock2.IsSecondLevelVisible = true;
                  withBlock2.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                  withBlock2.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                  withBlock2.DivTitleAdditionalAttr = "onclick=\"sh0('dcroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

              }

              if (Request.Params["c"] == "1")
              {
                  this.TreeLocations1.Visible = false;
                  this.TreeComparators1.Visible = false;
                  this.TreeVars1.IsSecondLevelVisible = true;
              }
          }

          public void SetUI()
          {
              if (bEn)
              {
                  btnNext.Value = commonModule.NEXTSTRINGEN;
                  btnPrev.Value = commonModule.PREVSTRINGEN;
              }
              else
              {
                  btnNext.Value = commonModule.NEXTSTRING;
                  btnPrev.Value = commonModule.PREVSTRING;
              }

              if (char.IsNumber(Request.Params["c"].ToString(), 0))
              {
                  if (Convert.ToInt32(Request.Params["c"]) == 1)
                  {
                      isSingleVar = false;
                  }
                  else
                  {
                      isSingleVar = true;
                  }
              }
              else
              {
                  isSingleVar = true;
              }
          }

          public void ShowResultAv()
          {
              // Dim oDt As DataTable = GetDataAv(BuildSqlAvFinal)
              // Me.DataGrid1.Visible = False
              // Me.Literal1.Text = DrawTable(oDt, "grid") 'sesuaikan dengan file .css
              // Me.LiteralToolTip.Text = WriteToolTipDiv()
              // If isChartVisible Then
              // CreateChartCatSumm(oDt)
              // End If
              // Dim dg1 As New DataGrid
              // Me.PlaceHolder1.Controls.Add(dg1)
              // dg1.DataSource = oDt
              // dg1.DataBind()

              DataTable[] oDt = GetDataAv2(BuildSqlAvFinal());
              this.DataGrid1.Visible = false;
              this.Literal1.Text = DrawTable2(oDt[0], oDt[1], "grid"); // sesuaikan dengan file .css
              this.LiteralToolTip.Text =WriteToolTipDiv();
              if (isChartVisible)
              {
                  CreateChartCatSumm(oDt[1]);
                  CreateChartCatSumm2(oDt[1], oDt[0]);
              }
          }


          public void ShowResultCat()
          {
              DataTable oDt = GetDataCat(BuildSqlCatFinal());
              this.DataGrid1.Visible = false;
              this.Literal1.Text = DrawTable(oDt, "grid"); // sesuaikan dengan file .css
              this.Literal1.Text += WriteToolTipDiv();
              if (isChartVisible)
              {
                  CreateChartCat(oDt);
              }
          }

          public void ShowResultCont()
          {
              var sb = new StringBuilder();
              if (sDist == "All") // All districts in a provinces
              {
                  sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, oGt.ProvFld, oGg.Prov, null, oGg.ProvName, null, null, null, oGv.Tbl_Id, BasicCompType.Province, oGv.Criteria));
                  sb.Append(";");
                  sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, null, null, null, null, null, null, null, oGv.Tbl_Id, BasicCompType.National, oGv.Criteria));
                  sb.Append(";");
              }
              else if (sDist == "Natl") // All provinces / national
              {
                  sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, null, null, null, null, null, null, null, oGv.Tbl_Id, BasicCompType.National, oGv.Criteria));
                  sb.Append(";");
              }
              else
              {
                  sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, oGt.KabuFld, oGg.Kabu, oGt.ProvFld, oGg.Prov, oGg.KabuName, oGg.ProvName, null, null, null, oGv.Tbl_Id, BasicCompType.District, oGv.Criteria));
                  sb.Append(";");
                  sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, oGt.ProvFld, oGg.Prov, null, oGg.ProvName, null, null, null, oGv.Tbl_Id, BasicCompType.Province, oGv.Criteria));
                  sb.Append(";");
                  sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, null, null, null, null, null, null, null, oGv.Tbl_Id, BasicCompType.National, oGv.Criteria));
                  sb.Append(";");
              }

              // ********************

              string[] comps = GetRequestComparators();
              if (!(comps == null))
              {
                  DataTable dt = GetComparator(oGt.CompTable);
                  DataRow[] drw;
                  for (int i = 0, loopTo = comps.Length - 1; i <= loopTo; i++)
                  {
                      drw = dt.Select("subgrp_id=" + comps[i]);
                      if (bEn) // Jagan (maksudnya: Jangan) pake IIF (soalnya FalsePart-nya tetep di-evaluate)
                      {
                          for (int j = 0, loopTo1 = drw.Length - 1; j <= loopTo1; j++)
                          {
                              sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, null, null, null, null, drw[j]["var"].ToString(), drw[j]["value"].ToString(), drw[j]["desc_en"].ToString(), oGv.Tbl_Id, BasicCompType.Other, oGv.Criteria));
                              sb.Append(";");
                          }
                      }
                      else
                      {
                          for (int j = 0, loopTo2 = drw.Length - 1; j <= loopTo2; j++)
                          {
                              sb.Append(BuildSqlCont(oGv.Var, oGv.Tbl, null, null, null, null, null, null, drw[j]["var"].ToString(), drw[j]["value"].ToString(), drw[j]["desc"].ToString(), oGv.Tbl_Id, BasicCompType.Other, oGv.Criteria));
                              sb.Append(";");
                          }
                      }
                  }

                  if (Trace.IsEnabled)
                  {
                      Trace.Warn("BuildSqlCont()", sb.ToString());
                  }
                  // ********************
              }

              if ((sb.ToString(sb.Length - 1, 1) ?? "") == ";")
              {
                  sb.Remove(sb.Length - 1, 1);    // removes trailkling (= maksud gw trailing lho!) semicolon
              }

              DataTable oDt = GetDataCont(sb.ToString());
              this.DataGrid1.Visible = true;
              this.DataGrid1.Caption = "<DIV STYLE=\"background-color:#AAAAFF;font:bold 10pt Arial;\">" + (bEn? oGv.Var_Parent_Desc_En: oGv.Var_Parent_Desc) + "</DIV><DIV STYLE=\"background-color:#AAAAFF;font:bold 8pt Arial;\">" + (bEn? oGv.Desc_En: oGv.Desc) + "</DIV>";
              this.DataGrid1.DataSource = oDt;
              this.DataGrid1.DataBind();
              this.Literal1.Text += WriteToolTipDiv();
              if (isChartVisible)
              {
                  CreateChartCont(oDt);
                  CreateChartContBar(oDt);
              }
          }


          private string WriteToolTipDiv() // this function nulisin div buat bikin tooltip di WebChartViewer
          {
              StringBuilder fsOut;
              fsOut = new StringBuilder("<DIV ID=\"tip\" ");
              fsOut.Append(" Style = \"position:absolute; ");
              fsOut.Append(" top:0px;left:0px;width:200px;height:50px; ");
              fsOut.Append(" background:yellow;z-Order:4;visibility:hidden; ");
              fsOut.Append(" border:1px solid black; ");
              fsOut.Append(" font:bold 12px Arial; ");
              fsOut.Append(" padding:3px 3px 3px 10px; ");
              fsOut.Append(" overflow:visible; ");
              fsOut.Append(" filter:Alpha(Opacity=85) \"> ");
              fsOut.Append(" </DIV> ");
              return fsOut.ToString();
          }


    }
}