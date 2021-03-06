﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
namespace gds
{
    public partial class mnuBottom : System.Web.UI.UserControl
    {
        protected HtmlTableCell tdLogo;
        private int _selectedIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ShowData();
        }


        public void SetSelectedIndex(int index)
        {
            _selectedIndex = index;
        }

        private void ShowData()
        {
            DataSet ds;
            DataView dv;
            if (Cache["mnuTop"] == null)
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("mnuTop.xml"));
                Cache.Insert("mnuTop", ds, new CacheDependency(Server.MapPath("mnuTop.xml")));
            }
            else
            {
                ds = (DataSet)Cache["mnuTop"];
            }

            dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "isvisible=1";
            dv.Sort = "sortorder ASC";
            this.Repeater1.DataSource = dv;
            this.Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableCell tdl = (HtmlTableCell)e.Item.FindControl("tdl");
                HtmlTableCell tdc = (HtmlTableCell)e.Item.FindControl("tdc");
                HtmlTableCell tdr = (HtmlTableCell)e.Item.FindControl("tdr");
                HtmlAnchor lblTitle = (HtmlAnchor)e.Item.FindControl("lblTitle");
                if (commonModule.IsEnglish())
                {
                    lblTitle.InnerHtml = drv["linktitle_en"].ToString();
                    lblTitle.Title = drv["linktooltip_en"].ToString();
                }
                else
                {
                    lblTitle.InnerHtml = drv["linktitle"].ToString();
                    lblTitle.Title = drv["linktooltip"].ToString();
                }

                if (drv["id"].ToString() == _selectedIndex.ToString())
                {
                    tdl.Attributes.Add("background", "images/yyl.gif");
                    tdc.Attributes.Add("background", "images/yyy.gif");
                    tdr.Attributes.Add("background", "images/yyr.gif");
                    lblTitle.Style.Add("color", "navy");
                }
            }

        }
    }
}