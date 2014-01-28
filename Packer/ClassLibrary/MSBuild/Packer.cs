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
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using SmallSharpTools.Packer.Utilities;
using System.IO;
using Util = SmallSharpTools.Packer.Utilities;
using Properties = SmallSharpTools.Packer.Utilities.Properties;

namespace SmallSharpTools.Packer.MSBuild
{

    public class Packer : Task, Util.ILogger
    {

        #region "  Properties  "

        private PackMode _packMode = PackMode.Packer;

        /// <summary>
        /// Gets or sets the pack mode.
        /// </summary>
        /// <value>The command.</value>
        /// <enum cref="SmallSharpTools.Packer.MSBuild.PackMode"/>
        public string Mode
        {
            get { return _packMode.ToString(); }
            set
            {
                if (Enum.IsDefined(typeof(PackMode), value))
                {
                    _packMode = (PackMode)Enum.Parse(typeof(PackMode), value);
                }
                else
                {
                    throw new ArgumentException(
                        string.Format("The value '{0}' is not in the PackMode Enum.", value));
                }
            }
        }

        private ITaskItem[] _inputFiles;

        /// <summary>
        /// Gets or sets the input files to pack.
        /// </summary>
        /// <value>The files to pack.</value>
        [Required]
        public ITaskItem[] InputFiles
        {
            get { return _inputFiles; }
            set { _inputFiles = value; }
        }

        private string _outputFileName;

        /// <summary>
        /// Gets or sets the output filename
        /// </summary>
        public string OutputFileName
        {
            get { return _outputFileName; }
            set { _outputFileName = value; }
        }

        private string _workingDirectory;

        /// <summary>
        /// Gets or sets the working directory for the zip file.
        /// </summary>
        /// <value>The working directory.</value>
        /// <remarks>
        /// The working directory is the base of the zip file.  
        /// All files will be made relative from the working directory.
        /// </remarks>
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set { _workingDirectory = value; }
        }

        private bool _verbose;

        /// <summary>
        /// Controls whether additional details are printed to the console
        /// </summary>
        public bool Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
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
            FileProcessor.SetLogger(this);
            if (!String.IsNullOrEmpty(WorkingDirectory))
            {
                if (Directory.Exists(WorkingDirectory))
                {
                    Directory.SetCurrentDirectory(WorkingDirectory);
                }
                else
                {
                    FileProcessor.Logger.WriteMessage(
                        String.Format(Properties.Resources.PackerWorkingDirectoryNotFound, WorkingDirectory), 
                        MessageType.Error);
                    return false;
                }
            }
            PackMode mode = (PackMode)Enum.Parse(typeof(PackMode), Mode);
            List<string> inputFiles = new List<string>();
            foreach (ITaskItem item in InputFiles)
            {
                inputFiles.Add(item.ItemSpec);
            }
            return FileProcessor.Run(OutputFileName, mode, inputFiles.ToArray(), Verbose);
        }

        #endregion

        #region ILogger Members

        public void WriteMessage(string message, MessageType messageType)
        {
            if (messageType == MessageType.Normal)
            {
                Log.LogMessage(message);
            }
            else if (messageType == MessageType.Warning)
            {
                Log.LogWarning(message);
            }
            else if (messageType == MessageType.Error)
            {
                Log.LogError(message);
            }
        }

        #endregion

    }

}
