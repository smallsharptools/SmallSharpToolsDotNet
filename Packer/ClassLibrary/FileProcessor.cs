#region Copyright © 2007-2009 Brennan Stehling. SmallSharpTools.com. All rights reserved.
/*
Copyright © 2007-2009 Brennan Stehling. SmallSharpTools.com. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS" AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion

using System;
using System.Text;
using System.IO;
using SmallSharpTools.Packer.Utilities.Dean.Edwards;
using SmallSharpTools.Packer.Utilities.Isaac.Schlueter;
using SmallSharpTools.Packer.Utilities.JavaScriptSupport;
using System.Web;

namespace SmallSharpTools.Packer.Utilities
{

    /// <summary>
    /// The list of the modes available to the Packer
    /// </summary>
    public enum PackMode
    {
        /// <summary>
        /// Use Packer by Dean Edwards
        /// </summary>
        Packer,
        /// <summary>
        /// Use JSMin by Douglas Crockford
        /// </summary>
        JSMin,
        /// <summary>
        /// Use CSSMinify by Isaac Schlueter
        /// </summary>
        CSSMin,
        /// <summary>
        /// Use Combine to just combine multiple files into one
        /// </summary>
        Combine
    }

    /// <summary>
    /// Compresses provided input files according the selected method
    /// </summary>
    public class FileProcessor
    {

        #region "  Constants  "

        private const string PackerName = "Packer";
        private const string JsMinName = "JSMin";
        private const string CSSMinName = "CSSMin";
        private const string CombineName = "Combine";

        private const string PackerBanner = "/*\n" +
                                            " * " + PackerName + "\n" +
                                            " * Javascript Compressor\n" +
                                            " * http://dean.edwards.name/\n" +
                                            " * http://www.smallsharptools.com/Projects/Packer/\n" +
                                            "*/\n";

        private const string JsMinBanner = "/*\n" +
                                           " * " + JsMinName + "\n" +
                                           " * Javascript Compressor\n" +
                                           " * http://www.crockford.com/\n" +
                                           " * http://www.smallsharptools.com/Projects/Packer/\n" +
                                           "*/\n";

        private const string CSSMinBanner = "/*\n" +
                                           " * " + CSSMinName + "\n" +
                                           " * CSS Compressor\n" +
                                           " * http://foohack.com/\n" +
                                           " * http://www.smallsharptools.com/Projects/Packer/\n" +
                                           "*/\n";

        private const string CombineBanner = "/*\n" +
                                           " * " + CombineName + "\n" +
                                           " * Multiple files combined into one.\n" +
                                           " * http://www.smallsharptools.com/Projects/Packer/\n" +
                                           "*/\n";

        #endregion

        /// <summary>
        /// Combines and compresses input files using the selected method.
        /// </summary>
        /// <param name="mode">Compression method</param>
        /// <param name="inputFiles">Files to compress</param>
        /// <param name="verbose">Display informational messages and warnings</param>
        /// <returns>Compressed content</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if an unknown PackMode is provided</exception>
        public static string Run(PackMode mode, string[] inputFiles, bool verbose)
        {
            StringBuilder output = new StringBuilder();

            try
            {
                switch (mode)
                {
                    case PackMode.Packer:
                        output.AppendLine(PackerBanner);
                        break;
                    case PackMode.JSMin:
                        output.AppendLine(JsMinBanner);
                        break;
                    case PackMode.CSSMin:
                        output.AppendLine(CSSMinBanner);
                        break;
                    case PackMode.Combine:
                        output.AppendLine(CombineBanner);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("mode", mode, "Mode provided was not recognized");
                }

                foreach (string filename in inputFiles)
                {
                    StringBuilder sb = new StringBuilder();

                    if (File.Exists(filename))
                    {
                        if (verbose)
                        {
                            FileProcessor.Logger.WriteMessage("Processing " + filename, MessageType.Normal);
                            if (filename.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                            {
                                output.AppendLine("// " + filename);
                            }
                            else if (filename.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase))
                            {
                                output.AppendLine("/* " + filename + " */");
                            }
                        }

                        using (StreamReader streamReader = File.OpenText(filename))
                        {
                            sb.AppendLine(streamReader.ReadToEnd());
                        }

                        // run file prrocessor on each file (to do work on smaller pieces)
                        switch (mode)
                        {
                            case PackMode.Packer:
                                output.AppendLine(RunPacker(sb.ToString()));
                                break;
                            case PackMode.JSMin:
                                output.AppendLine(RunJsMin(sb.ToString()));
                                break;
                            case PackMode.CSSMin:
                                output.AppendLine(RunCSSMin(sb.ToString()));
                                break;
                            case PackMode.Combine:
                                output.AppendLine(RunCombine(sb.ToString()));
                                break;
                        }
                    }
                    else
                    {
                        FileProcessor.Logger.WriteMessage("File does not exist: " + filename, MessageType.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                FileProcessor.Logger.WriteMessage(ex.Message, MessageType.Error);
            }
            return output.ToString();
        }

        /// <summary>
        /// Combines and compresses input files using the selected method.
        /// </summary>
        /// <param name="outputFile">File to write the resulting file to</param>
        /// <param name="mode">Compression method</param>
        /// <param name="inputFiles">Files to compress</param>
        /// <param name="verbose">Display informational messages and warnings</param>
        /// <returns>Compressed files</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if an unknown PackMode is provided</exception>
        public static bool Run(string outputFile, PackMode mode, string[] inputFiles, bool verbose)
        {
            string output = Run(mode, inputFiles, verbose);

            try
            {
                FileProcessor.Logger.WriteMessage("Writing output to " + outputFile, MessageType.Normal);

                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                StreamWriter sw = new StreamWriter(outputFile, false);
                StringReader stringReader = new StringReader(output);

                while (stringReader.Peek() > 0)
                {
                    string line = stringReader.ReadLine();
                    sw.WriteLine(line);
                }

                stringReader.Close();
                sw.Close();
            }
            catch (Exception ex)
            {
                FileProcessor.Logger.WriteMessage(ex.Message, MessageType.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Packs the provided script
        /// </summary>
        /// <param name="script">Script to pack</param>
        /// <returns>Packed script</returns>
        private static string RunPacker(string script)
        {
            ECMAScriptPacker p = new ECMAScriptPacker(ECMAScriptPacker.PackerEncoding.Normal, true, false);
            return p.Pack(script).Replace("\n", "\r\n");
        }

        /// <summary>
        /// Minifies the provided script
        /// </summary>
        /// <param name="script">Script to minify</param>
        /// <returns>Minified script</returns>
        private static string RunJsMin(string script)
        {
            return JavaScriptMinifier.Minify(script);
        }

        /// <summary>
        /// Minifies the provided CSS
        /// </summary>
        /// <param name="css">CSS to minify</param>
        /// <returns>Minified CSS</returns>
        private static string RunCSSMin(string css)
        {
            return CSSMinifier.Minify(css);
        }
        
        /// <summary>
        /// Combine simply appends multiple files together
        /// </summary>
        /// <param name="text">Input Text</param>
        /// <returns>Combined output</returns>
        private static string RunCombine(string text)
        {
            return text;
        }

        /// <summary>
        /// Set the logger. Defaults to ConsoleLogger.
        /// </summary>
        /// <param name="logger"></param>
        public static void SetLogger(ILogger logger)
        {
            Logger = logger;
        }

        private static ILogger _logger;

        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    if (HttpContext.Current != null)
                    {
                        _logger = new WebLogger();
                    }
                    else
                    {
                        _logger = new ConsoleLogger();
                    }
                }
                return _logger;
            }
            private set
            {
                _logger = value;
            }
        }

    }

}
