using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

using SmallSharpTools.VisualStudioExtensions;

namespace SmallSharpTools.VisualStudioPlugins.CustomTools
{
    [ComVisible(true)]
    [Guid("4621DA88-2F9A-4999-A04C-44B3498718BC")]
    //[CLSCompliant(false)]
    public class CompositeCustomTool : BaseCustomTool
    {

        /// <summary>
        /// Does the code generation.
        /// </summary>
        /// <param name="inputFileName">The name of the input file.</param>
        /// <param name="inputFileContent">unused</param>
        /// <returns>The generated code bytes.</returns>
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            CompositeClassGenerator generator = 
                new CompositeClassGenerator(this, GetCodeWriter(), inputFileName);
            string code = generator.GetCode();
            return Encoding.ASCII.GetBytes(code);
        }

    }
}
