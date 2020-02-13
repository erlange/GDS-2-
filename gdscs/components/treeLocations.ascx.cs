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
	public partial class treeLocations : System.Web.UI.UserControl, IGdsTree
	{
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
		string _SelectedID;
		string _DivTitleString;
		string _DivTitleStringEn;


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
				Literal1.Text = BuildLocationTree();
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

		public string BuildLocationTree()
		{
			System.Text.StringBuilder fsOut = new System.Text.StringBuilder();
			string sProvNm = "";
			string sProv = "";
			string sKabuNm = "";
			string sKabu = "";
			string sGroup = "";
			DataRow[] dRowProv, dRowKabu;
			string sConn = commonModule.GetConnString();
			string sqlGroup = "SELECT grp,grpnm FROM t00grp ORDER BY grp ASC";
			string sqlProv = "SELECT prov, grp,provnm FROM t01prov ORDER BY prov ASC";
			string sqlKabu = "SELECT kabu, prov, kabunm FROM t02kabu ORDER BY prov,kabu ASC";
			DataTable dtGroup, dtProv, dtKabu;
			dtGroup = commonModule.GetData(sConn, sqlGroup);
			dtProv = commonModule.GetData(sConn, sqlProv);
			dtKabu = commonModule.GetData(sConn, sqlKabu);


			// ****************** Shows/hides all districts****************** 
			// sOut &= "<div style=""margin-left:20px;padding: 2px 2px 2px 2px;width:280px;cursor:hand;"" onSelectStart=""return false;"" onmouseover=""ca();"" onmouseout=""ca();"">" & vbCrLf
			// sOut &= "<div class=""i1"" ID=""vv""  "
			// sOut &= " onClick = "" "

			// For i As Integer = 0 To dtGroup.Rows.Count - 1
			// sOut &= "shf('dd"
			// sOut &= i
			// sOut &= "');"
			// For j As Integer = 0 To dtProv.Select("grp = '" & dtGroup.Rows(i)("grp") & "'").Length - 1
			// sOut &= "shf('ddd"
			// sOut &= i & j
			// sOut &= "');"
			// Next
			// Next

			// sOut &= "vv.style.display='none';hh.style.display='';"" > "
			// sOut &= IIf(bEn, "Expand All", "Tampilkan Seluruh Kabupaten")
			// sOut &= "</div>"

			// sOut &= vbCrLf
			// sOut &= "<div class=""i1"" ID=""hh"" style=""display:none;"" "
			// sOut &= " onClick = """

			// 'For i As Integer = 0 To dtGroup.Rows.Count - 1
			// '    sOut &= "hide('dd"
			// '    sOut &= i
			// '    sOut &= "');"
			// 'Next
			// For i As Integer = 0 To dtGroup.Rows.Count - 1
			// sOut &= "shf('dd"
			// sOut &= i
			// sOut &= "');"
			// For j As Integer = 0 To dtProv.Select("grp = '" & dtGroup.Rows(i)("grp") & "'").Length - 1
			// sOut &= "shf('ddd"
			// sOut &= i & j
			// sOut &= "');"
			// Next
			// Next

			// sOut &= "vv.style.display='';hh.style.display='none';"" > "
			// sOut &= IIf(bEn, "Hide All Districts", "Tutup Semua Kabupaten")
			// sOut &= "</div>"
			// ****************** END OF Shows/hides all districts****************** 
			{
				var withBlock = fsOut;

				// DivContainer
				withBlock.AppendFormat("<div style=\"{0}\" {1} {2}>"
					, (_DivContainerStyle != ""? _DivContainerStyle: "cursor:hand")
					, (_DivContainerClass != ""? "class=\"" + _DivContainerClass + "\"": "")
					, _DivContainerAdditionalAttr);

				// DivTitle
				withBlock.AppendFormat("<div style=\"{0}\" {1} {2}>"
					, (_DivTitleStyle != ""? _DivTitleStyle: "cursor:hand;")
					, (_DivTitleClass != ""? "class=\"" + _DivTitleClass + "\"": "class=\"iFl\" ")
					, (_DivTitleAdditionalAttr != "" ? _DivTitleAdditionalAttr : "onclick=\"sh0('dlroot');\""));
				if (_IsSecondLevelVisible)
				{
					withBlock.AppendFormat("<img src=\"images/minus.gif\" ID=\"dlrootimg\">&nbsp;");
					withBlock.AppendFormat("<img src=\"images/tree.gif\" ID=\"dlrootimgb\">&nbsp;");
					withBlock.Append(_IsEnglish? _DivTitleStringEn: _DivTitleString);
					withBlock.Append("&nbsp;&nbsp;&nbsp;");
					withBlock.AppendFormat("</div>");
					// END OF DivTitle

					// DivContent
					withBlock.AppendFormat("<div style=\"{0}\" {1} ID=\"dlroot\" {2}>"
						, (_DivContentStyle != ""? _DivContentStyle: "padding-left:30px;")
						, (_DivContentClass != ""? "class=\"" + _DivContentClass + "\"": "")
						, _DivContentAdditionalAttr);
				}
				else
				{
					withBlock.AppendFormat("<img src=\"images/plus.gif\" ID=\"dlrootimg\">&nbsp;");
					withBlock.AppendFormat("<img src=\"images/tree.gif\" ID=\"dlrootimgb\">&nbsp;");
					withBlock.AppendFormat(_IsEnglish ? _DivTitleStringEn : _DivTitleString);
					withBlock.Append("&nbsp;&nbsp;&nbsp;");
					// If _IsEnglish Then
					// .AppendFormat("<img id=""imgL"" src=""images/help.gif"" border=""0"" onmouseover=""ci('tip','{0}')"" onmousemove=""cm('tip')"" onmouseout=""ch('tip')"">", "Select one province/district from the list below.")
					// Else
					// .AppendFormat("<img id=""imgL"" src=""images/help.gif"" border=""0"" onmouseover=""ci('tip','{0}')"" onmousemove=""cm('tip')"" onmouseout=""ch('tip')"">", "Pilih salah satu kabupaten/propinsi dari daftar pilihan berikut.")
					// End If
					withBlock.AppendFormat("</div>");
					// END OF DivTitle

					// DivContent
					withBlock.AppendFormat("<div style=\"{0}\" {1} ID=\"dlroot\" {2}>"
						, (_DivContentStyle != ""? _DivContentStyle: "padding-left:30px;display:none;")
						, (_DivContentClass != ""? "class=\"" + _DivContentClass + "\"": "")
						, _DivContentAdditionalAttr);
				}


				for (int i = 0; i <= dtGroup.Rows.Count - 1; i++)
				{
					sGroup = dtGroup.Rows[i]["grpnm"].ToString();
					withBlock.AppendLine();
					withBlock.AppendLine();
					if (_IsEnglish)
					{
						// sOut &= "<div class=""iFl""  title=""Click to see all provinces in " & sGroup & """ onClick=""shf('dd" & i & "');"">" & vbCrLf 'uses dua img
						withBlock.AppendFormat("<div class=\"iFl\"  title=\"Click to see all provinces in " + sGroup + "\" onClick=\"sh('dl" + i + "');\">");
						withBlock.AppendLine();
					}
					else
					{
						// sOut &= "<div class=""iFl""  title=""Klik untuk melihat semua propinsi di " & sGroup & """ onClick=""shf('dd" & i & "');"">" & vbCrLf  'uses dua img
						withBlock.AppendFormat("<div class=\"iFl\"  title=\"Klik untuk melihat semua propinsi di " + sGroup + "\" onClick=\"sh('dl" + i + "');\">");
						withBlock.AppendLine();
						// sOut &= "   <img src=""images/plus.gif"" ID=""dd" & i & "imga"" >&nbsp;<img src=""images/fc.gif"" ID=""dd" & i & "imgb"" >&nbsp;"  'uses dua img

					}
					withBlock.AppendFormat("   <img src=\"images/fplus.gif\" ID=\"dl" + i + "img\" >&nbsp;");

					withBlock.AppendLine(sGroup);
					withBlock.AppendLine("</div>" );
					dRowProv = dtProv.Select("grp = '" + dtGroup.Rows[i]["grp"] + "'");

					withBlock.AppendFormat("<DIV ID=\"dl" + i + "\" style=\"display:none;margin-left:30px;\">");
					withBlock.AppendLine();

					for (int j = 0; j <= dRowProv.Length - 1; j++)
					{
						sProv = dRowProv[j]["prov"].ToString();
						sProvNm = dRowProv[j]["provnm"].ToString();
						withBlock.AppendLine();
						withBlock.AppendLine();
						if (_IsEnglish)
						{
							withBlock.AppendFormat("   <div class=\"iFl2\" title=\"Click to see all districts in " + sProvNm + "\"  onClick=\"sh('dll" + i + j + "');\">" );
							withBlock.AppendLine();
						}
						else
						{
							withBlock.AppendFormat("   <div class=\"iFl2\" title=\"Klik untuk melihat semua kabupaten di " + sProvNm + "\"  onClick=\"sh('dll" + i + j + "');\">" );
							// sOut &= "      <img src=""images/plus.gif"" ID=""ddd" & i & j & "imga"" >&nbsp;<img src=""images/fc.gif"" ID=""ddd" & i & j & "imgb"" >&nbsp;" 'uses dua img
							withBlock.AppendLine();
						}
						withBlock.AppendLine("      <img src=\"images/fplus.gif\" ID=\"dll" + i + j + "img\" >&nbsp;");
						withBlock.AppendLine(sProvNm);
						withBlock.AppendLine("   </div>");
						dRowKabu = dtKabu.Select("prov = '" + dRowProv[j]["prov"] + "'");

						withBlock.AppendLine("   <DIV ID=\"dll" + i + j + "\" style=\"display:none;margin-left:30px;\">");

						if (_IsEnglish)
						{
							if (_SelectedID == sProv + "~All")
								withBlock.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" CHECKED><B>All of the districts</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
							else
								withBlock.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" ><B>All of the districts</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
						}
						else if (_SelectedID == sProv + "~All")
							withBlock.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" CHECKED><B>Seluruh kabupaten</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
						else
							withBlock.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" ><B>Seluruh kabupaten</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
						for (int k = 0; k <= dRowKabu.Length - 1; k++)
						{
							sKabu = dRowKabu[k]["kabu"].ToString();
							sKabuNm = dRowKabu[k]["kabunm"].ToString();
							if (_SelectedID == sProv + "~" + sKabu)
								withBlock.AppendFormat("     <label for=\"d{0}\"><div class=\"i4\"><input type=\"radio\" ID=\"d{0}\" name=\"l\" VALUE=\"{0}\" CHECKED>{1}</DIV></LABEL>", new string[] { sProv + "~" + sKabu, sKabuNm });
							else
								withBlock.AppendFormat("     <label for=\"d{0}\"><div class=\"i4\"><input type=\"radio\" ID=\"d{0}\" name=\"l\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { sProv + "~" + sKabu, sKabuNm });
							withBlock.AppendLine();
						}
						withBlock.AppendLine();
						withBlock.AppendLine("   </div>");
					}
					withBlock.AppendLine();
					withBlock.AppendLine("</div>");
				}
				if (_SelectedID == "All~Natl")
					withBlock.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\" checked=\"checked\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish? "National": "Nasional") }));
				else if (_SelectedID == "")
					withBlock.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\" checked=\"checked\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish? "National": "Nasional") }));
				else if (SelectedID == "All")
					withBlock.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\" checked=\"checked\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish? "National": "Nasional") }));
				else
					withBlock.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish ? "National" : "Nasional") }));

				withBlock.AppendFormat("</div>");
				withBlock.AppendFormat("</div>"); // END OF DivContent

				withBlock.AppendFormat("</div>"); // END OF DivContainer
			}
			return fsOut.ToString();
		}

	}
}