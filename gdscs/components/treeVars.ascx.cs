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

        static int ii;

        public string SelectedID
        {
            get
            {
                return _SelectedID;
            }
            set
            {
                _SelectedID = value;
            }
        }

        public bool IsContVarOnly
        {
            get
            {
                return _IsContVarOnly;
            }
            set
            {
                _IsContVarOnly = value;
            }
        }

        public bool IsEnglish
        {
            get
            {
                return _IsEnglish;
            }
            set
            {
                _IsEnglish = value;
            }
        }

        public int DatasetNumber
        {
            get
            {
                return _DataSetNumber;
            }
            set
            {
                _DataSetNumber = value;
            }
        }

        public bool IsSecondLevelVisible
        {
            get
            {
                return _IsSecondLevelVisible;
            }
            set
            {
                _IsSecondLevelVisible = value;
            }
        }

        public string DivContainerClass
        {
            get
            {
                return _DivContainerClass;
            }
            set
            {
                _DivContainerClass = value;
            }
        }

        public string DivContainerStyle
        {
            get
            {
                return _DivContainerStyle;
            }
            set
            {
                _DivContainerStyle = value;
            }
        }

        public string DivContentStyle
        {
            get
            {
                return _DivContentStyle;
            }
            set
            {
                _DivContentStyle = value;
            }
        }

        public string DivTitleStyle
        {
            get
            {
                return _DivTitleStyle;
            }
            set
            {
                _DivTitleStyle = value;
            }
        }

        public string DivContentClass
        {
            get
            {
                return _DivContentClass;
            }
            set
            {
                _DivContentClass = value;
            }
        }

        public string DivTitleString
        {
            get
            {
                return _DivTitleString;
            }
            set
            {
                _DivTitleString = value;
            }
        }

        public string DivTitleStringEn
        {
            get
            {
                return _DivTitleStringEn;
            }
            set
            {
                _DivTitleStringEn = value;
            }
        }

        public string DivTitleClass
        {
            get
            {
                return _DivTitleClass;
            }
            set
            {
                _DivTitleClass = value;
            }
        }

        public string DivContainerAdditionalAttr
        {
            get
            {
                return _DivContainerAdditionalAttr;
            }
            set
            {
                _DivContainerAdditionalAttr = value;
            }
        }

        public string DivContentAdditionalAttr
        {
            get
            {
                return _DivContentAdditionalAttr;
            }
            set
            {
                _DivContentAdditionalAttr = value;
            }
        }

        public string DivTitleAdditionalAttr
        {
            get
            {
                return _DivTitleAdditionalAttr;
            }
            set
            {
                _DivTitleAdditionalAttr = value;
            }
        }

        public gdsTable GdsTable
        {
            get
            {
                return _GdsTable;
            }
            set
            {
                _GdsTable = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Literal1.Text = BuildVarTreeRoots(_IsEnglish);
            }
            catch (SqlException ex)
            {
                Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
                Response.Redirect("err.aspx");
            }
            catch (Exception ex)
            {
                Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
                System.Web.HttpContext.Current.Response.Redirect("err.aspx");
            }
        }

        private string BuildVarTreeRoots(bool isEnglish)
        {
            sl = new ArrayList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(string.Empty);
            // Dim i As Integer
            // Dim sDim As String


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
                if (!sl.Contains(dRw[i]["var"]))
                {
                    if (_IsEnglish)
                    {
                        desc = dRw[i].IsNull("desc_en") ? dRw[i]["desc"].ToString() : dRw[i]["desc_en"].ToString();
                    }
                    else
                    {
                        desc = dRw[i].IsNull("desc") ? "" : dRw[i]["desc"].ToString(); 
                    }

                    sl.Add(dRw[i]["var"]);
                    var_id = Convert.ToInt32(dRw[i]["var"]);
                    var = dRw[i].IsNull("var")? "": dRw[i]["var"].ToString();
                    contflg = dRw[i].IsNull("isContVar") ? 0 : Convert.ToInt32(dRw[i]["isContVar"]);
                    lvl = Convert.ToInt32(dRw[i]["lvl"]); // kolom Level (penanda lowest level)
                    if (lvl != 0) // Not a variable
                    {
                        // fsOut.AppendFormat("<DIV CLASS=""{1}"" STYLE=""padding-left:20px;"" onClick=""shf('dd{0}');"" onmouseover=""ca();"" onmouseout=""ca();"">", dRw(i)("var_id"), cssClass)    'uses dua img
                        // fsOut.AppendFormat("<IMG SRC=""images/plus.gif"" ID=""dd{0}imga"">&nbsp;&nbsp;<IMG SRC=""images/fc.gif"" ID=""dd{0}imgb"">&nbsp;", var_id)    'uses dua img

                        fsOut.AppendFormat("<DIV CLASS=\"{1}\" STYLE=\"padding-left:20px;\" onClick=\"sh('dv{0}');\" >", dRw[i]["var"], cssClass);
                        // TODO: Nantinya TABLE hrs diganti pake UL LI
                        // fsOut.AppendFormat("<TABLE BORDER=""0"" CELLPADDING=""0"" CELLSPACING=""0"">")
                        // fsOut.AppendFormat("<TR><TD VALIGN=""top"" WIDTH=""34"">")
                        fsOut.AppendFormat("<IMG SRC=\"images/fplus.gif\" ID=\"dv{0}img\">&nbsp;", var_id);
                        // fsOut.AppendFormat("</TD><TD CLASS=""{0}"" STYLE=""padding:0 0 0 0;"">", cssClass)
                        fsOut.Append(desc);
                        // fsOut.AppendFormat("</TD></TR>")
                        // fsOut.AppendFormat("</TABLE>")
                        fsOut.AppendLine("</DIV>");
                    }
                    else // then it's a variable 
                    {
                        if (_IsContVarOnly)
                        {
                            // ************* Ensures 1st & 2nd var. is always selected
                            // Static ij As Integer = 0
                            // If ij < 2 Then
                            // fsOut.AppendFormat("<LABEL FOR=""zz{0}""><DIV CLASS=""i4"" STYLE=""padding-left:20px;"" ><INPUT TYPE=""checkbox"" CHECKED ID=""zz{0}"" NAME=""v"" VALUE=""{1}~{2}~{3}~{4}~{5}~{0}~zzzz"" >{7}</DIV></LABEL>", New String() {var_id, "", "", "", contflg, exstr, "", desc})
                            // Else
                            fsOut.AppendFormat("<LABEL FOR=\"zz{0}\" ><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT TYPE=\"checkbox\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}~z\">{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                        }
                        // End If
                        // ij += 1
                        // ************* END OF Ensures 1st & 2nd var. is always selected
                        else
                        {
                            if (contflg == 5)       // Use Stored Proc?
                            {
                                fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                            }
                            else
                            {
                                ;
                                /* Cannot convert LocalDeclarationStatementSyntax, System.NotSupportedException: StaticKeyword not supported!
                           at ICSharpCode.CodeConverter.CSharp.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
                           at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertModifier(SyntaxToken m, TokenContext context) in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\CommonConversions.cs:line 358
                           at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertModifiersCore(Accessibility declaredAccessibility, IEnumerable`1 modifiers, TokenContext context)+MoveNext() in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\CommonConversions.cs:line 373
                           at System.Linq.Enumerable.ConcatIterator`1.MoveNext()
                           at System.Linq.Enumerable.WhereEnumerableIterator`1.ToArray()
                           at System.Linq.OrderedEnumerable`1.GetEnumerator()+MoveNext()
                           at Microsoft.CodeAnalysis.SyntaxTokenList.CreateNode(IEnumerable`1 tokens)
                           at Microsoft.CodeAnalysis.SyntaxTokenList..ctor(IEnumerable`1 tokens)
                           at Microsoft.CodeAnalysis.CSharp.SyntaxFactory.TokenList(IEnumerable`1 tokens)
                           at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertModifiers(SyntaxNode node, IReadOnlyCollection`1 modifiers, TokenContext context, Boolean isVariableOrConst, SyntaxKind[] extraCsModifierKinds) in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\CommonConversions.cs:line 326
                           at ICSharpCode.CodeConverter.CSharp.MethodBodyExecutableStatementVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node) in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\MethodBodyExecutableStatementVisitor.cs:line 89
                           at ICSharpCode.CodeConverter.CSharp.ByRefParameterVisitor.CreateLocals(VisualBasicSyntaxNode node) in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\ByRefParameterVisitor.cs:line 53
                           at ICSharpCode.CodeConverter.CSharp.ByRefParameterVisitor.AddLocalVariables(VisualBasicSyntaxNode node) in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\ByRefParameterVisitor.cs:line 43
                           at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisitInnerAsync(SyntaxNode node) in D:\GitWorkspace\CodeConverter\ICSharpCode.CodeConverter\CSharp\CommentConvertingMethodBodyVisitor.cs:line 32

                        Input:
                                                    Static ii As Integer = 0

                         */
                                if (ii == 0 | _SelectedID == var_id.ToString()) // ensures 1st var. is always selected
                                {
                                    fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" CHECKED NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                                }
                                else
                                {
                                    fsOut.AppendFormat("<LABEL FOR=\"zz{0}\"><DIV CLASS=\"i4\" STYLE=\"padding-left:30px;\" ><INPUT  TYPE=\"radio\" ID=\"zz{0}\" NAME=\"v\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { var_id.ToString(), desc });
                                }

                                ii += 1;
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

                    // *************** END OF Percobaan

                }
            }

            return fsOut.ToString();
        }

    }
}