using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

using EnvDTE;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

//using Microsoft.CustomTool;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Microsoft.VisualStudio.Designer.Interfaces;
using VSLangProj80;
using System.IO;

namespace SmallSharpTools.VSX.Composite
{
    public abstract class BaseCustomTool : BaseCodeGeneratorWithSite
    {

        private static Guid CodeDomInterfaceGuid = new Guid("{73E59688-C7C4-4a85-AF64-A538754784C5}");
        private static Guid CodeDomServiceGuid = CodeDomInterfaceGuid;

        protected abstract override byte[] GenerateCode(string inputFileName, string inputFileContent);

        public override string GetDefaultExtension()
        {
            string fileExtension = this.CodeProvider.FileExtension;
            if (((fileExtension != null) && (fileExtension.Length > 0)) && (fileExtension[0] != '.'))
            {
                fileExtension = "." + fileExtension;
            }
            return fileExtension;
        }

        private CodeDomProvider codeDomProvider;

        protected virtual CodeDomProvider CodeProvider
        {
            get
            {
                if (codeDomProvider == null)
                {
                    IVSMDCodeDomProvider service = (IVSMDCodeDomProvider)this.GetService(CodeDomServiceGuid);
                    if (service != null)
                    {
                        codeDomProvider = (CodeDomProvider)service.CodeDomProvider;
                    }
                }
                return codeDomProvider;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                this.codeDomProvider = value;
            }
        }

        internal Project GetProject()
        {
            ProjectItem projectItem = (ProjectItem)this.GetService(typeof(ProjectItem));
            Debug.Assert(projectItem != null);
            return projectItem.ContainingProject;
        }

        internal Solution GetSolution()
        {
            Solution solution = (Solution)this.GetService(typeof(Solution));
            Debug.Assert(solution != null);
            return solution;
        }

        public DirectoryInfo GetProjectDirectory()
        {
            Project project = GetProject();
            FileInfo projectFile = new FileInfo(project.FileName);
            return projectFile.Directory;
        }

        /// <summary>
        /// Gets a list of the assemblies referenced by the project
        /// </summary>
        /// <param name="isInclusive">Include assemblies from C:\Windows or not</param>
        /// <returns></returns>
        public Assembly[] GetReferencedAssemblies(bool isInclusive)
        {
            List<Assembly> assemblies = new List<Assembly>();
            Project project = GetProject();
            VSProject2 vsProject2 = project.Object as VSProject2;

            foreach (object obj in vsProject2.References)
            {
                try
                {
                    string windir = Environment.GetEnvironmentVariable("windir");
                    Reference3 ref3 = obj as Reference3;
                    if (ref3 != null)
                    {
                        // filter out the standard assemblies (isInclusive)
                        if (isInclusive || (!isInclusive && !ref3.Path.StartsWith(windir)))
                        {
                            Assembly assembly = AssemblyHelper.LoadAssembly(ref3.Path);
                            assemblies.Add(assembly);
                        }
                    }
                }
                catch
                {
                    // TODO show an alert with the error message
                    throw;
                }
            }
            return assemblies.ToArray();
        }

        protected string GetSolutionDetails()
        {
            StringBuilder code = new StringBuilder();
            Project project = GetProject();
            VSProject2 vsProject2 = project.Object as VSProject2;
            FileInfo projectFile = new FileInfo(project.FileName);
            DirectoryInfo projectDir = projectFile.Directory;

            code.AppendLine("/************/");
            code.AppendLine("/* Solution */");
            code.AppendLine("/************/");
            code.AppendLine("/* FileName: " + project.DTE.Solution.FileName + " */");
            code.AppendLine(String.Empty);

            code.AppendLine("/***********/");
            code.AppendLine("/* Project */");
            code.AppendLine("/***********/");
            code.AppendLine("/* Name: " + project.Name + " */");
            code.AppendLine("/* FileName: " + project.FileName + " */");
            code.AppendLine("/* FullName: " + project.FullName + " */");
            code.AppendLine("/* UniqueName: " + project.UniqueName + " */");
            code.AppendLine("/* FullName: " + projectDir.FullName + " */");
            code.AppendLine("/* TypeName: " + Information.TypeName(project.Object) + " */");
            code.AppendLine(String.Empty);

            if (vsProject2 != null)
            {
                code.AppendLine("/**************/");
                code.AppendLine("/* VSProject2 */");
                code.AppendLine("/**************/");
                code.AppendLine("/* VSProject2: " + vsProject2.ToString() + " */");
                code.AppendLine(String.Empty);

                code.AppendLine("/**************/");
                code.AppendLine("/* References */");
                code.AppendLine("/**************/");
                foreach (object obj in vsProject2.References)
                {

                    try
                    {
                        string windir = Environment.GetEnvironmentVariable("windir");
                        Reference3 ref3 = obj as Reference3;
                        if (ref3 != null)
                        {
                            // filter out the standard assemblies
                            if (!ref3.Path.StartsWith(windir))
                            {
                                code.AppendLine("/* TypeName: " + Information.TypeName(obj) + " */");
                                code.AppendLine("/* Name: " + ref3.Name + " */");
                                code.AppendLine("/* Path: " + ref3.Path + " */");
                                code.AppendLine("/* AutoReferenced: " + ref3.AutoReferenced + " */");
                                code.AppendLine("/* Description: " + ref3.Description + " */");
                                code.AppendLine("/* Resolved: " + ref3.Resolved + " */");
                                code.AppendLine("/* Version: " + ref3.Version + " */");

                                code.AppendLine(AssemblyHelper.GetAssemblyDetails(ref3.Path));
                                code.AppendLine(String.Empty);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        code.AppendLine("/* Exception: " + ex.Message + " */");
                    }
                }
            }

            return code.ToString();
        }

    }
}
