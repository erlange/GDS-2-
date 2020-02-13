using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gds
{
    public partial class gds2_v : System.Web.UI.Page
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
            // getDsProv()
            // If bEn Then
            // If Request.Params("c") = 1 Then
            // Me.lblContent.Text = "In this page, you can compare two continuous variables to determine the correlation between them.<br>Please select 2 choices from the questionnaires list below, then click <b>Next &gt;&gt;</b> to see the result."
            // Else
            // Me.lblContent.Text = "In this page, you can analyze one selected variable from the Questionnaires List for a province/district. You can compare the result with one or more comparators from the Comparators List, then click <b>Next &gt;&gt;</b> to see the result."
            // End If

// Else
            // If Request.Params("c") = 1 Then
            // Me.lblContent.Text = "Analisis ini memungkinkan Anda untuk membandingkan dua variabel untuk menentukan korelasi antara keduanya.<br>Pilihlah dua variabel dari daftar kuesioner berikut ini, kemudian tekan tombol <b>Lanjut &gt;&gt;</b> untuk menampilkan hasilnya."
            // Else
            // Me.lblContent.Text = "Anda dapat melihat hasil survey dari suatu variabel/kuesioner yang Anda pilih dari daftar kuesioner berikut ini untuk suatu propinsi/kabupaten.<br>Pilihlah salah satu kabupaten atau propinsi, lalu pilih salah satu variabel dari daftar kuesioner. Anda dapat juga membandingkan hasilnya dengan memilih satu pembanding atau lebih dari daftar pembanding, kemudian tekan tombol <b>Lanjut &gt;&gt;</b> untuk menampilkan hasilnya."

// End If
            // End If
            catch (Exception ex)
            {
                string newLine = System.Environment.NewLine;
                Session["errMsg"] = ex.StackTrace + newLine + Request.Url.ToString() + newLine + DateTime.Now;
                Response.Redirect("err.aspx");
            }

        }


        public void SetUI()
        {
            if (bEn)
            {
                this.btnNext.Value = commonModule.NEXTSTRINGEN;
                this.btnPrev.Value = commonModule.PREVSTRINGEN;
                this.lblTreeDs.Text = "Select a Survey Dataset: ";
            }
            else
            {
                this.btnNext.Value = commonModule.NEXTSTRING;
                this.btnPrev.Value = commonModule.PREVSTRING;
                this.lblTreeDs.Text = "Pilihan Dataset Survey: ";
            }

            if (Request.Params["c"] != null)
            {
                if (Request.Params["c"].ToString() == "1")
                {
                    isSingleVar = false;
                }
                else
                {
                    isSingleVar = true;
                }
            }
            else
            {
                isSingleVar = true;
            }

            this.pTitleV.Visible = true;
            this.pTextV.Visible = true;
            this.pTitleV.PanelId = 8;
            this.pTextV.PanelId = 6;
        }

        public void GetRequest()
        {
            bool _isEnglish = bEn;
            var _datasetNumber = default(int);
            bool _isContVarOnly = false;
            int mode;
            if (Request.Params["m"] != null)
            {
                mode = Convert.ToInt32(Request.Params["m"]);
            }
            else
            {
                mode = 0;
            }

            if (Request.Params["ds"] != null)
            {
                if (Request.Params["ds"] != "")
                {
                    _datasetNumber = Convert.ToInt32(Request.QueryString["ds"]);
                }
            }
            else
            {
                _datasetNumber = 11;
            }

            iDs = _datasetNumber;
            if (Request.Params["r"] != "")
            {
                iR = Request.Params["r"];
            }

            if (Request.Params["c"] == "1")
            {
                _isContVarOnly = true;
            }
            else if (Request.Params["c"] == "2")
            {
                var i = default(int);
                if (Request.Params["lstds2"] != "")
                {
                    i = Convert.ToInt32(Request.Params["lstds2"]);
                }

                Response.Redirect("scrmkr.aspx?ds=" + i.ToString());
            }

            {
                
                if (Request.Params["r"] == "All" & (Request.Params["d"] == "" | Request.Params["d"] == null))
                {
                    TreeLocations1.SelectedID = "All~Natl";
                }
                else
                {
                    TreeLocations1.SelectedID = Request.Params["r"] + "~All";
                }

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
                this.TreeLocations1.Visible = false;
                this.TreeComparators1.Visible = false;
                this.TreeVars1.IsSecondLevelVisible = true;
            }
        }



    }
}