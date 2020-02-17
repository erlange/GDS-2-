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
    public partial class treeTwOut : System.Web.UI.UserControl
    {
        protected bool isSingleVar;
        protected bool bEn;
        protected bool isValidRequest;

        protected treeVars TreeVars1;
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
        private DataTable dt;
        private string sCn, sValues, s0, s1, sOut, sSql, sCh, sErr;
        private string[] aValues, aS0, aS1;
        private string[] sWhere = new string[2];
        private string[] gdstbl = new string[2];
        private int[] gdsid = new int[2];
        private string sQryDist;
        private string[] gdsvar = new string[2];
        private string sDist, sRegn;
        private string[] gdsvardesc = new string[2], exstr = new string[2];
        private double sCorr, sStdErr, sSlope, sY;
        private bool bErr;
        private bool isPct1, isPct2;
        private double Avg1, Avg2;
        private double Max1, Max2;
        private bool bSwap = false;
        private gdsTable oGt;
        private gdsGeo oGg;
        protected bool ch;
        protected int iDs;
        protected int lang;
        protected bool _isContVarOnly;
        protected int c;
        protected string submitString;
        protected string actionString;
        protected string nextString;
        protected string prevString;
        protected string chString;

        protected void Page_Load(object sender, EventArgs e)
        {
            pTextTw.PanelId = 7;
            Main();

        }

        private void BuildChart(DataTable tbl)
        {
            ChartDirector.TextBox tb, tb2;
            LegendBox lg;
            string s0, s1;
            var x = new double[tbl.Rows.Count];
            var y = new double[tbl.Rows.Count];
            var sLbl = new string[tbl.Rows.Count];
            int intWidth = 480;
            int intHeight = 520;
            var gradColor = new[] { 0, 0xAAAAFF, 128, 0xFFFFFF, 256, 0xAAAAFF };
            for (int i = 0, loopTo = tbl.Rows.Count - 1; i <= loopTo; i++)
            {
                x[i] = Convert.ToDouble(tbl.Rows[i][tbl.Columns[1]]);
                y[i] = Convert.ToDouble(tbl.Rows[i][tbl.Columns[2]]);
                sLbl[i] = tbl.Rows[i][tbl.Columns[0]].ToString();
            }

            var c = new XYChart(intWidth, intHeight);
            c.setPlotArea(90, 155, intWidth - 130, 300, 0xFFFFFF, -1, 0xC0C0C0, 0xC0C0C0, -1).setBackground(c.gradientColor(gradColor, 135), -1, 1);
            // .setBackground(c.gradientColor(ChartDirector.Chart.silverGradient, 180))
            c.setBackground(0xFFFFFF, 0xCCCCCC, 1);
            string sMean1 = commonModule.WrapString(gdsvardesc[0] + " (Mean1)", intWidth, 10);
            string sMean2 = commonModule.WrapString(gdsvardesc[1] + " (Mean2)", intWidth, 10);
            tb = c.addTitle(sMean1 + (char)10 + "&" + (char)10 + sMean2, "arialbd.ttf", 10);
            tb.setBackground(c.gradientColor(INTGRADBLEUINV, 180, INTRATIO), -1, 0);
            // tb.setBackground(&HE6E6FA, -1, 1)
            // c.setSize(intWidth, intHeight + tb.getHeight)

            lg = c.addLegend(90, 110, false, "arialbd.ttf", 9);
            lg.setBackground(Chart.Transparent);
            {
                //var withBlock = c.yAxis();
                c.yAxis().setTitle("Mean2", "arialbi.ttf", 10);
                c.yAxis().setWidth(3);
                c.yAxis().setLabelFormat(bEn ? "{value|1,.}" : "{value|1.,}");
            }

            {
                //var withBlock1 = c.xAxis();
                c.xAxis().setTitle("Mean1", "arialbi.ttf", 10);
                c.xAxis().setWidth(3);
                c.xAxis().setLabelFormat(bEn ? "{value|1,.}" : "{value|1.,}");
            }

            Layer scatter1 = c.addScatterLayer(x, y, "District", Chart.DiamondSymbol, 6, 0xFF, 0xFF0000);
            scatter1.addExtraField(sLbl);     // add district
            TrendLayer trend1 = c.addTrendLayer2(x, y, 0xFF0000, "Trend");
            trend1.setLineWidth(1);
            trend1.addConfidenceBand(0.95, 0x706666FF);
            // trend1.addPredictionBand(0.95, &H8066FF66)
            lg.addKey("95% Line Confidence", 0x706666FF);
            // lg.addKey("95% Point Confidence", &H8066FF66)

            tb2 = c.addText(0, c.getDrawArea().getHeight() - INTBOTTOMTEXTHEIGHT, commonModule.GetChartFooterString(), "arial.ttf", 8, 0x55, Chart.Center);
            // tb2 = c.addText(0, 450, "Indonesia - Governance and Decentralization Survey II", "arial.ttf", 8, &HFFFF00, Chart.Center)
            tb2.setSize(intWidth, INTBOTTOMTEXTHEIGHT);
            tb2.setBackground(c.gradientColor(INTGRADBLEU, 0, INTBOTTOMTEXTHEIGHT / 256, 0, INTBOTTOMTEXTHEIGHT), -1, 1);
            c.setAntiAlias(true, 1);
            WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
            sCorr = trend1.getCorrelation();
            sSlope = trend1.getSlope();
            sStdErr = trend1.getStdError();
            sY = trend1.getIntercept();
            c.setAntiAlias(true, 1);
            // If bIE Then
            // commented out as requested
            // s0 = "TITLE='District = {field0} " _
            // & Chr(10) & vbTab & Replace(gdsvardesc(0), "'", """") & " : {x} " _
            // & Chr(10) & vbTab & Replace(gdsvardesc(1), "'", """") & " : {value} '" _
            // & " onmouseover=""ci('District = {field0}<BR>&nbsp;&nbsp;" & Replace(Replace(gdsvardesc(0), "'", "\'"), """", "\""") & " : {x}<BR>&nbsp;&nbsp;" & Replace(Replace(gdsvardesc(1), "'", "\'"), """", "\""") & " : {value} ');"" onmousemove=""cm();"" onmouseout=""ch();""  "

            string sTipLabel;
            if (Request.Params["pr"] != "")
            {
                sTipLabel = bEn? "Kecamatan": "Kecamatan";
            }
            else
            {
                sTipLabel = bEn ? "Province" : "Propinsi";
            }

            if (bEn)
            {
                s0 = " onmouseover=\"ci('tip','" + sTipLabel + " = {field0}<BR>&nbsp;&nbsp;" + gdsvardesc[0].Replace("'", @"\'").Replace("\"", @"\""") + " : {x|2,.}<BR>&nbsp;&nbsp;" + gdsvardesc[1].Replace("'", @"\'").Replace("\"", @"\""") + " : {value|2,.} ');\" onmousemove=\"cm('tip');\" onmouseout=\"ch('tip');\"  ";
            }
            else
            {
                s0 = " onmouseover=\"ci('tip','" + sTipLabel + " = {field0}<BR>&nbsp;&nbsp;" + gdsvardesc[0].Replace("'", @"\'").Replace("\"", @"\""") + " : {x|2.,}<BR>&nbsp;&nbsp;" + gdsvardesc[1].Replace("'", @"\'").Replace("\"", @"\""") + " : {value|2.,} ');\" onmousemove=\"cm('tip');\" onmouseout=\"ch('tip');\"  ";
            }

            // commented out as requested
            // s1 = "TITLE='Trend : " _
            // & Chr(10) & vbTab & "Slope = {slope|4} " _
            // & Chr(10) & vbTab & "y-Intercept = {intercept|4} " _
            // & Chr(10) & vbTab & "Correlation coeff. = {corr|4} " _
            // & Chr(10) & vbTab & "Std. Err. = {stderr|4}'" _
            // & " onmouseover=""ci('Trend : <BR>&nbsp;&nbsp;Slope = {slope|4}<BR>&nbsp;&nbsp;y-Intercept = {intercept|4}<BR>&nbsp;&nbsp;Correlation coeff. = {corr|4}<BR>&nbsp;&nbsp;Std. Err. = {stderr|4}');"" onmousemove=""cm();"" onmouseout=""ch();""  "
            // s1 = " onmouseover=""ci('Trend : <BR>&nbsp;&nbsp;Slope = {slope|4}<BR>&nbsp;&nbsp;y-Intercept = {intercept|4}<BR>&nbsp;&nbsp;Correlation coeff. = {corr|4}<BR>&nbsp;&nbsp;Std. Err. = {stderr|4}');"" onmousemove=""cm();"" onmouseout=""ch();""  "

            if (bEn)
            {
                s1 = " onmouseover=\"ci('tip','Trend : <BR>&nbsp;&nbsp;Slope = {slope|2,.}<BR>&nbsp;&nbsp;y-Intercept = {intercept|2,.}<BR>&nbsp;&nbsp;R<SUP>2</SUP> = " + Math.Pow(sCorr, 2).ToString("0.00") + "');\" onmousemove=\"cm('tip');\" onmouseout=\"ch('tip');\"  ";
            }
            else
            {
                s1 = " onmouseover=\"ci('tip','Garis Tren : <BR>&nbsp;&nbsp;Kemiringan(Slope) = {slope|2.,}<BR>&nbsp;&nbsp;y-Intercept = {intercept|2.,}<BR>&nbsp;&nbsp;R<SUP>2</SUP> = " + Math.Pow(sCorr, 2).ToString( "0.00") + "');\" onmousemove=\"cm('tip');\" onmouseout=\"ch('tip');\"  ";
            }

            WebChartViewer1.ImageMap = scatter1.getHTMLImageMap("javascript:void(null)", " ", s0) + trend1.getHTMLImageMap("javascript:void(null)", " ", s1);

            // End If

            var sbT = new StringBuilder();
            sbT.AppendFormat("<TABLE BORDER=\"0\"  WIDTH=\"100%\" STYLE=\"border:solid 1pt black;\">");
            sbT.AppendFormat("<TR BGCOLOR=\"#EEEEEE\"><TD CLASS=\"H8\"><I>Slope</TD></I><TD ALIGN=\"right\" CLASS=\"H8\">{0}</TD></TR>", sSlope.ToString("0.00"));
            sbT.AppendFormat("<TR BGCOLOR=\"#e6e6fa\"><TD CLASS=\"H8\"><I>Y-Intercept</TD></I><TD ALIGN=\"right\" CLASS=\"H8\">{0}</TD></TR>", sY.ToString("0.00"));
            sbT.AppendFormat("<TR BGCOLOR=\"#EEEEEE\"><TD CLASS=\"H8\"><I>R<SUP>2</SUP></I></TD><TD ALIGN=\"right\" CLASS=\"H8\">{0}</TD></TR>", Math.Pow(sCorr, 2).ToString("0.0000"));
            sbT.AppendFormat("</TABLE>");
            this.literalFooter.Text = sbT.ToString();

            // DataGrid1.Caption &= sbT.ToString

            // With Me.Literal4
            // '.Text &= "<DIV STYLE=""font-size:11px;"">Right-click mouse button to save chart as picture." & IIf(bIE, "<BR>Move mouse over the chart for details.</DIV>", "") _
            // .Text &= "<DIV STYLE=""font-size:11px;"">Right-click mouse button to save chart as picture." & "<BR>Move mouse over the chart for details.</DIV>" _
            // & "<BR><TABLE BORDER=""0""  STYLE=""border:solid 1pt black;"">" _
            // & "<TR BGCOLOR=""#EEEEEE""><TD><I>Slope</TD></I><TD ALIGN=""right"">" & FormatNumber(sSlope, 2) & "</TD></TR>" _
            // & "<TR BGCOLOR=""#e6e6fa""><TD><I>Y-Intercept</TD></I><TD ALIGN=""right"">" & FormatNumber(sY, 2) & "</TD></TR>" _
            // & "<TR BGCOLOR=""#EEEEEE""><TD><I>R<SUP>2</SUP></I></TD><TD ALIGN=""right"">" & FormatNumber(sCorr ^ 2, 4) & " </TD></TR>" _
            // & "</TABLE>"
            // ' & "<TR BGCOLOR=""#EEEEEE""><TD><I>Correlation Coeff.</I></TD><TD ALIGN=""right"">" & FormatNumber(sCorr, 4) & " </TD></TR>" _
            // '& "<TR BGCOLOR=""#e6e6fa""><TD><I>Std. Error</I></TD><TD ALIGN=""right"">" & FormatNumber(sStdErr, 4) & "</TD></TR>" _
            // End With
        }

        private string BuildSqlCommon(string v1, string v2, int ds)
        {
            var fSql = new StringBuilder();
            // Select Case ds
            // Case 2, 3
            if (bEn)
            {
                fSql.AppendFormat(" SELECT [desc_en] ,criteria,tbl,var FROM {0} WHERE var_id = '{1}' ; ", oGt.VarTable, v1);
                fSql.AppendFormat(" SELECT [desc_en]  ,criteria,tbl, var FROM {0} WHERE var_id = '{1}'  ", oGt.VarTable, v2);
            }
            else
            {
                fSql.AppendFormat(" SELECT [desc] ,criteria,tbl,var FROM {0} WHERE var_id = '{1}' ; ", oGt.VarTable, v1);
                fSql.AppendFormat(" SELECT [desc]  ,criteria,tbl, var FROM {0} WHERE var_id = '{1}'  ", oGt.VarTable, v2);
            }
            // Case Else
            // fSql.AppendFormat(" SELECT gdsvardesc ,w1,null, gdsvar FROM {0} WHERE gdsid = '{1}' ; ", sQryVarDsc, v1)
            // fSql.AppendFormat(" SELECT gdsvardesc ,w1,null, gdsvar FROM {0} WHERE gdsid = '{1}'  ", sQryVarDsc, v2)
            // End Select
            if (Trace.IsEnabled)
            {
                Trace.Warn("BuildSqlCommon()", fSql.ToString());
            }

            return fSql.ToString();
        }

        private string BuildSqlKabu(string v1, string v2, string s1, string s2, string tb1, string tb2)
        {
            StringBuilder fSql;
            string sDup1, sDup2, sRes1, sRes2;
            short sNum1, sNum2;
            sNum1 = Convert.ToInt16(s1);
            if (sNum1 > 0)
            {
                //sDup1 = Strings.StrDup(sNum1 - 1, "9");
                sDup1 = sNum1.ToString().PadRight(sNum1 - 1, '9');
                sRes1 = " AND " + v1 + " NOT IN (" + sDup1 + "6, " + sDup1 + "7, " + sDup1 + "8, " + sDup1 + "9) ";
            }
            else
            {
                sRes1 = " ";
            }

            sNum2 = Convert.ToInt16(s2);
            if (sNum2 > 0)
            {
                //sDup2 = Strings.StrDup(sNum2 - 1, "9");
                sDup2 = sNum2.ToString().PadRight(sNum2 - 1, '9');
                sRes2 = " AND " + v2 + " NOT IN (" + sDup2 + "6, " + sDup2 + "7, " + sDup2 + "8, " + sDup2 + "9) ";
            }
            else
            {
                sRes2 = " ";
            }

            fSql = new StringBuilder();
            {
                //var withBlock = fSql;
                // .Append(" SELECT t1.District, t1.Mean1, t2.Mean2 FROM ")
                // .Append("	(SELECT  " & sQryDistrict & " AS District")
                // .Append("	, AVG(CAST(" & v1 & " AS MONEY)) AS Mean1 ")
                // .Append("	FROM " & sQryTable)
                // .Append("	WHERE(" & v1 & " Is Not null) ")
                // .Append(sWhere(0))
                // .Append(sRes1)
                // .Append("	 GROUP BY  " & sQryDistrict & "  ) t1 ")
                // .Append(" INNER JOIN ")
                // .Append("	(SELECT  " & sQryDistrict & "  AS District ")
                // .Append("	, AVG(CAST(" & v2 & " AS MONEY)) as Mean2 ")
                // .Append("	FROM " & sQryTable)
                // .Append("	WHERE(" & v2 & " Is Not null) ")
                // .Append(sWhere(1))
                // .Append(sRes2)
                // .Append("	GROUP BY  " & sQryDistrict & "  ) t2 ")
                // .Append(" ON t1.District = t2.District ")
                // .Append(" ORDER BY Mean2 DESC ")


                // Beware! COALESCE is SQL2K specific (Access doesn't understand)
                // .Append(" SELECT t0.District, COALESCE(t1.Mean1,0)AS Mean1, COALESCE(t2.Mean2,0) AS Mean2 FROM ")

                // If iDs = 11 Or iDs = 12 Then
                // tb1 = sQryTable
                // tb2 = sQryTable
                // End If
                fSql.AppendLine(" SELECT t0.KabuNm,  t1.Mean1 , t2.Mean2 , t0.kabu FROM ");
                fSql.AppendFormat(" (	SELECT DISTINCT prov+kabu As 'kabu',kabunm AS 'KabuNm' FROM {0}) t0 ", oGt.BaseTable);
                fSql.AppendLine(" LEFT JOIN ");
                fSql.AppendLine("	(SELECT  prov+kabu AS kabu");
                fSql.AppendLine("	, AVG(CAST(" + v1 + " AS MONEY)) AS Mean1 ");
                fSql.AppendLine("	FROM " + tb1);
                fSql.AppendLine("	WHERE(" + v1 + " Is Not null) ");
                // .AppendFormat("	AND prov='{0}' ", whereProv)

                fSql.AppendLine(sWhere[0]);
                fSql.AppendLine(sRes1);
                fSql.AppendLine("	 GROUP BY  prov+kabu) t1 ");
                fSql.AppendLine(" ON t0.kabu = t1.kabu ");
                fSql.AppendLine(" LEFT JOIN  ");
                fSql.AppendLine("	(SELECT  prov+kabu AS kabu");
                fSql.AppendLine("	, AVG(CAST(" + v2 + " AS MONEY)) as Mean2 ");
                fSql.AppendLine("	FROM " + tb2);
                fSql.AppendLine("	WHERE(" + v2 + " Is Not null) ");
                // .AppendFormat("	AND prov='{0}' ", whereProv)
                fSql.AppendLine(sWhere[1]);
                fSql.AppendLine(sRes2);
                fSql.AppendLine("	GROUP BY  prov+kabu ) t2 ");
                fSql.AppendLine(" ON t0.kabu = t2.kabu");
                fSql.AppendLine(" WHERE Mean1 IS NOT NULL AND Mean2 IS NOT NULL ");
                fSql.AppendLine(" ORDER BY Mean1 DESC ");
            }

            // Me.Literal5.Text = fSql.ToString		 'Debugging purpose
            if (Trace.IsEnabled)
            {
                Trace.Warn("BuildSqlKabu()", fSql.ToString());
            }

            return fSql.ToString();
        }

        private string BuildSqlKeca(string v1, string v2, string s1, string s2, string tb1, string tb2, string whereProv)
        {
            StringBuilder fSql;
            string sDup1, sDup2, sRes1, sRes2;
            short sNum1, sNum2;
            sNum1 = Convert.ToInt16(s1);
            if (sNum1 > 0)
            {
                //sDup1 = Strings.StrDup(sNum1 - 1, "9");
                sDup1 = sNum1.ToString().PadRight(sNum1 - 1, '9');
                sRes1 = " AND " + v1 + " NOT IN (" + sDup1 + "6, " + sDup1 + "7, " + sDup1 + "8, " + sDup1 + "9) ";
            }
            else
            {
                sRes1 = " ";
            }

            sNum2 = Convert.ToInt16(s2);
            if (sNum2 > 0)
            {
                //sDup2 = Strings.StrDup(sNum2 - 1, "9");
                sDup2 = sNum2.ToString().PadRight(sNum2 - 1, '9');
                sRes2 = " AND " + v2 + " NOT IN (" + sDup2 + "6, " + sDup2 + "7, " + sDup2 + "8, " + sDup2 + "9) ";
            }
            else
            {
                sRes2 = " ";
            }

            fSql = new StringBuilder();
            {
                //var withBlock = fSql;
                // .Append(" SELECT t1.District, t1.Mean1, t2.Mean2 FROM ")
                // .Append("	(SELECT  " & sQryDistrict & " AS District")
                // .Append("	, AVG(CAST(" & v1 & " AS MONEY)) AS Mean1 ")
                // .Append("	FROM " & sQryTable)
                // .Append("	WHERE(" & v1 & " Is Not null) ")
                // .Append(sWhere(0))
                // .Append(sRes1)
                // .Append("	 GROUP BY  " & sQryDistrict & "  ) t1 ")
                // .Append(" INNER JOIN ")
                // .Append("	(SELECT  " & sQryDistrict & "  AS District ")
                // .Append("	, AVG(CAST(" & v2 & " AS MONEY)) as Mean2 ")
                // .Append("	FROM " & sQryTable)
                // .Append("	WHERE(" & v2 & " Is Not null) ")
                // .Append(sWhere(1))
                // .Append(sRes2)
                // .Append("	GROUP BY  " & sQryDistrict & "  ) t2 ")
                // .Append(" ON t1.District = t2.District ")
                // .Append(" ORDER BY Mean2 DESC ")


                // Beware! COALESCE is SQL2K specific (Access doesn't understand)
                // .Append(" SELECT t0.District, COALESCE(t1.Mean1,0)AS Mean1, COALESCE(t2.Mean2,0) AS Mean2 FROM ")

                // If iDs = 11 Or iDs = 12 Then
                // tb1 = sQryTable
                // tb2 = sQryTable
                // End If
                fSql.AppendLine(" SELECT t0.KecaNm,  t1.Mean1 , t2.Mean2 , t0.keca FROM ");
                fSql.AppendFormat(" (	SELECT DISTINCT prov+kabu+keca As 'keca',kecanm AS 'KecaNm' FROM {0}) t0 ", oGt.BaseTable);
                fSql.AppendLine(" LEFT JOIN ");
                fSql.AppendLine ("	(SELECT  prov+kabu+keca AS keca");
                fSql.AppendLine("	, AVG(CAST(" + v1 + " AS MONEY)) AS Mean1 ");
                fSql.AppendLine("	FROM " + tb1);
                fSql.AppendLine("	WHERE(" + v1 + " Is Not null) ");
                fSql.AppendFormat("	AND prov='{0}' ", whereProv);
                fSql.AppendLine(sWhere[0]);
                fSql.AppendLine(sRes1);
                fSql.AppendLine("	 GROUP BY  prov+kabu+keca ) t1 ");
                fSql.AppendLine(" ON t0.keca = t1.keca ");
                fSql.AppendLine(" LEFT JOIN  ");
                fSql.AppendLine("	(SELECT  prov+kabu+keca AS keca");
                fSql.AppendLine("	, AVG(CAST(" + v2 + " AS MONEY)) as Mean2 ");
                fSql.AppendLine("	FROM " + tb2);
                fSql.AppendLine("	WHERE(" + v2 + " Is Not null) ");
                fSql.AppendFormat("	AND prov='{0}' ", whereProv);
                fSql.AppendLine(sWhere[1]);
                fSql.AppendLine(sRes2);
                fSql.AppendLine("	GROUP BY  prov+kabu+keca ) t2 ");
                fSql.AppendLine(" ON t0.keca = t2.keca ");
                fSql.AppendLine(" WHERE Mean1 IS NOT NULL AND Mean2 IS NOT NULL ");
                fSql.AppendLine(" ORDER BY Mean1 DESC ");
            }

            // Me.Literal5.Text = fSql.ToString		 'Debugging purpose
            if (Trace.IsEnabled)
            {
                Trace.Warn("BuildSqlKeca()", fSql.ToString());
            }

            return fSql.ToString();
        }

        private string BuildSqlProv(string v1, string v2, string s1, string s2, string tb1, string tb2)
        {
            StringBuilder fSql;
            string sDup1, sDup2, sRes1, sRes2;
            short sNum1, sNum2;
            sNum1 = Convert.ToInt16(s1);
            if (sNum1 > 0)
            {
                //sDup1 = Strings.StrDup(sNum1 - 1, "9");
                sDup1 = sNum1.ToString().PadRight(sNum1 - 1, '9');
                sRes1 = " AND " + v1 + " NOT IN (" + sDup1 + "6, " + sDup1 + "7, " + sDup1 + "8, " + sDup1 + "9) ";
            }
            else
            {
                sRes1 = " ";
            }

            sNum2 = Convert.ToInt16(s2);
            if (sNum2 > 0)
            {
                //sDup2 = Strings.StrDup(sNum2 - 1, "9");
                sDup2 = sNum2.ToString().PadRight(sNum2 - 1, '9');
                sRes2 = " AND " + v2 + " NOT IN (" + sDup2 + "6, " + sDup2 + "7, " + sDup2 + "8, " + sDup2 + "9) ";
            }
            else
            {
                sRes2 = " ";
            }

            fSql = new StringBuilder();
            {
                //var withBlock = fSql;
                // .Append(" SELECT t1.District, t1.Mean1, t2.Mean2 FROM ")
                // .Append("	(SELECT  " & sQryDistrict & " AS District")
                // .Append("	, AVG(CAST(" & v1 & " AS MONEY)) AS Mean1 ")
                // .Append("	FROM " & sQryTable)
                // .Append("	WHERE(" & v1 & " Is Not null) ")
                // .Append(sWhere(0))
                // .Append(sRes1)
                // .Append("	 GROUP BY  " & sQryDistrict & "  ) t1 ")
                // .Append(" INNER JOIN ")
                // .Append("	(SELECT  " & sQryDistrict & "  AS District ")
                // .Append("	, AVG(CAST(" & v2 & " AS MONEY)) as Mean2 ")
                // .Append("	FROM " & sQryTable)
                // .Append("	WHERE(" & v2 & " Is Not null) ")
                // .Append(sWhere(1))
                // .Append(sRes2)
                // .Append("	GROUP BY  " & sQryDistrict & "  ) t2 ")
                // .Append(" ON t1.District = t2.District ")
                // .Append(" ORDER BY Mean2 DESC ")


                // Beware! COALESCE is SQL2K specific (Access doesn't understand)
                // .Append(" SELECT t0.District, COALESCE(t1.Mean1,0)AS Mean1, COALESCE(t2.Mean2,0) AS Mean2 FROM ")

                // If iDs = 11 Or iDs = 12 Then
                // tb1 = sQryTable
                // tb2 = sQryTable
                // End If
                fSql.Append(" SELECT t0.ProvNm,  t1.Mean1 , t2.Mean2 , t0.prov FROM ");
                fSql.Append(" (	SELECT prov,provnm AS 'ProvNm' FROM t01Prov  ) t0 ");
                fSql.Append(" LEFT JOIN ");
                fSql.Append("	(SELECT  prov ");
                fSql.Append("	, AVG(CAST(" + v1 + " AS MONEY)) AS Mean1 ");
                fSql.Append("	FROM " + tb1);
                fSql.Append("	WHERE(" + v1 + " Is Not null) ");
                fSql.Append(sWhere[0]);
                fSql.Append(sRes1);
                fSql.Append("	 GROUP BY  prov  ) t1 ");
                fSql.Append(" ON t0.prov = t1.prov ");
                fSql.Append(" LEFT JOIN  ");
                fSql.Append("	(SELECT  prov ");
                fSql.Append("	, AVG(CAST(" + v2 + " AS MONEY)) as Mean2 ");
                fSql.Append("	FROM " + tb2);
                fSql.Append("	WHERE(" + v2 + " Is Not null) ");
                fSql.Append(sWhere[1]);
                fSql.Append(sRes2);
                fSql.Append("	GROUP BY  prov ) t2 ");
                fSql.Append(" ON t0.prov = t2.prov ");
                fSql.Append(" WHERE Mean1 IS NOT NULL AND Mean2 IS NOT NULL ");
                fSql.Append(" ORDER BY Mean1 DESC ");
            }

            // Me.Literal5.Text = fSql.ToString		 'Debugging purpose
            if (Trace.IsEnabled)
            {
                Trace.Warn("BuildSqlProv()", fSql.ToString());
            }

            return fSql.ToString();
        }

        private DataTable CreateTable()
        {
            DataTable dTbl;
            DataRow dRow;
            short i;
            var cn = new SqlConnection(commonModule.GetConnString());

            // oCm = New SqlCommand(BuildSqlCommon(gdsvar(0), gdsvar(1)), oCn)
            var cm = new SqlCommand(BuildSqlCommon(gdsid[0].ToString(), gdsid[1].ToString(), iDs), cn);
            cn.Open();
            SqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            gdsvardesc[0] = dr.IsDBNull(0) ? "" : dr[0].ToString();
            sWhere[0] = dr.IsDBNull(1) ? "" : " AND " + dr[1].ToString();
            // gdsvardesc(0) = RemoveLeadingChars(gdsvardesc(0))
            // If iDs = 2 Or iDs = 3 Then
            gdstbl[0] = dr[2].ToString();
            // End If
            gdsvar[0] = dr[3].ToString();
            dr.NextResult();
            dr.Read();
            gdsvardesc[1] = dr.IsDBNull(0) ? "" : dr[0].ToString();
            sWhere[1] = dr.IsDBNull(1) ? "" : " AND " + dr[1].ToString();
            // gdsvardesc(1) = RemoveLeadingChars(gdsvardesc(1))
            // If iDs = 2 Or iDs = 3 Then
            gdstbl[1] = dr[2].ToString();
            // End If

            gdsvar[1] = dr[3].ToString();
            dr.Close();
            if (string.IsNullOrEmpty(Request.Params["pr"]))
                cm.CommandText = BuildSqlKabu(gdsvar[0], gdsvar[1], exstr[0], exstr[1], gdstbl[0], gdstbl[1]);
            else
            {
                if (Request.Params["pr"] != "")
                {
                    cm.CommandText = BuildSqlKeca(gdsvar[0], gdsvar[1], exstr[0], exstr[1], gdstbl[0], gdstbl[1], Request.Params["pr"]);
                }
                else
                {
                    // Change from prov lvl into kabu lvl by request
                    // cm.CommandText = BuildSqlProv(gdsvar(0), gdsvar(1), exstr(0), exstr(1), gdstbl(0), gdstbl(1))
                    cm.CommandText = BuildSqlKabu(gdsvar[0], gdsvar[1], exstr[0], exstr[1], gdstbl[0], gdstbl[1]);
                }
            }



            dr = cm.ExecuteReader();

            // oDr.NextResult()
            dTbl = new DataTable();
            var loopTo = dr.FieldCount - 1;
            for (i = 0; i <= loopTo; i++)
                dTbl.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
            while (dr.Read())
            {
                dRow = dTbl.NewRow();
                dRow[0] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr[0].ToString().ToLower());
                var loopTo1 = dr.FieldCount - 1;
                for (i = 1; i <= loopTo1; i++)
                    dRow[i] = dr.IsDBNull(i) ? "0" : dr[i].ToString();
                dTbl.Rows.Add(dRow);
            }

            Avg1 = Convert.ToDouble(Convert.IsDBNull(dTbl.Compute("Avg(Mean1)", "")) ? 0 : dTbl.Compute("Avg(Mean1)", ""));
            Avg2 = Convert.ToDouble(Convert.IsDBNull(dTbl.Compute("Avg(Mean2)", "")) ? 0 : dTbl.Compute("Avg(Mean2)", ""));
            Max1 = Convert.ToDouble(Convert.IsDBNull(dTbl.Compute("Max(Mean1)", "")) ? 0 : dTbl.Compute("Max(Mean1)", ""));
            Max2 = Convert.ToDouble(Convert.IsDBNull(dTbl.Compute("Max(Mean2)", "")) ? 0 : dTbl.Compute("Max(Mean2)", ""));
            if (Avg1 <= 1)
            {
                var loopTo2 = dTbl.Rows.Count - 1;
                for (i = 0; i <= loopTo2; i++)
                    dTbl.Rows[i][1] = Convert.ToDouble(dTbl.Rows[i][1]) * 100;
                Max1 *= 100;
                isPct1 = true;
            }

            if (Avg2 <= 1)
            {
                var loopTo3 = dTbl.Rows.Count - 1;
                for (i = 0; i <= loopTo3; i++)
                    dTbl.Rows[i][2] = Convert.ToDouble(dTbl.Rows[i][2]) * 100;
                Max2 *= 100;
                isPct2 = true;
            }

            dr.Close();
            cn.Close();
            return dTbl;
        }

        public void GetRequest()
        {
            bEn = commonModule.IsEnglish();
            if (bEn)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("id-ID");
            }

            if (bEn)
            {
                nextString = "OK";
                prevString = "<< Previous";
                chString = "Show chart";
            }
            else
            {
                nextString = "OK";
                prevString = "<< Kembali";
                chString = "Tampilkan dengan chart";
            }

            if (Request.Params["ch"] == "1")
            {
                ch = true;
            }
            else
            {
                ch = false;
            }

            sCh = ch.ToString();
            if (Request.Params["ds"] != null)
            {
                iDs = int.Parse(Request.Params["ds"]);
            }
            else
            {
                iDs = 11;
            }

            // If Not Request.Params("c") Is Nothing Then
            // If Int32.Parse(Request.Params("c")) = 1 Then
            // _isContVarOnly = True
            // c = 1
            // submitString = String.Format("return dist3(0,{0});", lang)
            // actionString = "pvOut.aspx"
            // Else
            // _isContVarOnly = False
            // c = 0
            // submitString = String.Format("return mz(0,{0},{1})", c, lang)
            // actionString = "twOut.aspx"
            // End If
            // Else
            // _isContVarOnly = False
            // End If
            _isContVarOnly = true;
            c = 1;
            submitString = string.Format("return mz(0,{0},{1})", c, (bEn ? 1 : 0));
            if (Request.QueryString["sw"] != "" | (Request.QueryString["sw"] != null))
            {
                bSwap = true;
            }

            if (Request.Params["v"] != null)
            {
                if (Request.Params["v"] != "")
                {
                    sValues = Request.QueryString["v"];
                    sValues = sValues.Replace("~z,", "|");
                    aValues = sValues.Split('|');
                    s0 = aValues[0];
                    s1 = aValues[1];
                    aS0 = s0.Split('~');
                    aS1 = s1.Split('~');
                    // sDist = aS0(0)
                    // sRegn = aS0(1)
                    gdsid[0] = Convert.ToInt32(aS0[0]);
                    // gdsvar(0) = aS0(2)
                    // exstr(0) = aS0(4)

                    // gdsvardesc(0) = aS0(5)
                    gdsid[1] = Convert.ToInt32(aS1[0]);

                    // gdsvar(1) = aS1(2)
                    // exstr(1) = aS1(4)
                    // gdsvardesc(1) = aS1(5)
                    isValidRequest = true;
                    PanelResultToolBar1.SetPrintUrl("window.open('twp.aspx" + Request.Url.Query + "','_blank','width=800,status=0,toolbar=0,menubar=0,location=0,resizable=1,scrollbars=1');");
                }
                else
                    isValidRequest = false;
            }
            else
                isValidRequest = false;
        }

        public void InitTrees()
        {

            // TreeVars1.SelectedID = Request.Params("v")

            {
                //var withBlock = this.TreeVars1;
                TreeVars1.DatasetNumber = iDs;
                TreeVars1.GdsTable = oGt;
                TreeVars1.DivTitleString = "Pilih dari daftar kuesioner " + oGt.Desc;
                TreeVars1.DivTitleStringEn = oGt.Desc_En + " questionnaires list";
                TreeVars1.IsEnglish = bEn;
                TreeVars1.IsSecondLevelVisible = false;
                TreeVars1.IsContVarOnly = _isContVarOnly;
                TreeVars1.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                TreeVars1.DivContentStyle = "padding:0px 10px 0px 10px;";
                TreeVars1.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                TreeVars1.DivTitleAdditionalAttr = "onclick=\"sh0('dvroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

            }

            TreeVars1.IsSecondLevelVisible = _isContVarOnly;
        }

        public void SetUI()
        {
            if (bEn)
            {
                this.btnNext.Value = commonModule.NEXTSTRINGEN;
                this.btnPrev.Value = commonModule.PREVSTRINGEN;
            }
            else
            {
                this.btnNext.Value = commonModule.NEXTSTRING;
                this.btnPrev.Value = commonModule.PREVSTRING;
            }

            if (Request.Params["c"] != null)
            {
                if (char.IsNumber(Request.Params["c"], 0))
                {
                    if (int.Parse(Request.Params["c"]) == 1)
                    {
                        isSingleVar = false;
                    }
                    else
                    {
                        isSingleVar = true;
                    }
                }
                else
                    isSingleVar = true;
            }
            else
                isSingleVar = true;
        }

        private string WriteDiv()
        {
            StringBuilder fsOut;
            fsOut = new StringBuilder("<DIV ID=\"tip\" ");
            {
                //var withBlock = fsOut;
                fsOut.Append(" Style = \"position:absolute; ");
                fsOut.Append(" top:0px;left:0px;width:300px;height:50px; ");
                fsOut.Append(" background:yellow;z-Order:4;visibility:hidden; ");
                fsOut.Append(" border:1px solid black; ");
                fsOut.Append(" font:bold 12px Arial; ");
                fsOut.Append(" padding:3px 3px 3px 10px; ");
                fsOut.Append(" overflow:visible; ");
                fsOut.Append(" filter:Alpha(Opacity=70)\"> ");
                fsOut.Append(" </DIV> ");
            }

            return fsOut.ToString();
        }

        private void Main()
        {
            bEn = commonModule.IsEnglish();
            GetRequest();
            SetUI();
            oGt = new gdsTable(iDs);
            sQryDist = oGt.KabuNmFld;
            InitTrees();
            ViewState["ExcelFileName"] = bEn ? oGt.Desc_En : oGt.Desc;
            if (Trace.IsEnabled)
            {
                Trace.Warn("sValues", sValues);
                Trace.Warn("gdsid(0)", gdsid[0].ToString());
                Trace.Warn("gdsid(1)", gdsid[1].ToString());
            }
            // Title1.InnerHtml = "GDS2 Survey Two-Way Scatter Analysis - Dataset:" & IIf(bEn, oGt.Desc, oGt.Desc_En)

            if (isValidRequest)
            {
                dt = CreateTable();
                DataGridColumn colLbl;
                if (Request.Params["pr"] != null)
                {
                    colLbl = new BoundColumn();
                    {
                        //var withBlock = (BoundColumn)colLbl;
                        colLbl.HeaderText = bEn ? "Kecamatan" : "Kecamatan";
                        ((BoundColumn)colLbl).DataField = dt.Columns[0].ColumnName; // Prov or Dist Name
                        colLbl.SortExpression = dt.Columns[0].ColumnName; // Prov or Dist Name
                    }

                    DataGrid1.Columns.Add(colLbl);
                }
                else
                {
                    colLbl = new BoundColumn();
                    {
                        //var withBlock1 = (BoundColumn)colLbl;
                        colLbl.HeaderText = bEn ? "Kabupaten" : "Kabupaten";
                        ((BoundColumn)colLbl).DataField = dt.Columns[0].ColumnName; // Prov or Dist Name
                        colLbl.SortExpression = dt.Columns[0].ColumnName; // Prov or Dist Name
                    }

                    DataGrid1.Columns.Add(colLbl);

                    // Change to Kabu lvl from Prov lvl by request
                    // colLbl = New HyperLinkColumn
                    // With CType(colLbl, HyperLinkColumn)
                    // .HeaderText = IIf(bEn, "Province", "Propinsi")
                    // .DataTextField = dt.Columns(0).ColumnName 'Prov or Dist Name
                    // If oGt.HasKeca Then
                    // .DataNavigateUrlField = dt.Columns(dt.Columns.Count - 1).ColumnName 'Last column is the Prov or Dist Key
                    // .DataNavigateUrlFormatString = Server.UrlDecode(Request.Url.Query) & "&pr={0}"
                    // End If
                    // .SortExpression = dt.Columns(0).ColumnName 'Prov or Dist Name
                    // End With
                    // DataGrid1.Columns.Add(colLbl)
                }

                DataGrid1.Columns[0].ItemStyle.Wrap = false;
                var colMean1 = new BoundColumn();
                colMean1.DataField = dt.Columns[1].ColumnName; // Mean1
                colMean1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                if (bEn)
                    colMean1.DataFormatString = "{0:###,###,##0.00}";
                else
                    colMean1.DataFormatString = "{0:N}";

                colMean1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                colMean1.HeaderText = "Mean1";
                colMean1.SortExpression = dt.Columns[1].ColumnName;
                DataGrid1.Columns.Add(colMean1);
                var colMean2 = new BoundColumn();
                colMean2.DataField = dt.Columns[2].ColumnName; // Mean2
                colMean2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                colMean2.DataFormatString = "{0:###,###,##0.00}";
                colMean2.HeaderText = "Mean2";
                colMean2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                colMean2.SortExpression = dt.Columns[2].ColumnName;
                DataGrid1.Columns.Add(colMean2);
                DataGrid1.Caption = string.Format("<DIV STYLE=\"background-color:#003366;color:#FFFF66;border:solid 1px #CCCCFF;font:bold 10pt Arial;padding:4px 4px 4px 4px;text-align:left;\">{0} (Mean1)<HR>{1} (Mean2)</DIV>", gdsvardesc[0], gdsvardesc[1]);
                if (Request.Params["pr"] != "")
                {
                    oGg = new gdsGeo(Request.Params["pr"], "All");
                    DataGrid1.Caption += string.Format("<DIV STYLE=\"background-color:#EEEEEE;border:solid 1px #CCCCFF;font:bold 9pt Arial;padding:4px 4px 4px 4px;text-align:left;\">Propinsi {0}</DIV>", oGg.ProvName);                }

                this.DataGrid1.AllowSorting = true;
                this.DataGrid1.DataSource = dt.DefaultView;
                this.DataGrid1.DataBind();
                if (ch)
                {
                    this.WebChartViewer1.Visible = true;
                    // Me.Webchartviewer2.Visible = True
                    this.BuildChart(dt);
                    // Me.BuildChartMirror(dt)
                }

                // Me.Literal2.Text = WriteRef()
                this.Literal2.Text += WriteDiv();
            }

            if (isValidRequest)
            {
                this.pTextTw.Visible = false;
            }
            else
            {
                this.pTextTw.Visible = true;
            }
        }

    }
}