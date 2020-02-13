using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.VisualBasic;

namespace gds
{
    public partial class treeComparators : System.Web.UI.UserControl, IGdsTree
    {
        static int i;
        static int j;

        int _DataSetNumber;
        bool _IsEnglish;
        bool _IsSecondLevelVisible = true;
        string _DivContentStyle;
        string _DivTitleStyle;
        string _DivContentClass;
        string _DivTitleClass;
        string _DivContainerStyle;
        string _DivContainerClass;
        string _DivContainerAdditionalAttr;
        string _DivContentAdditionalAttr;
        string _DivTitleAdditionalAttr;
        string _DivTitleString;
        string _DivTitleStringEn;



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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string s = BuildComparatorTree();
                Literal1.Text = s;
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
                //Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
                //Response.Redirect("err.aspx");
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
                //Session["errMsg"] = ex.Message + (char)10 + Request.Url.ToString() + (char)10 + DateTime.Now;
                //Response.Redirect("err.aspx");
            }
        }

        public string BuildComparatorTree()
        {
            System.Text.StringBuilder fsOut = new System.Text.StringBuilder();
            {
                // DivContainer
                fsOut.AppendFormat("<DIV STYLE=\"{0}\" {1} {2}>"
                    , Interaction.IIf(_DivContainerStyle != "", _DivContainerStyle, "cursor:hand")
                    , Interaction.IIf(_DivContainerClass != "", "CLASS=\"" + _DivContainerClass + "\"", "")
                    , _DivContainerAdditionalAttr);

                // DivTitle
                fsOut.AppendFormat("<DIV STYLE=\"{0}\" {1} {2}>"
                    , Interaction.IIf(_DivTitleStyle != "", _DivTitleStyle, "cursor:hand;")
                    , Interaction.IIf(_DivTitleClass != "", "CLASS=\"" + _DivTitleClass + "\"", "CLASS=\"iFl\" ")
                    , Interaction.IIf(_DivTitleAdditionalAttr != "", _DivTitleAdditionalAttr, "onclick=\"sh0('dcroot');\""));

                if (_IsSecondLevelVisible)
                {
                    fsOut.AppendFormat("<IMG SRC=\"images/minus.gif\" ID=\"dcrootimg\">&nbsp;");
                    fsOut.AppendFormat("<IMG SRC=\"images/tree.gif\" ID=\"dcrootimgb\">&nbsp;");
                    fsOut.AppendFormat("{0}</DIV>", Interaction.IIf(_IsEnglish, _DivTitleStringEn, _DivTitleString));
                    // DivContent
                    fsOut.AppendFormat("<DIV STYLE=\"{0}\" {1} ID=\"dcroot\" {2}>"
                        , Interaction.IIf(_DivContentStyle != "", _DivContentStyle, "padding-left:20px;")
                        , Interaction.IIf(_DivContentClass != "", "CLASS=\"" + _DivContentClass + "\"", "")
                        , _DivContentAdditionalAttr);
                }
                else
                {
                    fsOut.AppendFormat("<IMG SRC=\"images/minus.gif\" ID=\"dcrootimg\">&nbsp;");
                    fsOut.AppendFormat("<IMG SRC=\"images/tree.gif\" ID=\"dcrootimgb\">&nbsp;");
                    fsOut.AppendFormat("{0}</DIV>", Interaction.IIf(_IsEnglish, _DivTitleStringEn, _DivTitleString));
                    // DivContent
                    fsOut.AppendFormat("<DIV STYLE=\"{0}\" {1} ID=\"dcroot\" {2}>"
                        , Interaction.IIf(_DivContentStyle != "", _DivContentStyle, "padding-left:20px;display:none;")
                        , Interaction.IIf(_DivContentClass != "", "CLASS=\"" + _DivContentClass + "\"", "")
                        , _DivContentAdditionalAttr);
                }

                // If Not bCont Then
                // If _IsEnglish Then
                // .Append("Comparator<HR>" & vbCrLf)
                // Else
                // .Append("Pembanding<HR>" & vbCrLf)
                // End If

                // End If

                // ******************** MULAI: Get comparators ***************
                // If Not bCont Then
                gdsTable oGt = new gdsTable(_DataSetNumber);
                string sql;
                if (_IsEnglish)
                    sql = "SELECT comp_id,[var],desc_en,subgrp_id,subgrp_en,grp_id,grp_en FROM " + oGt.CompTable + " WHERE isVisible=1 ORDER BY comp_id";
                else
                    sql = "SELECT comp_id,[var],[desc],subgrp_id,subgrp,grp_id,grp FROM  " + oGt.CompTable + "  WHERE isVisible=1 ORDER BY comp_id";
                // If _IsEnglish Then
                // sql = "SELECT comp_id,[var],desc_en,subgrp_id,subgrp_en,grp_id,grp_en FROM gds2_11_comp WHERE isVisible=1 ORDER BY comp_id"
                // Else
                // sql = "SELECT comp_id,[var],[desc],subgrp_id,subgrp,grp_id,grp FROM gds2_11_comp WHERE isVisible=1 ORDER BY comp_id"
                // End If
                DataTable dt;
                dt = commonModule.GetData(commonModule.GetConnString(), sql);

                Hashtable lstGroup = new Hashtable();
                DataRow[] drw;

                foreach (DataRow dr in dt.Rows)
                {
                    i += 1;
                    if (!lstGroup.Contains(dr["grp_id"]))
                    {
                        if (_IsEnglish)
                        {
                            lstGroup.Add(dr["grp_id"], dr["grp_en"]);
                            drw = dt.Select("grp_en='" + lstGroup[dr["grp_id"]] + "'");
                        }
                        else
                        {
                            lstGroup.Add(dr["grp_id"], dr["grp"]);
                            drw = dt.Select("grp='" + lstGroup[dr["grp_id"]] + "'");
                        }

                        // If i = 1 Then 'shows 1st. comp.
                        // .AppendFormat("<DIV  onClick=""sh('dc{0}');"" STYLE=""cursor:hand;padding-bottom:4px;""><IMG SRC=""images/fmin.gif"" ID=""dc{0}img""><B>{1}</B></DIV>", dr("grp_id"), lstGroup(dr("grp_id"))) 'lstGroup(dr("grp_id")) will show value-nya
                        // .AppendFormat("<DIV ID=""dc{0}"" STYLE=""position:relative;left:15px;"" >", dr("grp_id"))
                        // Else
                        // .AppendFormat("<DIV  onClick=""sh('dc{0}');""  STYLE=""cursor:hand;padding-bottom:4px;""><IMG SRC=""images/plus.gif"" ID=""dc{0}img"">&nbsp;&nbsp;<B>{1}</B></DIV>", dr("grp_id"), lstGroup(dr("grp_id"))) 'lstGroup(dr("grp_id")) will show value-nya   'uses dua img
                        fsOut.AppendFormat("<DIV CLASS=\"iFl\"  onClick=\"sh('dc{0}');\"  STYLE=\"cursor:hand;padding-left:20px;\"><IMG SRC=\"images/fplus.gif\" ID=\"dc{0}img\"><B>{1}</B></DIV>", dr["grp_id"], lstGroup[dr["grp_id"]]); // lstGroup(dr("grp_id")) will show value-nya
                        fsOut.AppendFormat("<DIV ID=\"dc{0}\" STYLE=\"padding-left:50px;display:none;\" >", dr["grp_id"]);
                        // End If
                        Hashtable lstSubGroup = new Hashtable();
                        foreach (DataRow drSubGroup in drw)
                        {
                            j += 1;
                            if (!lstSubGroup.Contains(drSubGroup["subgrp_id"]))
                            {
                                if (_IsEnglish)
                                    lstSubGroup.Add(drSubGroup["subgrp_id"], drSubGroup["subgrp_en"]);
                                else
                                    lstSubGroup.Add(drSubGroup["subgrp_id"], drSubGroup["subgrp"]);
                                if (j <= 4 & i == 1)
                                    fsOut.AppendFormat("<LABEL FOR=\"cp{0}\"><DIV STYLE=\"font-size:8pt;\"><INPUT TYPE=\"CHECKBOX\" ID=\"cp{0}\" NAME=\"cp{0}\" VALUE=\"1\" CHECKED>{1}</DIV></LABEL>", drSubGroup["subgrp_id"], lstSubGroup[drSubGroup["subgrp_id"]]);
                                else
                                    fsOut.AppendFormat("<LABEL FOR=\"cp{0}\"><DIV STYLE=\"font-size:8pt;\"><INPUT TYPE=\"CHECKBOX\" ID=\"cp{0}\" NAME=\"cp{0}\" VALUE=\"1\">{1}</DIV></LABEL>", drSubGroup["subgrp_id"], lstSubGroup[drSubGroup["subgrp_id"]]);
                            }
                        } // For Each drSubGroup As DataRow In drw
                        fsOut.AppendLine("</DIV>");
                    }
                    // ****** shows more comparators only for household (gsd11)
                    if (_DataSetNumber != 11 & i > 1)
                        break;
                } // For Each dr As DataRow In dt.Rows
                // End If
                // *************** SELESAI: Get Comparator ***************


                // .Append("<INPUT TYPE=""hidden"" NAME=""ds"" VALUE=""" & _DataSetNumber & """>" & vbCrLf)
                // If _IsEnglish Then
                // .Append("<INPUT CLASS=""hnd"" TYPE=""checkbox"" NAME=""ch"" ID=""ch"" VALUE=""1"" CHECKED><LABEL FOR=""ch"">Plot with Chart?</LABEL>" & vbCrLf)
                // Else
                // .Append("<INPUT CLASS=""hnd"" TYPE=""checkbox"" NAME=""ch"" ID=""ch"" VALUE=""1"" CHECKED><LABEL FOR=""ch"">Tampilkan Chart?</LABEL>" & vbCrLf)
                // End If
                // .Append("<BR><BR>" & vbCrLf)
                // .Append("<CENTER>" & vbCrLf)

                // If _IsEnglish Then
                // .Append("<INPUT CLASS=""hnd"" TYPE=""button"" VALUE=""<< Back"" ONCLICK=""history.go(-1);"">" & vbCrLf)
                // Else
                // .Append("<INPUT CLASS=""hnd"" TYPE=""button"" VALUE=""<< Kembali"" ONCLICK=""history.go(-1);"">" & vbCrLf)
                // End If

                // .Append("&nbsp;&nbsp;&nbsp;" & vbCrLf)

                // If _IsEnglish Then
                // .Append("<INPUT ID=""cmdOK"" CLASS=""hnd"" TYPE=""Submit"" VALUE=""Next >>"">" & vbCrLf)
                // Else
                // .Append("<INPUT ID=""cmdOK"" CLASS=""hnd"" TYPE=""Submit"" VALUE=""Lanjut >>"">" & vbCrLf)
                // End If

                // .Append("</CENTER>" & vbCrLf)

                // If Not bCont Then
                // .Append("<TR BGCOLOR=""#d8bfd8"" & vbCrLf><TD STYLE=""padding:5px 0px 5px 5px;"" onmouseover=""this.style.background='#FFFF00';""  onmouseout=""this.style.background='#d8bfd8';"">" & vbCrLf)
                // .Append("<A CLASS=""a1"" HREF=""javascript:dsel(document.forms['frmVar']);"">Analyze selected variable for other district</A>" & vbCrLf)
                // .Append("</TD></TR>" & vbCrLf)
                // End If

                // .Append("<TR BGCOLOR=""#d8bfd8"" & vbCrLf><TD STYLE=""padding:5px 0px 5px 5px;"" onmouseover=""this.style.background='#FFFF00';""  onmouseout=""this.style.background='#d8bfd8';"">" & vbCrLf)
                // .Append("<A CLASS=""a1"" HREF=""dist.aspx?ds=" & iDs & """>Back to the District Selection page</A>" & vbCrLf)
                // .Append("</TD></TR>" & vbCrLf)

                // If Not bCont Then
                // .Append("<FORM NAME=""f2"" ACTION=""dist2.aspx"" METHOD=""GET"">" & vbCrLf)
                // .Append("<INPUT TYPE=""hidden"" NAME=""ds"" VALUE=""" & iDs & """>" & vbCrLf)
                // .Append("<INPUT TYPE=""hidden"" NAME=""v"" VALUE="""">" & vbCrLf)
                // '.Append("<INPUT TYPE=""hidden"" NAME=""d"" VALUE=""" & sDist & """>" & vbCrLf)
                // '.Append("<INPUT TYPE=""hidden"" NAME=""r"" VALUE=""" & sRegn & """>" & vbCrLf)
                // .Append("</FORM>" & vbCrLf)
                // End If
                fsOut.AppendLine("</DIV>"); // END OF DivContent
                fsOut.AppendLine("</DIV>"); // END OF DivContainer
            }
            return fsOut.ToString();
        }


    }
}