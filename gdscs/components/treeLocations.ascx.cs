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
            get { return _SelectedID; }
            set { _SelectedID = value; }
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
		protected void Page_Load(object sender, EventArgs e)
		{
            Literal1.Text = BuildLocationTree();
            //try
            //{
            //    Literal1.Text = BuildLocationTree();
            //}
            //catch (Exception ex)
            //{
            //    commonModule.RedirectError(ex);
            //}
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


			{

				// DivContainer
                fsOut.AppendFormat("<div style=\"{0}\" {1} {2}>"
					, (_DivContainerStyle != ""? _DivContainerStyle: "cursor:hand")
					, (_DivContainerClass != ""? "class=\"" + _DivContainerClass + "\"": "")
					, _DivContainerAdditionalAttr);

				// DivTitle
                fsOut.AppendFormat("<div style=\"{0}\" {1} {2}>"
					, (_DivTitleStyle != ""? _DivTitleStyle: "cursor:hand;")
					, (_DivTitleClass != ""? "class=\"" + _DivTitleClass + "\"": "class=\"iFl\" ")
					, (_DivTitleAdditionalAttr != "" ? _DivTitleAdditionalAttr : "onclick=\"sh0('dlroot');\""));
				if (_IsSecondLevelVisible)
				{
                    fsOut.AppendFormat("<img src=\"images/minus.gif\" ID=\"dlrootimg\">&nbsp;");
                    fsOut.AppendFormat("<img src=\"images/tree.gif\" ID=\"dlrootimgb\">&nbsp;");
                    fsOut.Append(_IsEnglish ? _DivTitleStringEn : _DivTitleString);
                    fsOut.Append("&nbsp;&nbsp;&nbsp;");
                    fsOut.AppendFormat("</div>");
					// END OF DivTitle

					// DivContent
                    fsOut.AppendFormat("<div style=\"{0}\" {1} ID=\"dlroot\" {2}>"
						, (_DivContentStyle != ""? _DivContentStyle: "padding-left:30px;")
						, (_DivContentClass != ""? "class=\"" + _DivContentClass + "\"": "")
						, _DivContentAdditionalAttr);
				}
				else
				{
                    fsOut.AppendFormat("<img src=\"images/plus.gif\" ID=\"dlrootimg\">&nbsp;");
                    fsOut.AppendFormat("<img src=\"images/tree.gif\" ID=\"dlrootimgb\">&nbsp;");
                    fsOut.AppendFormat(_IsEnglish ? _DivTitleStringEn : _DivTitleString);
                    fsOut.Append("&nbsp;&nbsp;&nbsp;");
                    fsOut.AppendFormat("</div>");
					// END OF DivTitle

					// DivContent
                    fsOut.AppendFormat("<div style=\"{0}\" {1} ID=\"dlroot\" {2}>"
						, (_DivContentStyle != ""? _DivContentStyle: "padding-left:30px;display:none;")
						, (_DivContentClass != ""? "class=\"" + _DivContentClass + "\"": "")
						, _DivContentAdditionalAttr);
				}


				for (int i = 0; i < dtGroup.Rows.Count ; i++)
				{
					sGroup = dtGroup.Rows[i]["grpnm"].ToString();
                    fsOut.AppendLine();
                    fsOut.AppendLine();
					if (_IsEnglish)
					{
						// sOut &= "<div class=""iFl""  title=""Click to see all provinces in " & sGroup & """ onClick=""shf('dd" & i & "');"">" & vbCrLf 'uses dua img
                        fsOut.AppendFormat("<div class=\"iFl\"  title=\"Click to see all provinces in " + sGroup + "\" onClick=\"sh('dl" + i + "');\">");
                        fsOut.AppendLine();
					}
					else
					{
						// sOut &= "<div class=""iFl""  title=""Klik untuk melihat semua propinsi di " & sGroup & """ onClick=""shf('dd" & i & "');"">" & vbCrLf  'uses dua img
                        fsOut.AppendFormat("<div class=\"iFl\"  title=\"Klik untuk melihat semua propinsi di " + sGroup + "\" onClick=\"sh('dl" + i + "');\">");
                        fsOut.AppendLine();
						// sOut &= "   <img src=""images/plus.gif"" ID=""dd" & i & "imga"" >&nbsp;<img src=""images/fc.gif"" ID=""dd" & i & "imgb"" >&nbsp;"  'uses dua img

					}
                    fsOut.AppendFormat("   <img src=\"images/fplus.gif\" ID=\"dl" + i + "img\" >&nbsp;");

                    fsOut.AppendLine(sGroup);
                    fsOut.AppendLine("</div>");
					dRowProv = dtProv.Select("grp = '" + dtGroup.Rows[i]["grp"] + "'");

                    fsOut.AppendFormat("<DIV ID=\"dl" + i + "\" style=\"display:none;margin-left:30px;\">");
                    fsOut.AppendLine();

					for (int j = 0; j < dRowProv.Length ; j++)
					{
						sProv = dRowProv[j]["prov"].ToString();
						sProvNm = dRowProv[j]["provnm"].ToString();
                        fsOut.AppendLine();
                        fsOut.AppendLine();
						if (_IsEnglish)
						{
                            fsOut.AppendFormat("   <div class=\"iFl2\" title=\"Click to see all districts in " + sProvNm + "\"  onClick=\"sh('dll" + i + j + "');\">");
                            fsOut.AppendLine();
						}
						else
						{
                            fsOut.AppendFormat("   <div class=\"iFl2\" title=\"Klik untuk melihat semua kabupaten di " + sProvNm + "\"  onClick=\"sh('dll" + i + j + "');\">");
							// sOut &= "      <img src=""images/plus.gif"" ID=""ddd" & i & j & "imga"" >&nbsp;<img src=""images/fc.gif"" ID=""ddd" & i & j & "imgb"" >&nbsp;" 'uses dua img
                            fsOut.AppendLine();
						}
                        fsOut.AppendLine("      <img src=\"images/fplus.gif\" ID=\"dll" + i + j + "img\" >&nbsp;");
                        fsOut.AppendLine(sProvNm);
                        fsOut.AppendLine("   </div>");
						dRowKabu = dtKabu.Select("prov = '" + dRowProv[j]["prov"] + "'");

                        fsOut.AppendLine("   <DIV ID=\"dll" + i + j + "\" style=\"display:none;margin-left:30px;\">");

						if (_IsEnglish)
						{
							if (_SelectedID == sProv + "~All")
                                fsOut.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" CHECKED><B>All of the districts</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
							else
                                fsOut.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" ><B>All of the districts</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
						}
						else if (_SelectedID == sProv + "~All")
                            fsOut.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" CHECKED><B>Seluruh kabupaten</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });
						else
                            fsOut.AppendFormat("     <label for=\"all{2}\"><div class=\"i4\"><input type=\"radio\" ID=\"all{2}\" name=\"l\" VALUE=\"{2}~{1}\" ><B>Seluruh kabupaten</B></DIV></LABEL>", new string[] { j.ToString(), "All", sProv });

                        for (int k = 0; k < dRowKabu.Length; k++)
						{
							sKabu = dRowKabu[k]["kabu"].ToString();
							sKabuNm = dRowKabu[k]["kabunm"].ToString();
							if (_SelectedID == sProv + "~" + sKabu)
                                fsOut.AppendFormat("     <label for=\"d{0}\"><div class=\"i4\"><input type=\"radio\" ID=\"d{0}\" name=\"l\" VALUE=\"{0}\" CHECKED>{1}</DIV></LABEL>", new string[] { sProv + "~" + sKabu, sKabuNm });
							else
                                fsOut.AppendFormat("     <label for=\"d{0}\"><div class=\"i4\"><input type=\"radio\" ID=\"d{0}\" name=\"l\" VALUE=\"{0}\" >{1}</DIV></LABEL>", new string[] { sProv + "~" + sKabu, sKabuNm });
                            fsOut.AppendLine();
						}
                        fsOut.AppendLine();
                        fsOut.AppendLine("   </div>");
					}
                    fsOut.AppendLine();
                    fsOut.AppendLine("</div>");
				}
				if (_SelectedID == "All~Natl")
                    fsOut.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\" checked=\"checked\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish ? "National" : "Nasional") }));
				else if (_SelectedID == "")
                    fsOut.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\" checked=\"checked\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish ? "National" : "Nasional") }));
				else if (SelectedID == "All")
                    fsOut.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\" checked=\"checked\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish ? "National" : "Nasional") }));
				else
                    fsOut.AppendFormat(string.Format("<label for=\"N_{2}\"><div class=\"iFl\" style=\"padding-left:20px;\"><input type=\"radio\" ID=\"N_{2}\" name=\"l\" value=\"{0}~{1}\">{2}</div></label>", new string[] { "All", "Natl", (_IsEnglish ? "National" : "Nasional") }));

                fsOut.AppendFormat("</div>");
                fsOut.AppendFormat("</div>"); // END OF DivContent

                fsOut.AppendFormat("</div>"); // END OF DivContainer
			}
			return fsOut.ToString();
		}

	}
}