using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using ChartDirector;
using Microsoft.VisualBasic;

namespace gds
{
    public partial class panelHighlight : System.Web.UI.UserControl
    {
        private int _gdsId;
        private int _varId;
        private int _islandId;


        public int GdsId
        {
            get
            {
                return _gdsId;
            }
            set
            {
                _gdsId = value;
            }
        }
        public int VarId
        {
            get
            {
                return _varId;
            }
            set
            {
                _varId = value;
            }
        }
        public int IslandId
        {
            get
            {
                return _islandId;
            }
            set
            {
                _islandId = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillListBoxes();
                _islandId = Convert.ToInt32(lstIsland.SelectedItem.Value);
                _varId = Convert.ToInt32(lstVar.SelectedItem.Value);
            }
            ShowData(11, Convert.ToInt32(lstVar.SelectedItem.Value),Convert.ToInt32( lstIsland.SelectedItem.Value), this.lstIsland.SelectedItem.Text);
        }



        public void ShowData(int gdsId, int varId, int islandId, string islandName)
        {
            gdsHighlight hl = new gdsHighlight();
            DataTable dt = hl.GetSurveyHighlightByIsland(gdsId, varId, islandId, commonModule.IsEnglish());
            // Me.DataGrid1.DataSource = dt
            // Me.DataGrid1.DataBind()
            if (commonModule.IsEnglish())
                ShowChart(dt, hl.VarDescEn, varId, islandName);
            else
                ShowChart(dt, hl.VarDesc, varId, islandName);
        }

        public void FillListBoxes()
        {
            System.Data.DataSet ds;
            string filename = Server.MapPath("highlight.xml");
            if (Cache["highlight"] == null)
            {
                ds = new System.Data.DataSet();
                ds.ReadXml(filename);
                Cache.Insert("highlight", ds, new System.Web.Caching.CacheDependency(filename));
            }

            {
                var withBlock = this.lstIsland.Items;
                if (commonModule.IsEnglish())
                {
                    withBlock.Add(new ListItem("National", "0"));
                    withBlock.Add(new ListItem("Sumatera", "1"));
                    withBlock.Add(new ListItem("Java & Bali", "2"));
                }
                else
                {
                    withBlock.Add(new ListItem("Nasional", "0"));
                    withBlock.Add(new ListItem("Sumatera", "1"));
                    withBlock.Add(new ListItem("Jawa & Bali", "2"));
                }
                withBlock.Add(new ListItem("Kalimantan", "3"));
                withBlock.Add(new ListItem("Sulawesi", "4"));
                withBlock.Add(new ListItem("NTB & NTT", "5"));
                withBlock.Add(new ListItem("Maluku & Papua", "6"));
                
            }
            lstVar.DataSource = ((System.Data.DataSet)Cache["highlight"]).Tables[0];
            lstVar.DataValueField = "var_id";
            if (commonModule.IsEnglish())
                lstVar.DataTextField = "desc_en";
            else
                lstVar.DataTextField = "desc";
            lstVar.DataBind();
        }


