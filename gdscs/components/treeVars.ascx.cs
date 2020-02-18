using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace gds
{
    public partial class treeVars : System.Web.UI.UserControl, IGdsTree
    {
        bool _IsEnglish;
        bool _IsContVarOnly;
        bool _IsSecondLevelVisible;
        int _DataSetNumber;
        string _DivContentStyle;
        string _DivTitleStyle;
        string _DivContentClass;
        string _DivTitleClass;
        string _DivContainerStyle;
        string _DivContainerClass;
        string _DivContainerAdditionalAttr;
        string _DivContentAdditionalAttr;
        string _DivTitleAdditionalAttr;
        string _SelectedID;
        string _DivTitleString;
        string _DivTitleStringEn;
        gdsTable _GdsTable;
        ArrayList sl;

        static int ii = 0;

        public string SelectedID
        {
            get { return _SelectedID; }
            set { _SelectedID = value; }
        }

        public bool IsContVarOnly
        {
            get { return _IsContVarOnly; }
            set { _IsContVarOnly = value; }
        }

        public bool IsEnglish
        {
            get { return _IsEnglish; }
            set { _IsEnglish = value; }
        }

        public int DatasetNumber
        {
            get { return _DataSetNumber; }
            set { _DataSetNumber = value; }
        }

        public bool IsSecondLevelVisible
        {
            get { return _IsSecondLevelVisible; }
            set { _IsSecondLevelVisible = value; }
        }

        public string DivContainerClass
        {
            get { return _DivContainerClass; }
            set { _DivContainerClass = value; }
        }

        public string DivContainerStyle
        {
            get { return _DivContainerStyle; }
            set { _DivContainerStyle = value; }
        }

        public string DivContentStyle
        {
            get { return _DivContentStyle; }
            set { _DivContentStyle = value; }
        }

        public string DivTitleStyle
        {
            get { return _DivTitleStyle; }
            set { _DivTitleStyle = value; }
        }

        public string DivContentClass
        {
            get { return _DivContentClass; }
            set { _DivContentClass = value; }
        }

        public string DivTitleString
        {
            get { return _DivTitleString; }
            set { _DivTitleString = value; }
        }

        public string DivTitleStringEn
        {
            get { return _DivTitleStringEn; }
            set { _DivTitleStringEn = value; }
        }

        public string DivTitleClass
        {
            get { return _DivTitleClass; }
            set { _DivTitleClass = value; }
        }

        public string DivContainerAdditionalAttr
        {
            get { return _DivContainerAdditionalAttr; }
            set { _DivContainerAdditionalAttr = value; }
        }

        public string DivContentAdditionalAttr
        {
            get { return _DivContentAdditionalAttr; }
            set { _DivContentAdditionalAttr = value; }
        }

        public string DivTitleAdditionalAttr
        {
            get { return _DivTitleAdditionalAttr; }
            set { _DivTitleAdditionalAttr = value; }
        }

        public gdsTable GdsTable
        {
            get { return _GdsTable; }
            set { _GdsTable = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            {
                Literal1.Text = BuildVarTreeRoots(_IsEnglish);
            }
            //catch (SqlException ex)
            //{
            //    commonModule.RedirectError(ex);
            //    Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
            //    Response.Redirect("err.aspx");
            //}
            //catch (Exception ex)
            //{
            //    commonModule.RedirectError(ex);
            //    Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
            //    System.Web.HttpContext.Current.Response.Redirect("err.aspx");
            //}
        }

        private string BuildVarTreeRoots(bool isEnglish)
        {
            sl = new ArrayList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(string.Empty);


            string sql = "SELECT [var],[lvl],[var_id],[var_parent]" + ",  [desc_en] ,[desc] " + ",[isContVar]" + " FROM " + _GdsTable.VarTable + " WHERE [isVisible] = 1 " + (_IsContVarOnly? " AND [isContVar] > 0": " AND [isContVar] < 2") + " ORDER BY [ord]";
            if (Trace.IsEnabled)
                Trace.Warn("BuildVarTreeRoots()", sql);
            
            DataTable dt = commonModule.GetData(commonModule.GetConnString(), sql);
            {
                // DivContainer
                sb.AppendFormat("<DIV STYLE=\"{0}\" {1} {2}>"
                    , (_DivContainerStyle != ""? _DivContainerStyle: "cursor:hand")
                    , (_DivContainerClass != ""? "CLASS=\"" + _DivContainerClass + "\"": "")
                    , _DivContainerAdditionalAttr);

                // DivTitle
                sb.AppendFormat("<DIV STYLE=\"{0}\" {1} {2}>"
                    , (_DivTitleStyle != ""? _DivTitleStyle: "cursor:hand;")
                    , (_DivTitleClass != ""? "CLASS=\"" + _DivTitleClass + "\"": "CLASS=\"iFl\" ")
                    , (_DivTitleAdditionalAttr != ""? _DivTitleAdditionalAttr: "onclick=\"sh0('dvroot');\""));

                if (_IsSecondLevelVisible)
                {
                    if (_IsContVarOnly)
                    {
                        sb.AppendFormat("<IMG SRC=\"images/plus.gif\" ID=\"dvrootimg\">&nbsp;");
                        sb.AppendFormat("<IMG SRC=\"images/tree.gif\" ID=\"dvrootimgb\">&nbsp;");
                        sb.AppendFormat("{0}</DIV>", (_IsEnglish? _DivTitleStringEn: _DivTitleString));
                    }
                    else
                    {
                        sb.AppendFormat("<IMG SRC=\"images/plus.gif\" ID=\"dvrootimg\">&nbsp;");
                        sb.AppendFormat("<IMG SRC=\"images/tree.gif\" ID=\"dvrootimgb\">&nbsp;");
                        sb.AppendFormat("{0}</DIV>", (_IsEnglish? _DivTitleStringEn: _DivTitleString));
                    }
                    // DivContent
                    sb.AppendFormat("<DIV STYLE=\"{0}\" {1} ID=\"dvroot\" {2}>"
                        , (_DivContentStyle != ""? _DivContentStyle: "padding-left:20px;display:block;")
                        , (_DivContentClass != ""? "CLASS=\"" + _DivContentClass + "\"":"")
                         , _DivContentAdditionalAttr );
                    sb.AppendLine();
                }
                else
                {
                    if (_IsContVarOnly)
                    {
                        sb.AppendFormat("<IMG SRC=\"images/plus.gif\" ID=\"dvrootimg\">&nbsp;");
                        sb.AppendFormat("<IMG SRC=\"images/tree.gif\" ID=\"dvrootimgb\">&nbsp;");
                        sb.AppendFormat("{0}</DIV>", (_IsEnglish ? _DivTitleStringEn : _DivTitleString));
                    }
                    else
                    {
                        sb.AppendFormat("<IMG SRC=\"images/plus.gif\" ID=\"dvrootimg\">&nbsp;");
                        sb.AppendFormat("<IMG SRC=\"images/tree.gif\" ID=\"dvrootimgb\">&nbsp;");
                        sb.AppendFormat("{0}</DIV>", (_IsEnglish? _DivTitleStringEn: _DivTitleString));
                    }
                    // DivContent
                    sb.AppendFormat("<DIV STYLE=\"{0}\" {1} ID=\"dvroot\" {2}>"
                        , (_DivContentStyle != ""? _DivContentStyle: "padding-left:20px;display:none;")
                        , (_DivContentClass != ""? "CLASS=\"" + _DivContentClass + "\"": "")
                         , _DivContentAdditionalAttr );
                    sb.AppendLine();
                }

                sb.Append(BuildTreeChildren(dt, "var_parent IS NULL ", "iFl", _IsEnglish));  // Recursive function call

                sb.Append("</DIV>"); // END Of DivContent
                sb.Append("</DIV>"); // END Of DivContainer
                sb.AppendLine();
            }

            return sb.ToString();
        }


        private string BuildTreeChildren2(DataTable table, string where, string cssClass, bool isEnglish)
        {
            string desc;
            int var_id;
            string var;
            int contflg;
            int lvl;
            string exstr;
            var fsOut = new StringBuilder(string.Empty);
            DataRow[] dRw = table.Select(where);
            for (int i = 0, loopTo = dRw.Length - 1; i <= loopTo; i++)
            {
                if (!sl.Contains(dRw[i]["var"]))
                {
                    if (_IsEnglish)
                        desc = dRw[i].IsNull("desc_en") ? dRw[i]["desc"].ToString() : dRw[i]["desc_en"].ToString();
                    else
                        desc = dRw[i].IsNull("desc") ? "" : dRw[i]["desc"].ToString(); 

                    sl.Add(dRw[i]["var"]);
                    var_id = Convert.IsDBNull(dRw[i]["var"]) ? 0 : Convert.ToInt32(dRw[i]["var"]);
                    var = dRw[i].IsNull("var")? "": dRw[i]["var"].ToString();
                    contflg = dRw[i].IsNull("isContVar") ? 0 : Convert.ToInt32(dRw[i]["isContVar"]);
                    lvl = Convert.ToInt32(dRw[i]["lvl"]); // kolom Level (penanda lowest level)
                    if (lvl != 0) 
                    {
                        fsOut.AppendFormat("<DIV CLASS=\"{1}\" STYLE=\"padding-left:20px;\" onClick=\"sh('dv{0}');\" >", dRw[i]["var"], cssClass);
                        fsOut.AppendFormat("<IMG SRC=\"images/fplus.gif\" ID=\"dv{0}img\">&nbsp;", var_id);
                        fsOut.Append(desc);
                        fsOut.AppendLine("</DIV>");
                    }
                    else 
                    {
                        if (_IsContVarOnly)
                            fsOut.AppendFormat("<LABEL FOR=\"zz{0}\" ><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT TYPE=\"checkbox\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}~z\">{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                        else
                        {
                            if (contflg == 5)       // Use Stored Proc?
                                fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                            else
                            {
                                if (ii == 0 | _SelectedID == var_id.ToString()) // ensures 1st var. is always selected
                                    fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" CHECKED NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                                else
                                    fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });

                                ii++;
                            }
                        }
                    }

                    // no need to traverse through all nodes, lvl=0 tells us to stop. 
                    if (!(lvl == 0))
                    {
                        fsOut.AppendFormat("<DIV ID=\"dv{0}\" STYLE=\"padding:0px 0px 5px 20px;display:none;\"> ", var_id);
                        switch (lvl)
                        {
                            case 0:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id.ToString() + "'", "iFl", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i4", isEnglish))
                            case 1:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id.ToString() + "'", "iFl2", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i4", isEnglish))
                            case 2:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id.ToString() + "'", "iFl2", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i3", isEnglish))
                            case 3:
                                {
                                    // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i1", isEnglish))
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id.ToString() + "'", "iFl2", _IsEnglish));
                                    break;
                                }

                            default:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id.ToString() + "'", "iFl", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i1", isEnglish))
                        }

                        fsOut.AppendLine("</DIV> ");
                    }
                }
            }
            return fsOut.ToString();
        }

        private string BuildTreeChildren(DataTable table, string where, string cssClass, bool isEnglish)
        {
            string desc;
            int var_id;
            string var;
            int contflg;
            int lvl;
            string exstr;
            var fsOut = new StringBuilder(string.Empty);
            DataRow[] dRw = table.Select(where);
            for (int i = 0, loopTo = dRw.Length - 1; i <= loopTo; i++)
            {
                if (!sl.Contains(dRw[i]["var_id"]))
                {
                    if (_IsEnglish)
                        desc = dRw[i].IsNull("desc_en") ? dRw[i]["desc"].ToString() : dRw[i]["desc_en"].ToString();
                    else
                        desc = dRw[i].IsNull("desc") ? "" : dRw[i]["desc"].ToString();

                    sl.Add(dRw[i]["var_id"]);
                    var_id = Convert.ToInt32(dRw[i]["var_id"]);
                    var = dRw[i].IsNull("var") ? "" : dRw[i]["var"].ToString();
                    
                    contflg = dRw[i].IsNull("isContVar") ? 0 : Convert.ToInt32(dRw[i]["isContVar"]);
                    lvl = Convert.ToInt32(dRw[i]["lvl"]); // kolom Level (penanda lowest level)
                    if (lvl != 0) // Not a variable
                    {
                        fsOut.AppendFormat("<DIV CLASS=\"{1}\" STYLE=\"padding-left:20px;\" onClick=\"sh('dv{0}');\" >", dRw[i]["var_id"].ToString(), cssClass);
                        fsOut.AppendFormat("<IMG SRC=\"images/fplus.gif\" ID=\"dv{0}img\">&nbsp;", var_id);
                        fsOut.Append(desc);
                        fsOut.AppendLine("</DIV>");
                    }
                    else // then it's a variable 
                    {
                        if (_IsContVarOnly)
                            fsOut.AppendFormat("<LABEL FOR=\"zz{0}\" ><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT TYPE=\"checkbox\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}~z\">{1}</DIV></LABEL>", new object[] { var_id, desc });
                        else
                        {
                            if (contflg == 5)       
                                fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new object[] { var_id, desc });
                            else
                            {
                                if (ii == 0 | _SelectedID == var_id.ToString())// ensures 1st var. is always selected
                                    fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" CHECKED NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new object [] { var_id, desc });
                                else
                                    fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new object[] { var_id, desc });

                                ii += 1;
                            }
                        }
                    }

                    // no need to traverse through all nodes, lvl=0 tells us to stop. 
                    if (lvl != 0)
                    {
                        fsOut.AppendFormat("<DIV ID=\"dv{0}\" STYLE=\"padding:0px 0px 5px 20px;display:none;\"> ", var_id);
                        switch (lvl)
                        {
                            case 0:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id + "'", "iFl", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i4", isEnglish))
                            case 1:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id + "'", "iFl2", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i4", isEnglish))
                            case 2:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id + "'", "iFl2", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i3", isEnglish))
                            case 3:
                                {
                                    // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i1", isEnglish))
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id + "'", "iFl2", _IsEnglish));
                                    break;
                                }

                            default:
                                {
                                    fsOut.Append(BuildTreeChildren(table, " var_parent = '" + var_id + "'", "iFl", _IsEnglish));
                                    break;
                                }
                            // fsOut.Append(BuildTreeChildren(table, " var_parent = '" & var_id & "'", "i1", isEnglish))
                        }

                        fsOut.AppendLine("</DIV> ");
                    }


                }
            }

            return fsOut.ToString();
        }


    }
}