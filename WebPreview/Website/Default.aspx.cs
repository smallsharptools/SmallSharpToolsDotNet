using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<string> sites = new List<string>();
            sites.Add("http://www.apple.com/");
            sites.Add("http://www.digg.com/");
            sites.Add("http://www.blizzard.com/");
            sites.Add("http://www.bungie.com/");
            sites.Add("http://www.marvel.com/");
            sites.Add("http://www.homestarrunner.com/");
            sites.Add("http://www.msnbc.com/");
            sites.Add("http://www.cnn.com/");
            sites.Add("http://www.microsoft.com/");
            sites.Add("http://channel9.msdn.com/");
            sites.Add("http://www.asp.net/");
            sites.Add("http://www.iis.net/");
            rptImages.DataSource = sites;
            rptImages.DataBind();
        }
    }

    public string GetWebPreviewUrl(string site)
    {
        return ResolveClientUrl("~/WebPreviewHandler.ashx") +
            String.Format("?url={0}&width=200&height=150", HttpUtility.UrlDecode(site));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(TextBox1.Text))
        {
            string url = ResolveClientUrl("~/WebPreviewHandler.ashx") + "?url=" +
                HttpUtility.UrlEncode(TextBox1.Text) + "&width=200&height=150";
            Image1.ImageUrl = url;
            Image1.Visible = true;
        }
    }

    protected void rptImages_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        String site = e.Item.DataItem as String;
        Image image = e.Item.FindControl("Image1") as Image;
        if (!String.IsNullOrEmpty(site) && image != null)
        {
            image.ImageUrl = GetWebPreviewUrl(site);
            image.AlternateText = site;
            image.Attributes["title"] = site;
            HyperLink hyperlink = e.Item.FindControl("HyperLink1") as HyperLink;
            if (hyperlink != null)
            {
                hyperlink.NavigateUrl = site;
            }
        }
    }
}