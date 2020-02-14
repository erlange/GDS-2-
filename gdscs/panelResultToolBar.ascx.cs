using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class panelResultToolBar : System.Web.UI.UserControl
    {
        public event btnExcelClickEventHandler btnExcelClick;
        public delegate void btnExcelClickEventHandler(object sender, EventArgs e);

        public event btnPrintClickEventHandler btnPrintClick;
        public delegate void btnPrintClickEventHandler(object sender, EventArgs e);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (commonModule.IsEnglish())
            {
                this.lblVarSel.Text = "Select Other Variable";
            }
            else
            {
                this.lblVarSel.Text = "Pilih Variabel Lain";
            }
        }
        public void SetPrintUrl(string url)
        {
            btnPrint2.Attributes.Add("onclick", url);

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            
        }


    }
}