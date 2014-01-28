// XsdClassesGen.cs: Chris Sells [csells@sellsbrothers.com]
// Wrapper around 'xsd.exe foo.xsd /classes'
#region Copyright © 2002 Chris Sells
/* This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the
 * use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software in a
 *    product, an acknowledgment in the product documentation is requested, as
 *    shown here:
 * 
 *    Portions copyright © 2002 Chris Sells (http://www.sellsbrothers.com/).
 * 
 * 2. No substantial portion of this source code may be redistributed without
 *    the express written permission of the copyright holders, where
 *    "substantial" is defined as enough code to be recognizably from this code.
 */
#endregion

using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Win32;  // Registry
using System.Diagnostics;
using Microsoft.VisualStudio.TextTemplating.VSHost;   // Process
//using SellsBrothers.VSDesigner.CodeGenerator; // Replacement for VS.NET design classes

/* Registration:
 * c:/> regasm /codebase XsdClassesGen.dll
 * 
 * Unregistration:
 * c:/> regasm /unregister XsdClassesGen.dll
 *
 * Usage:
 * Add a .xsd file in the correct format and set:
 *  Build Action: Content
 *  Custom Tool: SBXsdClassesGenerator
 */

namespace XsdClassesGenerator
{
    public class XsdClassesGenerator
    {
        public XsdClassesGenerator(string language)
        {
            this.language = language;
        }

        protected static string GetStringFromFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public string GenerateCodeFromXsd(string xsdSource, string defaultNamespace)
        {
            string codeReturn = "";

            string tempXsdFile = null, codeFile = null;
            try
            {
                // Find xsd.exe
                // TODO: Would be nice to use same BCL classes that xsd.exe uses,
                //		if they're available.
                string sdkInstallRoot = "";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework"))
                {
                    //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework[sdkInstallRoot]
                    Version rtVersion = System.Environment.Version;
                    string sdkInstallRootKey = "sdkInstallRootv1.0";

                    //CHANGED: the two lines directly below were changed to enable this in VS2005B1 as well
                    if (rtVersion > new Version("2.0")) sdkInstallRootKey = "sdkInstallRootv2.0";
                    else if (rtVersion > new Version("1.1")) sdkInstallRootKey = "sdkInstallRootv1.1";

                    sdkInstallRoot = (string)key.GetValue(sdkInstallRootKey);
                }

                string xsdExe = sdkInstallRoot + @"bin\xsd.exe";
                if (!File.Exists(xsdExe)) throw new ApplicationException("Can't find xsd.exe: " + xsdExe);

                // Get a temp dir for output
                string outputDir = Path.GetTempPath();
                if (outputDir.EndsWith(@"\"))
                {
                    // Strip trailing \ for xsd.exe, which doesn't like it if it's in quotes
                    outputDir = outputDir.Substring(0, outputDir.Length - 1);
                }

                // Create the file to feed into xsd.exe
                tempXsdFile = Path.ChangeExtension(Path.GetTempFileName(), "xsd");
                using (StreamWriter writer = new StreamWriter(tempXsdFile))
                    writer.Write(xsdSource);

                // Fire up xsd.exe
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = xsdExe;
                info.Arguments = "\"" + tempXsdFile + "\"" +
                  " /classes" +
                  " /l:" + language +
                  (defaultNamespace != "" ? (" /n:" + defaultNamespace) : "") +
                  " /o:" + "\"" + outputDir + "\"";
                info.CreateNoWindow = true;
                info.RedirectStandardError = true;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                using (Process process = Process.Start(info)) process.WaitForExit();

                // Harvest output
                codeFile = Path.ChangeExtension(tempXsdFile, language);
                codeReturn = GetStringFromFile(codeFile);
            }
            finally
            {
                // Delete temp files
                // (NOTE: System.IO.File guarantees that exceptions are not thrown if files don't exist)
                if (tempXsdFile != null) File.Delete(tempXsdFile);
                if (codeFile != null) File.Delete(codeFile);
            }

            return codeReturn;
        }

        protected string language;
    }

    public class CSharpXsdClassesGenerator : XsdClassesGenerator
    {
        public CSharpXsdClassesGenerator() : base("CS") { }
    }

    public class VBXsdClassesGenerator : XsdClassesGenerator
    {
        public VBXsdClassesGenerator() : base("VB") { }
    }

    public class JScriptXsdClassesGenerator : XsdClassesGenerator
    {
        public JScriptXsdClassesGenerator() : base("JS") { }
    }

    public abstract class VsXsdClassGenerator : BaseCodeGeneratorWithSite
    {
        public byte[] GenerateCode(XsdClassesGenerator generator, string fileName, string fileContents)
        {
            string code = "";
            try
            {

                code = generator.GenerateCodeFromXsd(fileContents, this.FileNamespace);
            }
            catch (Exception e)
            {
                code = "***ERROR***\n" + e.Message;
            }

            return System.Text.Encoding.ASCII.GetBytes(code);
        }

        public string DefaultNamespace
        {
            get
            {
                // HACK this is hard-coded which is very bad
                return "MyNamespace";
            }
        }

        protected static Guid CSharpCategoryGuid = new Guid("{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}");
        protected static Guid VBCategoryGuid = new Guid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}");

        protected static Version GetVsVersion()
        {
            Version rtVersion = System.Environment.Version;
            if (rtVersion <= new Version("1.1")) return new Version("7.0");

            //CHANGED: the two lines directly below were changed to enable this in VS2005B1 as well
            else if (rtVersion <= new Version("2.0")) return new Version("7.1");
            return new Version("8.0");
        }

        protected static string GetKeyName(Guid categoryGuid, Version vsVersion)
        {
            string name = "SBXsdClassesGenerator";
            string ver = (vsVersion == null ? GetVsVersion() : vsVersion).ToString();
            return @"SOFTWARE\Microsoft\VisualStudio\" + ver + @"\Generators\{" + categoryGuid.ToString() + @"}\" + name;
        }

        protected static void RegisterCustomTool(Guid categoryGuid, Type generatorType, string desc, Version vsVersion)
        {
            /*
          * [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\7.0\Generators\{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}\SBCollectionGenerator]
          * @="Sells Brothers C# Code Generator for Type-Safe Collections"
          * "CLSID"="{3D05404F-6ECF-42b2-8435-2AB63382FFA2}"
          * "GeneratesDesignTimeSource"=dword:00000001
          */

            // Assume GUID is associated with this class via the GuidAttribute.
            // Since this is required for COM interop to work with VS.NET, this seems reasonable
            string generatorGuid = ((GuidAttribute)(generatorType.GetCustomAttributes(typeof(GuidAttribute), true)[0])).Value;

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(GetKeyName(categoryGuid, vsVersion)))
            {
                key.SetValue("", desc);
                key.SetValue("CLSID", "{" + generatorGuid + "}");
                key.SetValue("GeneratesDesignTimeSource", 1);
            }
        }

