using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SmallSharpTools.WebPreview
{
    public class ThumbnailBuilder
    {

        #region "  Constants  "

        public const int ImageCompressionQuality = 90;

        #endregion

        #region "  Events  "

        public event EventHandler ExceptionCatching;

        /// <summary>
        /// Raise the ExceptionCatching event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExceptionCatching(EventArgs e)
        {
            if (ExceptionCatching != null)
            {
                ExceptionCatching(this, e);
            }
        }

        #endregion

        #region "  Methods  "

        public void CreateThumbnail(string sourceFilename, string cachedFilename, int width, int height)
        {
            if (File.Exists(sourceFilename))
            {
                FileInfo inputFile = new FileInfo(sourceFilename);
                FileInfo outputFile = new FileInfo(cachedFilename);

                if (!outputFile.Directory.Exists)
                {
                    outputFile.Directory.Create();
                }

                if (outputFile.Exists && inputFile.CreationTime < outputFile.CreationTime)
                {
                    return;
                }

                try
                {
                    Bitmap inBmp = new Bitmap(sourceFilename);
                    //Bitmap outBmp = new Bitmap(width, height);
                    //using (Graphics g = Graphics.FromImage(outBmp))
                    //{
                    //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //    g.DrawImage(inBmp, 0, 0, width, height);
                    //}

                    // CREDITS: http://www.thebrainparasite.com/post/Creating-great-thumbnails-in-ASPNET.aspx

                    using (Bitmap outBmp = CreateThumbnail(inBmp, width, height, true))
                    {
                        //Configure JPEG Compression Engine
                        EncoderParameters encoderParams = new EncoderParameters();
                        long[] quality = new long[1];
                        quality[0] = ImageCompressionQuality;
                        EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, quality);
                        encoderParams.Param[0] = encoderParam;

                        ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                        ImageCodecInfo jpegICI = null;
                        for (int x = 0; x < arrayICI.Length; x++)
                        {
                            if (arrayICI[x].FormatDescription.Equals("JPEG"))
                            {
                                jpegICI = arrayICI[x];
                                break;
                            }
                        }

                        outBmp.Save(cachedFilename, jpegICI, encoderParams);
                    }

                    //outBmp.Save(cachedFilename);
                }
                catch (Exception ex)
                {
                    CurrentError = ex;
                    OnExceptionCatching(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// A better alternative to Image.GetThumbnail. Higher quality but slightly slower
        /// </summary>
        /// <param name="source"></param>
        /// <param name="thumbWi"></param>
        /// <param name="thumbHi"></param>
        /// <returns></returns>
        private Bitmap CreateThumbnail(Bitmap source, int thumbWi, int thumbHi, bool maintainAspect)
        {
            // return the source image if it's smaller than the designated thumbnail
            if (source.Width < thumbWi && source.Height < thumbHi) return source;

            System.Drawing.Bitmap ret = null;
            try
            {
                int wi, hi;

                wi = thumbWi;
                hi = thumbHi;

                if (maintainAspect)
                {
                    // maintain the aspect ratio despite the thumbnail size parameters
                    if (source.Width > source.Height)
                    {
                        wi = thumbWi;
                        hi = (int)(source.Height * ((decimal)thumbWi / source.Width));
                    }
                    else
                    {
                        hi = thumbHi;
                        wi = (int)(source.Width * ((decimal)thumbHi / source.Height));
                    }
                }

                ret = new Bitmap(wi, hi);
                using (Graphics g = Graphics.FromImage(ret))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(Brushes.White, 0, 0, wi, hi);
                    g.DrawImage(source, 0, 0, wi, hi);
                }
            }
            catch
            {
                ret = null;
            }

            return ret;
        }

        public string GetSourceFilename(string url)
        {
            Uri uri = new Uri(url);
            string shortFilename = uri.Host.Replace(".", "_") +
                uri.LocalPath.Replace("/", "_") + ".png";
            return Path.Combine(WebPreviewConfiguration.SourceImageDirectory, shortFilename);
        }

        public string GetCachedFilename(string sourceFilename, int width, int height)
        {
            FileInfo sourceFile = new FileInfo(sourceFilename);
            string shortFilename = sourceFile.Name;
            string ext = Path.GetExtension(shortFilename);
            string replacementEnding = String.Format("{0}x{1}-", width, height) + ext;
            string cachedFilename = shortFilename.Replace(ext, replacementEnding);
            return Path.Combine(WebPreviewConfiguration.CachingImageDirectory, cachedFilename);
        }

        public string GetContentType(string filename)
        {
            string contentType = "image/x-unknown";
            string ext = Path.GetExtension(filename);
            switch (ext)
            {
                case ".jpg":
                    contentType = "image/jpeg";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = "image/jpeg";
                    break;
            }
            return contentType;
        }

        #endregion

        #region "  Properties  "

        private Exception _currentException = null;

        public Exception CurrentError
        {
            get { return _currentException; }
            set { _currentException = value; }
        }

        #endregion

    }

}