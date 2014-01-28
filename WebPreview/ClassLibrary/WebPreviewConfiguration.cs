using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;

namespace SmallSharpTools.WebPreview
{
    public static class WebPreviewConfiguration
    {

        public static string SourceImageDirectory
        {
            get
            {
                if (ConfigurationManager.AppSettings["WebPreview.SourceImageDirectory"] != null)
                {
                    return HttpContext.Current.Server.MapPath(
                        ConfigurationManager.AppSettings["WebPreview.SourceImageDirectory"]);
                }
                else
                {
                    return HttpContext.Current.Server.MapPath("~/App_Data/Generated");
                }
            }
        }

        public static string CachingImageDirectory
        {
            get
            {
                if (ConfigurationManager.AppSettings["WebPreview.CachingImageDirectory"] != null)
                {
                    return HttpContext.Current.Server.MapPath(
                        ConfigurationManager.AppSettings["WebPreview.CachingImageDirectory"]);
                }
                else
                {
                    return HttpContext.Current.Server.MapPath("~/App_Data/Generated/Cache");
                }
            }
        }

        public static int ImageCompressionRatio
        {
            get
            {
                if (ConfigurationManager.AppSettings["WebPreview.ImageCompressionRatio"] != null)
                {
                    return int.Parse(ConfigurationManager.AppSettings["WebPreview.ImageCompressionRatio"]);
                }
                else
                {
                    return 75;
                }
            }
        }
    }
}
