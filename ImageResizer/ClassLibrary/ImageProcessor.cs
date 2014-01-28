using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SmallSharpTools.ImageResizer
{
    /// <summary>
    /// The ImageProcessor does the bulk of the work to process a directory,
    /// generate resized images and save those images into the output locations.
    /// </summary>
    public class ImageProcessor
    {

        public event EventHandler<ProgressEventArgs> ProgressChanged;
        
        private static ImageProcessor _instance = null;

        private static string defaultImageSizes = "300x225,160x120";

        private ImageProcessor() {}

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static ImageProcessor Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = new ImageProcessor();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Raise the ProgressChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProgressChanged(ProgressEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }
        
        /// <summary>
        /// Runs the image processor using the given directory
        /// </summary>
        /// <param name="directory"></param>
        public void Run(string directory)
        {
            try
            {
                ProcessDirectory(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Canceled = true;
            }
        }
        
        /// <summary>
        /// Loops over the given directory and passes each file to ProcessFile
        /// </summary>
        /// <param name="directory"></param>
        protected void ProcessDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                FileInfo[] files = dirInfo.GetFiles();
                List<FileInfo> imageFiles = new List<FileInfo>();
                foreach (FileInfo fileInfo in files)
                {
                    if (IsImageFile(fileInfo))
                    {
                        imageFiles.Add(fileInfo);
                    }
                }
                TotalFiles = imageFiles.Count;
                ProgressEventArgs args1 = new ProgressEventArgs(ProcessedFiles, TotalFiles);
                OnProgressChanged(args1);
                foreach (FileInfo fileInfo in imageFiles)
                {
                    if (Canceled)
                    {
                        return;
                    }
                    ProcessFile(fileInfo);
                    ProcessedFiles += 1;
                    ProgressEventArgs args2 = new ProgressEventArgs(ProcessedFiles, TotalFiles);
                    OnProgressChanged(args2);
                }
            }
            Completed = true;
        }
        
        protected bool IsImageFile(FileInfo fileInfo)
        {
            switch (fileInfo.Extension.ToLower())
            {
                case ".jpg":
                    break;
                case ".jpeg":
                    break;
                case ".tif":
                    break;
                case ".tiff":
                    break;
                case ".gif":
                    break;
                case ".png":
                    break;
                default:
                    // this extension is not supported
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check if the file is an image and passes it to ProcessImageFile
        /// </summary>
        /// <param name="fileInfo"></param>
        protected void ProcessFile(FileInfo fileInfo)
        {
            if (IsImageFile(fileInfo))
            {
                ProcessImageFile(fileInfo);
            }
        }
        
        /// <summary>
        /// Processes the image file.
        /// </summary>
        /// <param name="imageFileInfo"></param>
        protected void ProcessImageFile(FileInfo imageFileInfo)
        {
            if (Canceled)
            {
                return;
            }
            string[] dimensions = ImageSizes.Split(",".ToCharArray()[0]);
            Bitmap tmpBmp = new Bitmap(imageFileInfo.FullName);
            foreach (string dimension in dimensions)
            {
                string[] numbers = dimension.Split("x".ToCharArray()[0]);
                int width;
                int height;
                if (numbers.Length == 2)
                {
                    if (int.TryParse(numbers[0], out width) &&
                        int.TryParse(numbers[1], out height))
                    {
                        if (tmpBmp.Width > width)
                        {
                            CreateOutputImageFile(imageFileInfo, width, height);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Create output images for the given size in a folder named using the size.
        /// </summary>
        /// <param name="imageFileInfo"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected void CreateOutputImageFile(FileInfo imageFileInfo, int width, int height)
        {
            string directoryName = String.Format("{0}x{1}", width.ToString(), height.ToString());
            string outputPath = Path.Combine(imageFileInfo.Directory.FullName, directoryName);
            if (!Directory.Exists(outputPath))
            {
                PrepareOutputDirectory(outputPath);
            }

            Bitmap inBmp = new Bitmap(imageFileInfo.FullName);
            Bitmap outBmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(outBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(inBmp, 0, 0, width, height);
            }
            
            string outputFilename = outputPath + Path.DirectorySeparatorChar + imageFileInfo.Name;

            switch (imageFileInfo.Extension.ToLower())
            {
                case ".jpg":
                    outBmp.Save(outputFilename, ImageFormat.Jpeg);
                    break;
                case ".jpeg":
                    outBmp.Save(outputFilename, ImageFormat.Jpeg);
                    break;
                case ".tif":
                    outBmp.Save(outputFilename, ImageFormat.Tiff);
                    break;
                case ".tiff":
                    outBmp.Save(outputFilename, ImageFormat.Tiff);
                    break;
                case ".gif":
                    outBmp.Save(outputFilename, ImageFormat.Gif);
                    break;
                case ".png":
                    outBmp.Save(outputFilename, ImageFormat.Png);
                    break;
                default:
                    // do nothing
                    break;
            }
        }
        
        /// <summary>
        /// Prepare a new output directory
        /// </summary>
        /// <param name="outputDirectory"></param>
        protected void PrepareOutputDirectory(string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);
            //TODO place a text file explaining this directory was generated by this utility.
        }

        private int _processedFilesCount = 0;
        public int ProcessedFiles
        {
            get
            {
                return _processedFilesCount;
            }
            set
            {
                _processedFilesCount = value;
            }
            
        }

        private int _totalFiles = 0;
        public int TotalFiles
        {
            get
            {
                return _totalFiles;
            }
            set
            {
                _totalFiles = value;
            }

        }

        private bool _completed = false;
        public bool Completed
        {
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
            }
        }

        private bool _canceled = false;
        public bool Canceled
        {
            get
            {
                return _canceled;
            }
            set
            {
                _canceled = value;
            }
        }
        
        public string ImageSizes
        {
            get
            {
                if (ConfigurationManager.AppSettings["ImageSizes"] == null)
                {
                    return defaultImageSizes;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ImageSizes"];
                }
            }
        }
    }

}