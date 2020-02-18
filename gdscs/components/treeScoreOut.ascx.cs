using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using ChartDirector;
using System.Text;

namespace gds
{
    public partial class treeScoreOut : System.Web.UI.UserControl
    {
        private const int INTLEGENDHEIGHT = 16;
        private const int INTBOTTOMTEXTHEIGHT = 20;
        private const int INTBASEHEIGHT = 400;
        private const float INTRATIO = 0.15F;
        private int[] INTGRAD = new[] { 0, 0xFFCCFF, 128, 0xFFFFFF, 256, 0xFFCCFF };
        private int[] INTGRADBLEU = new[] { 0, 0xFFFFFF, 128, 0x9999CC, 255, 0xFFFFFF };
        private int[] INTGRADBLEUINV = new[] { 0, 0x9999CC, 128, 0xFFFFFF, 255, 0x9999CC };
        private int[] INTGRADBLEU2 = new[] { 0, 0x5555CC, 128, 0xFFFFFF, 255, 0x5555CC };
        private int[] INTGRADBLEU2INV = new[] { 0, 0x5555CC, 128, 0xFFFFFF, 255, 0x5555CC };
        private int[] INTGRADROUGE = new[] { 0, 0xFFFFFF, 128, 0xFF66CC, 255, 0xFFFFFF };
        private bool[] bValid = new bool[5];
        private bool bErr;
        private string[] v = new string[5], vt = new string[5];
        private float[] nWght = new float[5], numWeight = new float[1];
        private short[] gdsi = new short[5], gdsid=new short[1];
        private string[] gdsv = new string[5], gdsvar =new string[1];
        private string[] gdsvd = new string[5], gdsvardesc = new string[1], gdstbl = new string[1];
        private string[] sWhereEx;
        private short[] xstr = new short[5], exstr = new short[1];
        private short[] rvsflg = new short[5], revVarFlg = new short[1];
        private short i, iVarCount;
        private string sSql, sErr;
        // Dim i, iVarCount, xstr(4), exstr() As Int16

