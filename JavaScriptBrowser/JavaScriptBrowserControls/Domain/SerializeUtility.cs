using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace SmallSharpTools.JavaScriptBrowser.Controls.Domain
{
    public class SerializeUtility<T>
    {
        public const string StorageDirectory = "SmallSharpTools.JavaScriptBrowser";

        #region "  Public Methods  "

        public void SerializeData(T data, string filename)
        {
            CreateStorageDirectory();
            string tempFilePath = GetTempFilePath(filename);
            string filePath = GetFilePath(filename);
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
            using (FileStream outputStream = File.OpenWrite(tempFilePath))
            {
                //IFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(outputStream, data);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(outputStream, data);
                outputStream.Close();
            }
            // now move the temp file over the actual file
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public T DeserializeData(string filename)
        {
            if (!Directory.Exists(GetStoragePath()))
            {
                return default(T);
            }

            string filePath = GetFilePath(filename);
            using (FileStream inputStream = File.OpenRead(filePath))
            {
                try
                {
                    //IFormatter formatter = new BinaryFormatter();
                    //object obj = formatter.Deserialize(inputStream);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    object obj = serializer.Deserialize(inputStream);
                    return (T)obj;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Format not recognized", ex);
                }
            }
        }

        public static string GetFilePath(string filename)
        {
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Filename is required", "filename");
            }
            return Path.Combine(GetStoragePath(), filename);
        }

        public static void CreateStorageDirectory()
        {
            string path = GetStoragePath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion

        #region "  Protected Methods  "

        protected string GetTempFilePath(string filename)
        {
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Filename is required", "filename");
            }
            return Path.Combine(GetStoragePath(), filename + ".tmp");
        }

        protected static string GetStoragePath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                StorageDirectory);
        }

        #endregion

    }
}