        protected static void UnregisterCustomTool(Guid categoryGuid, Type generatorType, Version vsVersion)
        {
            Registry.LocalMachine.DeleteSubKey(GetKeyName(categoryGuid, vsVersion), false);
        }
    }

    [Guid("3D05404F-6ECF-42b2-8435-2AB63382FFA2")]
    public class VsCSharpXsdClassesGenerator : VsXsdClassGenerator
    {

        public override string GetDefaultExtension()
        {
            return ".cs";
        }

        protected override byte[] GenerateCode(string fileName, string fileContents)
        {
            return GenerateCode(new CSharpXsdClassesGenerator(), fileName, fileContents);
        }

        [ComRegisterFunction]
        public static void RegisterClass(Type t)
        {
            Guid category = CSharpCategoryGuid;
            Type type = typeof(VsCSharpXsdClassesGenerator);
            string desc = "Sells Brothers C# Code Generator for XSD classes";

            // CHANGED for VS2005
            // Should work for both VS.NET 2002, 2003 and 2005B1
            RegisterCustomTool(category, type, desc, new Version(7, 0));
            RegisterCustomTool(category, type, desc, new Version(7, 1));
            RegisterCustomTool(category, type, desc, new Version(8, 0));
        }

        [ComUnregisterFunction]
        public static void UnregisterClass(Type t)
        {
            Guid category = CSharpCategoryGuid;
            Type type = typeof(VsCSharpXsdClassesGenerator);

            // CHANGED for VS2005
            // Should work for both VS.NET 2002, 2003 and 2005B1
            UnregisterCustomTool(category, type, new Version(7, 0));
            UnregisterCustomTool(category, type, new Version(7, 1));
            UnregisterCustomTool(category, type, new Version(8, 0));
        }

    }

    [Guid("F8F34971-A8B5-48bc-8FBA-924E44A942CC")]
    public class VsVBXsdClassesGenerator : VsXsdClassGenerator
    {

        public override string GetDefaultExtension()
        {
            return ".vb";
        }

        protected override byte[] GenerateCode(string fileName, string fileContents)
        {
            return GenerateCode(new VBXsdClassesGenerator(), fileName, fileContents);
        }

        [ComRegisterFunction]
        public static void RegisterClass(Type t)
        {
            Guid category = VBCategoryGuid;
            Type type = typeof(VsVBXsdClassesGenerator);
            string desc = "Sells Brothers VB Code Generator for XSD classes";

            // CHANGED for VS2005
            // Should work for both VS.NET 2002, 2003 and 2005B1
            RegisterCustomTool(category, type, desc, new Version(7, 0));
            RegisterCustomTool(category, type, desc, new Version(7, 1));
            RegisterCustomTool(category, type, desc, new Version(8, 0));
        }

        [ComUnregisterFunction]
        public static void UnregisterClass(Type t)
        {
            Guid category = VBCategoryGuid;
            Type type = typeof(VsVBXsdClassesGenerator);

            // CHANGED for VS2005
            // Should work for both VS.NET 2002, 2003 and 2005
            UnregisterCustomTool(category, type, new Version(7, 0));
            UnregisterCustomTool(category, type, new Version(7, 1));
            UnregisterCustomTool(category, type, new Version(8, 0));
        }

    }
}
