using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallSharpTools;
using SmallSharpTools.Logging;
using SmallSharpTools.YahooPack.Providers;
using SmallSharpTools.YahooPack.Web.Security;

public partial class PhotosList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();

         if (yahooSession.IsAuthenticated)
         {
             string url = "http://photos.yahooapis.com/V3.0/listPhotos";
             
             DataSet dsServices;
             dsServices = yahooSession.GetService(new Uri(url));

             dlPhotos.DataSource = dsServices.Tables["Image"];
             dlPhotos.DataBind();
             
             //foreach (DataRow row in dsServices.Tables["Image"].Rows)
             //{
             //    if ("thumb".Equals(row["type"]))
             //    {
             //        int width;
             //        int height;
             //        int.TryParse(row["Width"].ToString(), out width);
             //        int.TryParse(row["Height"].ToString(), out height);
             //        Image img = new Image();
             //        img.ID = "img_" + row["ImageList_Id"];
             //        img.Width = width;
             //        img.Height= height;
             //        img.AlternateText = "";
             //        img.ImageUrl = row["Image_Text"].ToString();
             //        Panel1.Controls.Add(img);
             //        Panel1.Controls.Add(new LiteralControl("<br />\n"));
             //    }
             //}
         }
    }
    
    protected void dlPhotos_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            DataRow row = ((DataRowView)(e.Item.DataItem)).Row;
            if ("thumb".Equals( row["type"]))
            {   
                Image img = e.Item.FindControl("Image1") as Image;
                if (img != null)
                {
                    int width;
                    int height;
                    int.TryParse(row["width"].ToString(), out width);
                    int.TryParse(row["height"].ToString(), out height);
                    img.ImageUrl = row["Image_Text"].ToString();
                    img.Width = width;
                    img.Height = height;
                }
            }
        }
    }

    private ILogger Logger
    {
        get
        {
            return LoggingProvider.Instance.GetLogger(GetType());
        }
    }
}
