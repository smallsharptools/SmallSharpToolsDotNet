#region Copyright © 2007 Brennan Stehling. SmallSharpTools.com. All rights reserved.
/*
Copyright © 2007 Brennan Stehling. SmallSharpTools.com. All rights reserved.

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
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using SmallSharpTools.Tidy;

namespace SmallSharpTools.Tidy.MSBuild
{
    public class Tidy : Task
    {

        #region "  Properties  "

        private bool _verbose = true;

        /// <summary>
        /// Controls whether additional details are printed to the console
        /// </summary>
        public bool Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }

        private string _optionFile;

        /// <summary>
        /// Specifies the configuration file for Tidy (optional)
        /// </summary>
        public string OptionFile
        {
            get { return _optionFile; }
            set { _optionFile = value; }
        }

        private string _errorFile;

        /// <summary>
        /// Specifies the file where errors will be logged (optional)
        /// </summary>
        public string ErrorFile
        {
            get { return _errorFile; }
            set { _errorFile = value; }
        }

        private ITaskItem[] _inputFiles;

        /// <summary>
        /// Gets or sets the input files to pack.
        /// </summary>
        /// <value>The files to tidy.</value>
        [Required]
        public ITaskItem[] InputFiles
        {
            get { return _inputFiles; }
            set { _inputFiles = value; }
        }

        private ITaskItem[] _outputFiles;

        /// <summary>
        /// Gets or sets the output files to pack.
        /// </summary>
        /// <value>The files to output.</value>
        [Required]
        public ITaskItem[] OutputFiles
        {
            get { return _outputFiles; }
            set { _outputFiles = value; }
        }

        #endregion

        #region "  Task Overrides  "

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            List<string> inputFiles = new List<string>();
            foreach (ITaskItem item in InputFiles)
            {
                inputFiles.Add(item.ItemSpec);
            }
            List<string> outputFiles = new List<string>();
            foreach (ITaskItem item in OutputFiles)
            {
                outputFiles.Add(item.ItemSpec);
            }

            MarkupCleaner cleaner = new MarkupCleaner();
            cleaner.OptionFile = OptionFile;
            cleaner.ErrorFile = ErrorFile;

            for (int i = 0; i < inputFiles.Count; i++)
            {
                if (Verbose)
                {
                    Console.WriteLine("Cleaning: " + inputFiles[i] + " -> " + outputFiles[i]);
                }
                cleaner.CleanFile(inputFiles[i], outputFiles[i]);
            }

            return true;
        }

        #endregion

    }
}
