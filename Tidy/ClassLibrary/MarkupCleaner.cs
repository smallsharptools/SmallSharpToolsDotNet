using System;
using System.Collections.Generic;
using System.Text;
using Tidy;

namespace SmallSharpTools.Tidy
{
    public class MarkupCleaner
    {

        public event EventHandler ErrorOccured;

        protected virtual void OnErrorOccured(EventArgs e)
        {
            if (ErrorOccured != null)
            {
                ErrorOccured(this, e);
            }
        }

        public void CleanFile(string inputFilename, string outputFilename)
        {
            Document document = GetNewDocument();

            if (Status >= 0)
            {
                Status = document.ParseFile(inputFilename);
            }

            ProcessDocument(document);

            if (Status >= 0)
            {
                document.SaveFile(outputFilename);
                return;
            }

            throw new InvalidOperationException("Unable to clean document");
        }

        public string CleanFile(string inputFilename)
        {
            Document document = GetNewDocument();

            if (Status >= 0)
            {
                Status = document.ParseFile(inputFilename);
            }

            ProcessDocument(document);

            if (Status >= 0)
            {
                return document.SaveString();
            }

            throw new InvalidOperationException("Unable to clean document");
        }

        public string CleanContent(string content)
        {
            Document document = GetNewDocument();

            document.ParseString(content);
            
            ProcessDocument(document);

            if (Status >= 0)
            {
                return document.SaveString();
            }

            throw new InvalidOperationException("Unable to clean document");
        }

        public void CleanContent(string content, string outputFilename)
        {
            Document document = GetNewDocument();

            document.ParseString(content);

            ProcessDocument(document);

            if (Status >= 0)
            {
                document.SaveFile(outputFilename);
                return;
            }

            throw new InvalidOperationException("Unable to clean document");
        }

        private Document GetNewDocument()
        {
            Document document = new Document();

            if (!String.IsNullOrEmpty(OptionFile) && Status >= 0)
            {
                Status = document.LoadConfig(OptionFile);
            }

            if (!String.IsNullOrEmpty(ErrorFile) && Status >= 0)
            {
                Status = document.SetErrorFile(ErrorFile);
            }

            return document;
        }

        private void ProcessDocument(Document document)
        {

            if (Status >= 0)
            {
                Status = document.CleanAndRepair();
            }

            if (Status >= 0)
            {
                Status = document.RunDiagnostics();
            }

            if (Status > 1)
            {
                document.SetOptBool(TidyOptionId.TidyForceOutput, 1);
            }
        }

        public string GetBodyContent(string content)
        {
            string startMarker = "<body>";
            string endMarker = "</body>";
            int startLength = startMarker.Length;
            int startIndex = content.IndexOf(startMarker);
            if (startIndex != -1)
            {
                int endIndex = content.IndexOf(endMarker);
                return content.Substring(
                    startIndex + startLength, (endIndex - startIndex + startLength));
            }
            else
            {
                return content;
            }
        }

        public string GetStatusMessage()
        {
            // Note: anything below 0 is actually a severe error,
            // but it is simply assumed for the default if it does
            // not match the other defined values

            switch (Status)
            {
                case 0:
                    return "Success";
                case 1:
                    return "Warnings, No Errors";
                case 2:
                    return "Errors and Warnings";
                default:
                    return "Severe error";
            }
        }

        private int _status = 0;

        public int Status
        {
            get { return _status; }
            set {
                _status = value;
                if (value > 0)
                {
                    EventArgs args = EventArgs.Empty;
                    OnErrorOccured(args);
                }
            }
        }

        private string _optionFile = String.Empty;

        public string OptionFile
        {
            get { return _optionFile; }
            set { _optionFile = value; }
        }

        private string _errorFile = String.Empty;

        public string ErrorFile
        {
            get { return _errorFile; }
            set { _errorFile = value; }
        }

    }
}