        private double[] Max;
        private DataTable dt, dtResult;
        private DataView dv;
        private int iDs;
        private bool bEn;
        protected string sDs;
        private string sQryTable;
        private gdsTable oGt;
        private gdsGeo oGg;


        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.Params["v0"] != null) && (Request.Params["v1"] != null) & !(Request.Params["v2"] == null) & !(Request.Params["v3"] == null) && (Request.Params["v4"] != null))
            {
                Literal1.Visible = true;
                Literal2.Visible = true;
                WebChartViewer1.Visible = true;
                DataGrid2.Visible = true;
                GetRequestForm();
                BuildGrid();
                SetupUI();
                PanelResultToolBar1.Visible = true;
                PanelResultToolBar1.SetPrintUrl("window.open('scp.aspx" + Request.Url.Query + "','_blank','width=800,status=0,toolbar=0,menubar=0,location=0,resizable=1,scrollbars=1');");
                pTextSc.Visible = false;
            }
            else
            {
                Literal1.Visible = false;
                Literal2.Visible = false;
                WebChartViewer1.Visible = false;
                DataGrid2.Visible = false;
                PanelResultToolBar1.Visible = false;
                pTextSc.PanelId = 8;
                pTextSc.Visible = true;
            }
        }



        private void BuildChart(DataTable table, string sortBy, string sortType)
        {
            const int rowHeight = 15;
            const int baseHeight = 200;
            int h = baseHeight + table.Rows.Count * rowHeight;
            XYChart c;
            var data = new double[table.Rows.Count];
            var labels = new string[table.Rows.Count];
            short iRow, iCol;
            BarLayer Layer;
            ChartDirector.TextBox textbox;
            ChartDirector.TextBox tb2;
            string sHover;
            // ************* BEGIN Ini semua gara-gara sorting
            if ((sortType ?? "") == "ASC")
                table.DefaultView.Sort = sortBy + " DESC";
            else if ((sortType ?? "") == "DESC")
                table.DefaultView.Sort = sortBy + " ASC";

            table.DefaultView.Sort = sortBy + " " + sortType;
            ChartDirector.TextBox txtTitle;
            var loopTo = table.Rows.Count - 1;
            for (iRow = 0; iRow <= loopTo; iRow++) // urutan kebalik dari DataView
            {
                data[iRow] = Convert.ToDouble(table.DefaultView[table.Rows.Count - 1 - iRow]["Score"]);
                labels[iRow] = table.DefaultView[table.Rows.Count - 1 - iRow]["distName"].ToString();
            }
            // ************* END  Ini semua gara-gara sorting

            c = new XYChart(460, h, 0xFFFFFF, 0xCCCCCC, 1);
            // c = New XYChart(460, 600, &HFFFFFF, &HCCCCCC, 1)
            if (Request.Params["pr"] != null)
                txtTitle = c.addTitle(bEn? "Score On Each 'Kecamatan' in " + (char)10 + oGg.ProvName: "Score Masing-masing Kecamatan " + (char)10 + "di Propinsi " + oGg.ProvName, "arialbd.ttf", 10);
            else
                txtTitle = c.addTitle(bEn ? "Score On Each Province" : "Score Masing-masing Propinsi", "arialbd.ttf", 10);

            txtTitle.setBackground(c.gradientColor(INTGRADBLEUINV, 180, INTRATIO), -1, 0);

            // c.setPlotArea(120, 50, 280, 450, &HFFFFFF, &HFFFFFF, &HC0C0C0, &HC0C0C0 )
            c.setPlotArea(180, 50, 240, 80 + table.Rows.Count * 15);
            Layer = c.addBarLayer(data, c.gradientColor(INTGRADBLEU, 90, 240 / (double)256));
            // Layer = c.addBarLayer(data, c.gradientColor(Chart.goldGradient))
            Layer.setBorderColor(-1, 1);
            Layer.set3D(5);
            c.swapXY(true);
            Layer.setAggregateLabelFormat(bEn ? " {value|2,.}" : " {value|2.,}");
            Layer.setAggregateLabelStyle("arial.ttf", 8, 0x220055);
            textbox = c.xAxis().setLabels(labels);
            textbox.setFontStyle("arial.ttf");
            textbox.setFontSize(8);
            // textbox.setFontColor(&HCC)

            tb2 = c.addText(0, c.getDrawArea().getHeight() - INTBOTTOMTEXTHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0x55, Chart.Center);
            tb2.setSize(460, INTBOTTOMTEXTHEIGHT);
            tb2.setBackground(c.gradientColor(INTGRADBLEU, 0, INTBOTTOMTEXTHEIGHT / 256, 0, INTBOTTOMTEXTHEIGHT), -1, 1);
            // tb2.setBackground(&H191970, -1, 1)

            c.xAxis().setColors(Chart.Transparent, 0x55);
            c.yAxis().setColors(Chart.Transparent, 0x0);
            c.setAntiAlias(true, 1);
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            // If bIE Then
            // sHover = "TITLE='{xLabel}: {value} '"
            sHover = " onmouseover='ci(\"tip\",\"{xLabel}<BR>&nbsp;&nbsp;Score = {value|2}\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";
            WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
            // If Request.Params("pr") <> "" Then
            // WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover)
            // Else
            // WebChartViewer1.ImageMap = c.getHTMLImageMap(Server.UrlDecode(Request.Url.Query) & "&pr=" & oGg.Prov, " ", sHover)
            // End If
            // End If
            WebChartViewer1.DataBind();
        }

        private void BuildChartKabu(DataTable table, string sortBy, string sortType)
        {
            const int rowHeight = 15;
            const int baseHeight = 200;
            int h = baseHeight + table.Rows.Count * rowHeight;
            XYChart c;
            var data = new double[table.Rows.Count];
            var labels = new string[table.Rows.Count];
            short iRow, iCol;
            BarLayer Layer;
            ChartDirector.TextBox textbox;
            ChartDirector.TextBox tb2;
            string sHover;
            // ************* BEGIN sorting
            if ((sortType ?? "") == "ASC")
            {
                table.DefaultView.Sort = sortBy + " DESC";
            }
            else if ((sortType ?? "") == "DESC")
            {
                table.DefaultView.Sort = sortBy + " ASC";
            }

            table.DefaultView.Sort = sortBy + " " + sortType;
            ChartDirector.TextBox txtTitle;
            var loopTo = table.Rows.Count - 1;
            for (iRow = 0; iRow <= loopTo; iRow++) // urutan kebalik dari DataView
            {
                data[iRow] = Convert.ToDouble(table.DefaultView[table.Rows.Count - 1 - iRow]["Score"]);
                labels[iRow] = table.DefaultView[table.Rows.Count - 1 - iRow]["distName"].ToString();
            }
            // ************* END  sorting

            c = new XYChart(460, h, 0xFFFFFF, 0xCCCCCC, 1);
            // c = New XYChart(460, 600, &HFFFFFF, &HCCCCCC, 1)
            if (Request.Params["pr"] != null)
                txtTitle = c.addTitle(bEn ? "Score On Each 'Kecamatan' in " + (char)10 + oGg.ProvName : "Score Masing-masing Kecamatan " + (char)10 + "di Propinsi " + oGg.ProvName, "arialbd.ttf", 10);
            else
                txtTitle = c.addTitle(bEn ? "Score On Each District (Kabupaten)" : "Score Masing-masing Kabupaten", "arialbd.ttf", 10);

            txtTitle.setBackground(c.gradientColor(INTGRADBLEUINV, 180, INTRATIO), -1, 0);

            // c.setPlotArea(120, 50, 280, 450, &HFFFFFF, &HFFFFFF, &HC0C0C0, &HC0C0C0 )
            c.setPlotArea(180, 50, 240, 80 + table.Rows.Count * 15);
            Layer = c.addBarLayer(data, c.gradientColor(INTGRADBLEU, 90, 240 / (double)256));
            // Layer = c.addBarLayer(data, c.gradientColor(Chart.goldGradient))
            Layer.setBorderColor(-1, 1);
            Layer.set3D(5);
            c.swapXY(true);
            Layer.setAggregateLabelFormat(bEn ? " {value|2,.}" : " {value|2.,}");
            Layer.setAggregateLabelStyle("arial.ttf", 8, 0x220055);
            textbox = c.xAxis().setLabels(labels);
            textbox.setFontStyle("arial.ttf");
            textbox.setFontSize(8);
            // textbox.setFontColor(&HCC)

            tb2 = c.addText(0, c.getDrawArea().getHeight() - INTBOTTOMTEXTHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0x55, Chart.Center);
            tb2.setSize(460, INTBOTTOMTEXTHEIGHT);
            tb2.setBackground(c.gradientColor(INTGRADBLEU, 0, INTBOTTOMTEXTHEIGHT / 256, 0, INTBOTTOMTEXTHEIGHT), -1, 1);
            // tb2.setBackground(&H191970, -1, 1)

            c.xAxis().setColors(Chart.Transparent, 0x55);
            c.yAxis().setColors(Chart.Transparent, 0x0);
            c.setAntiAlias(true, 1);
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            // If bIE Then
            // sHover = "TITLE='{xLabel}: {value} '"
            sHover = " onmouseover='ci(\"tip\",\"{xLabel}<BR>&nbsp;&nbsp;Score = {value|2}\");' onmousemove='cm(\"tip\");' onmouseout='ch(\"tip\");'  ";
            WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover);
            // If Request.Params("pr") <> "" Then
            // WebChartViewer1.ImageMap = c.getHTMLImageMap("javascript:void(null);", " ", sHover)
            // Else
            // WebChartViewer1.ImageMap = c.getHTMLImageMap(Server.UrlDecode(Request.Url.Query) & "&pr=" & oGg.Prov, " ", sHover)
            // End If
            // End If
            WebChartViewer1.DataBind();
        }

        public void BuildGrid()
        {
            dt = BuildTable();
            dtResult = BuildTableResult(dt);
            //if (Trace.IsEnabled)
            //{
            //    Trace.Warn("BEFORE viewstate(\"sortBy\")", ViewState["sortBy"].ToString());
            //    Trace.Warn("BEFORE viewstate(\"sortType\")", ViewState["sortType"].ToString());
            //}

            if (ViewState["sortBy"] == null )
                ViewState["sortBy"] = "Score";

            if (ViewState["sortType"] == null )
                ViewState["sortType"] = "DESC";
            // dtResult.DefaultView.Sort = "Score DESC"

            if (Trace.IsEnabled)
            {
                Trace.Warn("AFTER viewstate(\"sortBy\")", ViewState["sortBy"].ToString());
                Trace.Warn("AFTER viewstate(\"sortType\")", ViewState["sortType"].ToString());
            }

            dtResult.DefaultView.Sort = ViewState["sortBy"] + " " + ViewState["sortType"];
            DataGrid2.DataSource = dtResult;
            // DataGrid2.AllowSorting = True 'TODO: setup sorting

            var colRank = new BoundColumn();
            colRank.DataField = "Rank";
            colRank.HeaderText = bEn? "Rank": "Peringkat";
            colRank.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            colRank.SortExpression = "Rank";
            DataGridColumn colDistName;
            if (Request.Params["pr"] != "")
            {
                colDistName = new BoundColumn();
                ((BoundColumn)colDistName).DataField = "distName";
                if (Request.Params["pr"] != "")
                    colDistName.HeaderText = bEn ? "Kecamatan" : "Kecamatan";
                else
                {
                    // colDistName.HeaderText = IIf(bEn, "Province", "Propinsi")
                    colDistName.HeaderText = bEn ? "District" : "Kabupaten";
                }
                colDistName.SortExpression = "distName";
            }
            else
            {
                colDistName = new HyperLinkColumn();
                {
                    ((HyperLinkColumn)colDistName).DataTextField = "distName";
                    // If oGt.HasKeca Then
                    // .DataNavigateUrlField = "dist"
                    // .DataNavigateUrlFormatString = Server.UrlDecode(Request.Url.Query) & "&pr={0}"
                    // .HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                    // End If
                    colDistName.SortExpression = "distName";
                }

                if (Request.Params["pr"] != "")
                    colDistName.HeaderText = bEn ? "Kecamatan" : "Kecamatan";
                else
                {
                    // colDistName.HeaderText = IIf(bEn, "Province", "Propinsi")
                    colDistName.HeaderText = bEn ? "District" : "Kabupaten";
                }
            }

            BoundColumn colScoreValue = new BoundColumn();
            colScoreValue.DataField = "Score";
            colScoreValue.DataFormatString = "{0:##.00}";
            colScoreValue.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            colScoreValue.HeaderText = bEn ? "Score" : "Nilai (Score)";
            colScoreValue.SortExpression = "Score";
            DataGrid2.Columns.Add(colRank);
            DataGrid2.Columns.Add(colDistName);
            DataGrid2.Columns.Add(colScoreValue);
            DataGrid2.ItemStyle.Font.Size = FontUnit.Point(8);
            DataGrid2.DataBind();
            if (Request.Params["ch"] == "1")
            {
                if (!IsPostBack)
                {
                    // BuildChart(dtResult, Me.ViewState("sortBy"), Me.ViewState("sortType"))
                    BuildChartKabu(dtResult, this.ViewState["sortBy"].ToString(), this.ViewState["sortType"].ToString());
                }
            }
        }

        private DataTable BuildTable()
        {
            var cn = new SqlConnection(commonModule.GetConnString());
            var cm = new SqlCommand(WriteSQLCommon(), cn);
            SqlDataReader dr;
            string sCn;
            DataRow dRow;
            DataTable tblAvg;
            tblAvg = new DataTable();
            int iRow = 0;
            cn.Open();
            if (Trace.IsEnabled)
            {
                Trace.Warn("WriteSQLCommon()", WriteSQLCommon());
            }

            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                sWhereEx[iRow] = dr.IsDBNull(2) ? " " : " AND " + dr[2];
                gdstbl[iRow] = dr[3].ToString();     // gdstbl column
                gdsvar[iRow] = dr["var"].ToString();
                if (bEn)
                    gdsvardesc[iRow] = dr["desc_en"].ToString();
                else
                    gdsvardesc[iRow] = dr["desc"].ToString();

                iRow += 1;
            }

            dr.Close();

            // If Request.Params("pr") <> "" Then
            // cm.CommandText = WriteSQLAvg(Request.Params("pr"))
            // Else
            // cm.CommandText = WriteSQLAvgProv()
            // End If

            if (Request.Params["pr"] != null)
                cm.CommandText = WriteSQLAvg(Request.Params["pr"]);
            else
                cm.CommandText = WriteSQLAvgKabu();

            //if (Trace.IsEnabled)
            //    Trace.Warn("WriteSQLAvg()", cm.CommandText);

            dr = cm.ExecuteReader();
            for (i = 0; i < dr.FieldCount; i++)
                tblAvg.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
            while (dr.Read())
            {
                dRow = tblAvg.NewRow();
                for (i = 0; i < dr.FieldCount; i++)
                    dRow[i] = dr.IsDBNull(i)? 0: dr[i];
                tblAvg.Rows.Add(dRow);
            }

            dr.Close();
            cn.Close();
            cn = null;
            cm = null;
            dr = null;
            return tblAvg;
        }

        private DataTable BuildTableResult(DataTable SourceTable)
        {
            DataTable tblTemp;
            var tblResult = new DataTable();
            DataRow dRow;
            float x;
            string sColDist = SourceTable.Columns[0].ColumnName;
            string sColDistId = SourceTable.Columns[SourceTable.Columns.Count - 1].ColumnName;
            tblTemp = new DataTable();
            tblTemp.Columns.Add(sColDist); // kolom 1 (distName): Nama daerahnya
            tblTemp.Columns.Add(sColDistId);  // last column (dist): kode propinsi
            // tblTemp.Columns.Add("dist")  'last column: kode propinsi
            tblTemp.Columns.Add("Score", typeof(float));
            for (int iCol = 0; iCol < iVarCount; iCol++)
                Max[iCol] = Convert.ToDouble(SourceTable.Compute("Max (" + SourceTable.Columns[iCol + 1].ColumnName + ")", ""));
            
            for (int iRow = 0; iRow < SourceTable.Rows.Count; iRow++)
            {
                x = 0;
                dRow = tblTemp.NewRow();
                dRow[0] = SourceTable.Rows[iRow][0];
                for (int iCol = 0; iCol < iVarCount; iCol++)
                {
                    if (revVarFlg[iCol] == 0)
                    {
                        if (Max[iCol] == 0)
                            x += 0;
                        else
                            x += numWeight[iCol] * Convert.ToSingle(SourceTable.Rows[iRow][iCol + 1]) / Convert.ToSingle(Max[iCol]);
                    }
                    else // Reversed?
                    {
                        if (Max[iCol] == 0)
                            x += 0;
                        else
                            x += numWeight[iCol] * (1 - Convert.ToSingle(SourceTable.Rows[iRow][iCol + 1]) / Convert.ToSingle(Max[iCol]));

                        if (Trace.IsEnabled)
                        {
                            Trace.Warn("numWeight(" + iCol + ")", numWeight[iCol].ToString());
                            Trace.Warn("Max(" + iCol + ")", Max[iCol].ToString());
                            Trace.Warn("SourceTable.Rows(" + iRow + ")(" + (iCol + 1) + ")", SourceTable.Rows[iRow][iCol + 1].ToString());
                            Trace.Warn("x", x.ToString());
                        }
                    }
                }

                dRow["Score"] = x;
                dRow[sColDistId] = SourceTable.Rows[iRow][sColDistId];
                if (Trace.IsEnabled)
                {
                    Trace.Warn("SourceTable.Rows(" + iRow + ")(\"dist\")", SourceTable.Rows[iRow]["dist"].ToString());
                    Trace.Warn("dRow(\"dist\")", SourceTable.Rows[iRow]["dist"].ToString());
                    Trace.Warn("dRow(\"Score\")", dRow["Score"].ToString());
                }

                tblTemp.Rows.Add(dRow);
            }

            DataView dv = tblTemp.DefaultView;
            dv.Sort = "Score DESC";
            tblResult.Columns.Add("Rank", typeof(int));
            tblResult.Columns.Add(sColDist); // kolom 1: Nama daerahnya
            tblResult.Columns.Add(sColDistId);  // last column: kode propinsi
            // tblTemp.Columns.Add("dist")  'last column: kode propinsi
            tblResult.Columns.Add("Score", typeof(float));
            for (int iRow = 0, loopTo3 = tblTemp.Rows.Count - 1; iRow <= loopTo3; iRow++)
            {
                dRow = tblResult.NewRow();
                dRow["Rank"] = iRow + 1;
                dRow[sColDist] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dv[iRow][sColDist].ToString()).ToLower();
                dRow[sColDistId] = dv[iRow][sColDistId];
                dRow["Score"] = dv[iRow]["Score"];
                tblResult.Rows.Add(dRow);
            }

            return tblResult;
        }

        private string GetRequestForm()
        {
            StringBuilder fsOut;
            fsOut = new StringBuilder();
            iVarCount = 0;
            sDs = Request.Params["ds"] != "" ? Request.Params["ds"] : "11";
            bEn = commonModule.IsEnglish();
            iDs = Convert.ToInt32( sDs);
            oGt = new gdsTable(iDs);
            if (Request.Params["pr"] != "")
                oGg = new gdsGeo(Request.Params["pr"], "All");

            for (int i = 0; i <= 4; i++)
            {
                if (Request.Params["v" + i] != "0")
                {
                    if (Request.Params["vt" + i] != null & Request.Params["vt" + i] != "" & char.IsNumber(Request.Params["vt" + i],0))
                        bValid[i] = true;
                }

                if (bValid[i])
                {
                    v[i] = Request.Params["v" + i];
                    vt[i] = Request.Params["vt" + i];     // e.g:<INPUT TYPE="text" NAME"vt3">
                    nWght[i] = Convert.ToSingle(vt[i]);
                    gdsi[i] = Convert.ToInt16(v[i].Split('~')[0]);
                    xstr[i] = Convert.ToInt16(v[i].Split('~')[1]);
                    rvsflg[i] = Convert.ToInt16(v[i].Split('~')[3]);

                    Array.Resize(ref gdsid, iVarCount + 1);
                    Array.Resize(ref gdsvar, iVarCount + 1);
                    Array.Resize(ref gdsvardesc, iVarCount + 1);
                    Array.Resize(ref sWhereEx, iVarCount + 1);
                    Array.Resize(ref gdstbl, iVarCount + 1);
                    Array.Resize(ref exstr, iVarCount + 1);
                    Array.Resize(ref numWeight, iVarCount + 1);
                    Array.Resize(ref revVarFlg, iVarCount + 1);

                    gdsid[iVarCount] = gdsi[i]; // norak ah pake english segala
                    exstr[iVarCount] = xstr[i];
                    numWeight[iVarCount] = nWght[i];
                    revVarFlg[iVarCount] = rvsflg[i];

                    iVarCount += 1;

                    //ReDim Max(iVarCount)
                    Max = new double[iVarCount];
                }
            }

            return default(string);
        }


        public void SetupUI()
        {
            Literal1.Text = WriteRequestForm("pnlCnt");
            Literal2.Text += WriteDiv();
            if (bEn)
            {
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("id-ID");
            }

            if (Request.Params["pr"] != null)
            {
                DataGrid2.Caption = "<DIV STYLE=\"background-color:#00008b;color:yellow;font:bold 10pt Arial;padding:5px 5px 5px 5px;\">" + (bEn? "Score on each 'Kecamatan' in Province " + oGg.ProvName: "Nilai Masing-masing Kecamatan di propinsi " + oGg.ProvName) + "</DIV>";
            }
            else
            {
                // DataGrid2.Caption = "<DIV STYLE=""background-color:#00008b;color:yellow;font:bold 10pt Arial;padding:5px 5px 5px 5px;"">" & IIf(bEn, "Score on Each Province", "Nilai Masing-masing Propinsi di Tingkat Nasional") & "</DIV>"
                DataGrid2.Caption = "<DIV STYLE=\"background-color:#00008b;color:yellow;font:bold 10pt Arial;padding:5px 5px 5px 5px;\">" + (bEn ? "Score on Each District (Kabupaten)" : "Nilai Masing-masing Kabupaten") + "</DIV>";
            }
            // Literal2.Text &= WriteRef()

        }


        private string WriteDiv()
        {
            StringBuilder fsOut;
            fsOut = new StringBuilder("<DIV ID=\"tip\" ");
            {
                fsOut.AppendLine(" Style = \"position:absolute; ");
                fsOut.AppendLine(" top:0px;left:0px;width:200px;height:50px; ");
                fsOut.AppendLine(" background:yellow;z-Order:4;visibility:hidden; ");
                fsOut.AppendLine(" border:1px solid black; ");
                fsOut.AppendLine(" font:bold 12px Arial; ");
                fsOut.AppendLine(" padding:3px 3px 3px 10px; ");
                fsOut.AppendLine(" overflow:visible; ");
                fsOut.AppendLine(" filter:Alpha(Opacity=70)\"> ");
                fsOut.AppendLine(" </DIV> ");
            }
            return fsOut.ToString();
        }


        private string WriteRequestForm(string cssClass)
        {
            StringBuilder fsOut;
            fsOut = new StringBuilder();
            // iVarCount = 0
            {
                fsOut.Append("<TABLE BORDER=\"0\" CLASS=\"" + cssClass + "\" STYLE=\"border:black 1px solid;\">");
                if (bEn)
                {
                    // .Append("<TR ALIGN=""CENTER"" bgcolor=""#191970""><TD>&nbsp;</TD><TD><B><FONT COLOR=""yellow"">Selected Variables</FONT></B></TD><TD><B><FONT COLOR=""yellow"">Weighting Value</FONT></B></TD><TD><B><FONT COLOR=""yellow"">Reversed Value*</FONT></B></TD></TR>")
                    fsOut.Append("<TR ALIGN=\"CENTER\" bgcolor=\"#191970\"><TD>&nbsp;</TD><TD><B><FONT COLOR=\"yellow\">Selected Variables</FONT></B></TD><TD><B><FONT COLOR=\"yellow\">Weighting Value</FONT></B></TD></TR>");
                }
                else
                {
                    // .Append("<TR ALIGN=""CENTER"" bgcolor=""#191970""><TD>&nbsp;</TD><TD><B><FONT COLOR=""yellow"">Pilihan Variabel</FONT></B></TD><TD><B><FONT COLOR=""yellow"">Nilai Bobot</FONT></B></TD><TD><B><FONT COLOR=""yellow"">Nilai Terbalik*</FONT></B></TD></TR>")
                    fsOut.Append("<TR ALIGN=\"CENTER\" bgcolor=\"#191970\"><TD>&nbsp;</TD><TD><B><FONT COLOR=\"yellow\">Pilihan Variabel</FONT></B></TD><TD><B><FONT COLOR=\"yellow\">Nilai Bobot</FONT></B></TD></TR>");
                }

                for (i = 0; i < iVarCount; i++)
                {
                    if (bValid[i])
                    {
                        fsOut.Append("<TR>");
                        fsOut.Append("<TD ALIGN=\"RIGHT\">" + (i + 1) + ".</TD>");
                        fsOut.Append("<TD>" + gdsvardesc[i] + "</TD>");
                        fsOut.Append("<TD ALIGN=\"RIGHT\">" + numWeight[i] + "</TD>");
                        fsOut.Append("</TR>");
                    }
                    // iVarCount += 1
                    else
                    {
                        fsOut.Append("<TR align=\"center\">");
                        fsOut.Append("<TD ALIGN=\"RIGHT\">" + (i + 1) + ".</TD>");
                        fsOut.Append("<TD BGCOLOR='LIGHTGREY'>N/A</TD>");
                        fsOut.Append("<TD BGCOLOR='LIGHTGREY'>N/A</TD>");
                        // .Append("<TD BGCOLOR='LIGHTGREY'>N/A</TD>")
                        fsOut.Append("</TR>");
                    }
                }
                fsOut.Append("</TABLE>");
            }
            return fsOut.ToString();
        }


        private string WriteSQLCommon()
        {
            StringBuilder fsOut;
            int iC;
            fsOut = new StringBuilder();
            for (iC = 0; iC < gdsid.Length; iC++)
            {
                if (bEn)
                    fsOut.AppendFormat(" SELECT var,[desc_en],criteria,tbl FROM {0}", oGt.VarTable);
                else
                    fsOut.AppendFormat(" SELECT var,[desc],criteria,tbl FROM {0}", oGt.VarTable);

                fsOut.AppendFormat(" WHERE ");
                fsOut.AppendFormat(" var_id = '{0}' ", gdsid[iC]);
                if (iC < gdsid.Length - 1)
                    fsOut.Append(" UNION ALL ");
            }

            return fsOut.ToString();
        }

        private string WriteSQLAvg(string whereProv)
        {
            StringBuilder fsOut;
            fsOut = new StringBuilder("SELECT vv.distName ");
            {
                //var withBlock = fsOut;
                for (i = 0; i < iVarCount; i++)
                    fsOut.AppendLine(", v" + i + "." + gdsvar[i]);

                fsOut.AppendLine(", vv.dist");
                fsOut.Append(" FROM ( SELECT DISTINCT  prov,prov+kabu+keca AS 'dist', kecanm AS 'distName' FROM " + oGt.BaseTable + " )  vv");
                for (i = 0; i < iVarCount; i++)
                {
                    fsOut.AppendLine();
                    sQryTable = gdstbl[i];
                    fsOut.AppendLine();
                    fsOut.AppendLine(" LEFT JOIN ");
                    fsOut.AppendLine(" ( ");
                    fsOut.AppendLine(" SELECT prov+kabu+keca AS dist "); // XTREME!! make sure the prov+kabu+keca exist in the tbl
                    fsOut.AppendLine(" , AVG(CAST(" + gdsvar[i] + " AS MONEY)) AS " + gdsvar[i]);    // AS avg" & i & " "
                    fsOut.AppendLine(" FROM " + sQryTable);
                    fsOut.AppendLine(" WHERE " + gdsvar[i] + " IS NOT NULL ");
                    fsOut.AppendLine(" AND prov ='" + whereProv + "'");
                    fsOut.AppendLine(sWhereEx[i]);

                    if (exstr[i] > 0)
                        fsOut.AppendLine(" AND " + gdsvar[i] + "  NOT IN ('" + string.Empty.PadRight(exstr[i] - 1, '9') + "6','" + string.Empty.PadRight(exstr[i] - 1, '9') + "7','" + string.Empty.PadRight(exstr[i] - 1, '9') + "8','" + string.Empty.PadRight(exstr[i] - 1, '9') + "9') ");
                    
                    fsOut.AppendLine(" GROUP BY  prov+kabu+keca  "); // XTREME!! make sure the prov+kabu+keca exist in the tbl
                    fsOut.AppendLine(" ) v" + i);
                    fsOut.AppendLine(" ON vv.dist = v" + i + ".dist");
                    // End If
                }

                fsOut.AppendLine(" WHERE prov ='" + whereProv + "'"); // XTREME!! make sure the prov+kabu+keca exist in the tbl
                fsOut.AppendLine(" ORDER BY vv.dist ");
            }

            if (Trace.IsEnabled)
                Trace.Warn("WriteSqlAvg", fsOut.ToString());

            return fsOut.ToString();
        }

        private string WriteSQLAvgKabu()
        {
            StringBuilder fsOut;
            fsOut = new StringBuilder("SELECT vv.distName ");
            {
                for (i = 0; i < iVarCount; i++)
                    fsOut.AppendLine(", v" + i + "." + gdsvar[i]);

                fsOut.AppendLine(" , vv.dist");
                fsOut.AppendLine(" FROM ( SELECT DISTINCT  " + oGt.KabuFld + " AS 'dist', kabunm AS 'distName' FROM " + oGt.BaseTable + " )  vv");
                for (i = 0; i < iVarCount; i++)
                {
                    fsOut.AppendLine();
                    sQryTable = gdstbl[i];
                    fsOut.AppendLine(" LEFT JOIN ");
                    fsOut.AppendLine(" ( ");
                    fsOut.AppendLine(" SELECT " + oGt.KabuFld + " AS dist ");
                    fsOut.AppendLine(" , AVG(CAST(" + gdsvar[i] + " AS MONEY)) AS " + gdsvar[i]);    // AS avg" & i & " "
                    fsOut.AppendLine(" FROM " + sQryTable);
                    fsOut.AppendLine(" WHERE " + gdsvar[i] + " IS NOT NULL ");
                    fsOut.AppendLine(sWhereEx[i]);

                    if (exstr[i] > 0)
                        fsOut.AppendLine(" AND " + gdsvar[i] + "  NOT IN ('" + string.Empty.PadRight(exstr[i] - 1, '9') + "6','" + string.Empty.PadRight(exstr[i] - 1, '9') + "7','" + string.Empty.PadRight(exstr[i] - 1, '9') + "8','" + string.Empty.PadRight(exstr[i] - 1, '9') + "9') ");

                    fsOut.AppendLine(" GROUP BY  " + oGt.KabuFld + "  ");
                    fsOut.AppendLine(" ) v" + i);
                    // If i > 0 Then
                    fsOut.AppendLine(" ON vv.dist = v" + i + ".dist");
                    // End If
                }

                fsOut.AppendLine(" ORDER BY vv.dist ");
            }

            return fsOut.ToString();
        }


    }
}