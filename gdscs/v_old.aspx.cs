using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class gds2_v_old : System.Web.UI.Page
    {
        protected bool isSingleVar;
        protected int iDs;
        string iR;
        protected bool bEn;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bEn = commonModule.IsEnglish();
                MnuBottom1.SetSelectedIndex(2);
                MnuTop1.SetSelectedIndex(2);
                SetUI();
                GetRequest();
            }
            catch (Exception ex)
            {
                string newLine = System.Environment.NewLine;
                Session["errMsg"] = ex.StackTrace + newLine + Request.Url.ToString() + newLine + DateTime.Now;
                Response.Redirect("err.aspx");
            }

        }


        public void SetUI()
        {
            btnNext.Value = bEn ? commonModule.NEXTSTRINGEN : commonModule.NEXTSTRING;
            btnPrev.Value = bEn ? commonModule.PREVSTRINGEN : commonModule.PREVSTRING;
            lblTreeDs.Text = bEn ? "Select a Survey Dataset: " : "Pilihan Dataset Survey: ";

            if (Request.Params["c"] != null)
            {
                if (Request.Params["c"].ToString() == "1")
                    isSingleVar = false;
                else
                    isSingleVar = true;
            }
            else
                isSingleVar = true;

            pTitleV.Visible = true;
            pTextV.Visible = true;
            pTitleV.PanelId = 8;
            pTextV.PanelId = 6;
        }

        public void GetRequest()
        {
            bool _isEnglish = bEn;
            var _datasetNumber = default(int);
            bool _isContVarOnly = false;
            int mode;
            if (Request.Params["m"] != null)
                mode = Convert.ToInt32(Request.Params["m"]);
            else
                mode = 0;

            if (Request.Params["ds"] != null)
            {
                if (Request.Params["ds"] != "")
                    _datasetNumber = Convert.ToInt32(Request.QueryString["ds"]);
            }
            else
                _datasetNumber = 11;

            iDs = _datasetNumber;
            if (Request.Params["r"] != "")
                iR = Request.Params["r"];

            if (Request.Params["c"] == "1")
                _isContVarOnly = true;
            else if (Request.Params["c"] == "2")
            {
                var i = default(int);
                if (Request.Params["lstds2"] != "")
                    i = Convert.ToInt32(Request.Params["lstds2"]);

                Response.Redirect("scrmkr.aspx?ds=" + i.ToString());
            }

            {
                
                if (Request.Params["r"] == "All" & (Request.Params["d"] == "" | Request.Params["d"] == null))
                    TreeLocations1.SelectedID = "All~Natl";
                else
                    TreeLocations1.SelectedID = Request.Params["r"] + "~All";

                TreeLocations1.DatasetNumber = _datasetNumber;
                TreeLocations1.IsEnglish = _isEnglish;
                TreeLocations1.DivTitleString = "<b>1.</b> Pilih Propinsi/Kabupaten";
                TreeLocations1.DivTitleStringEn = "<b>1.</b> Province/District Selection";
                TreeLocations1.IsSecondLevelVisible = true;
                TreeLocations1.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                TreeLocations1.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                TreeLocations1.DivTitleAdditionalAttr = "onclick=\"sh0('dlroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

            }

            {
                TreeVars1.DatasetNumber = _datasetNumber;
                TreeVars1.GdsTable = new gdsTable(_datasetNumber);
                TreeVars1.IsEnglish = _isEnglish;
                TreeVars1.DivTitleString = "<b>2.</b> Pilih dari daftar kuesioner " + TreeVars1.GdsTable.Desc;
                TreeVars1.DivTitleStringEn = "<b>2.</b> " + TreeVars1.GdsTable.Desc_En + " questionnaires list";
                TreeVars1.IsSecondLevelVisible = true;
                TreeVars1.IsContVarOnly = _isContVarOnly;
                TreeVars1.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                TreeVars1.DivContentStyle = "padding:0px 10px 0px 10px;";
                TreeVars1.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                TreeVars1.DivTitleAdditionalAttr = "onclick=\"sh0('dvroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

            }

            {
                TreeComparators1.DatasetNumber = _datasetNumber;
                TreeComparators1.IsEnglish = _isEnglish;
                TreeComparators1.DivTitleString = "<b>3.</b> Pilih Pembanding";
                TreeComparators1.DivTitleStringEn = "<b>3.</b> Comparators Selection";
                TreeComparators1.IsSecondLevelVisible = true;
                TreeComparators1.DivContainerStyle = "background-color:#F7F8FA;border:solid 1px #ABAEBF;";
                TreeComparators1.DivTitleStyle = "background-color:#DFDFE7;color:#000000;";
                TreeComparators1.DivTitleAdditionalAttr = "onclick=\"sh0('dcroot');\"" + "onmouseover=\"this.style.backgroundColor='#C9D8FC';\"" + "onmouseout=\"this.style.backgroundColor='#DFDFE7';\"";

            }


            if (Convert.ToInt32(Request.Params["c"]) == 1)
            {
                TreeLocations1.Visible = false;
                TreeComparators1.Visible = false;
                TreeVars1.IsSecondLevelVisible = true;
            }
        }



    }
}