        public void ShowChart(DataTable table, string desc, int varId, string islandName)
        {
            const int INTBOTTOMTEXTHEIGHT = 20;
            const int BASEHEIGHT = 200;
            const int BASEWIDTH = 200;
            const int RADIUS = 50;

            ChartDirector.TextBox tbTitle;
            ChartDirector.TextBox tbFooter;
            ChartDirector.TextBox tbIsland;
            ChartDirector.LegendBox tbLegend;
            PieChart tmpChart;
            PieChart c;

            int iRowsCount;
            iRowsCount = table.Rows.Count;
            double[] data = new double[iRowsCount - 1 + 1];
            string[] labels = new string[iRowsCount - 1 + 1];
            string title;
            string iSumTotal = "";

            title = Microsoft.VisualBasic.Strings.Left( commonModule.FormatByLength(desc, 30), commonModule.FormatByLength(desc, 30).Length - 1); // removes trailing Chr(10)

            for (int i = 0; i <= iRowsCount - 1; i++)
            {
                iSumTotal += table.Rows[i]["CountNatl"];
                data[i] = Convert.ToDouble( table.Rows[i]["CountNatl"]);
            }
            for (int i = 0; i <= iRowsCount - 1; i++)
                labels[i] = table.Rows[i]["desc"] + " (" + Microsoft.VisualBasic.Strings.FormatPercent(Convert.ToDouble(table.Rows[i]["CountNatl"]) / Convert.ToDouble( iSumTotal)) + ")";
            int tmpHeight = BASEHEIGHT + (10 * iRowsCount);
            int h = BASEHEIGHT + (10 * iRowsCount);

            tmpChart = new PieChart(BASEWIDTH, tmpHeight);
            tbTitle = tmpChart.addTitle2(Chart.Top, title, "Arialbd.ttf", 8);
            tbLegend = tmpChart.addLegend(20, 120, true, "Arial.ttf", 8);
            tmpChart.setData(data, labels);

            this.chart1.Image = tmpChart.makeWebImage(Chart.PNG); // panggil dulu untuk tahu tb.GetHeight()
            // tmpChart.layout()

            int iLabelHeight = tbTitle.getHeight();
            int iLegendW = tbLegend.getWidth();
            int iLegendH = tbLegend.getHeight();
            int iChartW;
            int iChartH;

            if (BASEWIDTH > iLegendW)
                iChartW = BASEWIDTH;
            else
                iChartW = iLegendW + 10;

            iChartH = iLabelHeight + iLegendH + RADIUS + RADIUS + INTBOTTOMTEXTHEIGHT;
            c = new PieChart(iChartW, iChartH);

            tbTitle = c.addTitle2(Chart.Top, title, "Arialbd.ttf", 8);
            tbTitle.setBackground(0xCCCCFF, 0xCCCCFF, 0);
            c.setPieSize(iChartW / 2, iLabelHeight + RADIUS, RADIUS);
            c.set3D(15);

            tbLegend = c.addLegend((iChartW - iLegendW) / 2, iLabelHeight + (2 * RADIUS), true, "Arial.ttf", 8);
            tbLegend.setBackground(Chart.Transparent, Chart.Transparent, 0);

            tbIsland = c.addText(5, iLabelHeight + 2, islandName, "Arialb.ttf", 8, 0x3366);

            c.setLabelStyle("Arial.ttf", 8, Chart.Transparent);
            c.setStartAngle(135);
            c.setLabelPos(-40);

            c.setData(data, labels);
            c.setAntiAlias(true, 1);
            c.setBackground(0xFFFFFF, 0xCCCCFF, 1);
            if (commonModule.IsEnglish())
                tbFooter = c.addText(0, iChartH - INTBOTTOMTEXTHEIGHT, "GDS 2 - Households Survey", "arial.ttf", 7, 0x55, Chart.Center);
            else
                tbFooter = c.addText(0, iChartH - INTBOTTOMTEXTHEIGHT, "GDS 2 - Survei Rumah Tangga", "arial.ttf", 7, 0x55, Chart.Center);
            tbFooter.setBackground(0xCCCCFF, 0xCCCCFF, 0);
            tbFooter.setSize(iChartW, INTBOTTOMTEXTHEIGHT);
            this.chart1.Image = c.makeWebImage(Chart.PNG);

            string clickUrl;
            clickUrl = string.Format("pvOut.aspx?v={0}&cp1=1&r=All&d=Natl&ds=11&c=0&ch=1", varId);
            if (commonModule.IsEnglish())
                chart1.ImageMap = c.getHTMLImageMap(clickUrl, "", "title='{label}. Count: {value}'");
            else
                chart1.ImageMap = c.getHTMLImageMap(clickUrl, "", "title='{label}. Jumlah: {value}'");
        }


    }
}