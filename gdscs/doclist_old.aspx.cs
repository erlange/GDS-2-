using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace gds
{
    public partial class doclist_old : System.Web.UI.Page
    {
        panelDocList p;
        protected void Page_Load(object sender, EventArgs e)
        {
            MnuTop1.SetSelectedIndex(3);
            MnuBottom1.SetSelectedIndex(3);
            var cat = new gdsDocumentCategories();
            if (!string.IsNullOrEmpty(Request.Params["id"]))
            {
                if (char.IsNumber(Request.Params["id"], 0))
                {
                    if (Convert.ToBoolean(cat.GetCategoryById(Convert.ToInt32(Request.Params["id"]))))
                    {
                        p = (panelDocList)Page.LoadControl("panelDocList.ascx");
                        p.CategoryId = cat.CategoryId;
                        PlaceHolder1.Controls.Add(p);
                        PlaceHolder1.Controls.Add(new WebControl(HtmlTextWriterTag.Br));
                    }
                }
                else
                {
                    DataTable dt = cat.GetCategories();
                    for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                    {
                        p = (panelDocList)Page.LoadControl("panelDocList.ascx");
                        p.CategoryId = Convert.ToInt32(dt.Rows[i]["categoryid"]);
                        PlaceHolder1.Controls.Add(p);
                        PlaceHolder1.Controls.Add(new WebControl(HtmlTextWriterTag.Br));
                    }
                }
            }
            else
            {
                DataTable dt = cat.GetCategories();
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    p = (panelDocList)Page.LoadControl("panelDocList.ascx");
                    p.CategoryId = Convert.ToInt32(dt.Rows[i]["categoryid"]);
                    PlaceHolder1.Controls.Add(p);
                    PlaceHolder1.Controls.Add(new WebControl(HtmlTextWriterTag.Br));
                }

            }

        }
    }
}