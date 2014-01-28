using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Text;

namespace SmallSharpTools.VSX.Composite.CustomTools
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
            try
            {
                // old way when there was GetCodeWriter in the base class
                CompositeClassGenerator generator =
                    new CompositeClassGenerator(this, CodeProvider, inputFileName);
                string code = generator.GetCode();
                return Encoding.ASCII.GetBytes(code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 


    }
}
