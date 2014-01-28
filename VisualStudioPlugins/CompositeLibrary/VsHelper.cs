using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using EnvDTE;
using EnvDTE80;
using VSLangProj;
using VSLangProj2;
using VSLangProj80;

using Microsoft.CustomTool;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace SmallSharpTools.VisualStudioExtensions
{
    public class VsHelper
    {

        public Project GetProject()
        {
            // TODO fix, this does not work
            //DTE2 dte2 = (DTE2) Microsoft.VisualBasic.Interaction.CreateObject("VisualStudio.DTE.8.0", String.Empty);
            DTE2 dte2 = Marshal.GetActiveObject("VisualStudio.DTE.8.0") as DTE2;
            foreach (object obj in dte2.Solution.Projects)
            {
                string typeName = Information.TypeName(obj);
                Console.WriteLine(typeName);
                Project proj = (Project) obj;
                Console.WriteLine(proj.FullName);
            }
            Project project = dte2.Solution.Projects.Item(1).ProjectItems.ContainingProject;
            Debug.Assert(project != null);
            return project;
        }

        /// <summary>
        /// Gets a list of the assembliesy referenced by the project
        /// </summary>
        /// <param name="isInclusive">Include assemblies from C:\Windows or not</param>
        /// <returns></returns>
        public Assembly[] GetReferencedAssemblies(bool isInclusive)
        {
            Project project = GetProject();
            return GetReferencedAssemblies(project, isInclusive);
        }
        public Assembly[] GetReferencedAssemblies(BaseCustomTool customTool, bool isInclusive)
        {
            Project project = customTool.GetProject();
            return GetReferencedAssemblies(project, isInclusive);
        }

        public Assembly[] GetReferencedAssemblies(Project project, bool isInclusive)
        {
            List<Assembly> assemblies = new List<Assembly>();
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
                catch (Exception ex)
                {
                    // TODO show an alert with the error message
                    throw;
                }
            }
            return assemblies.ToArray();
        }

        public DirectoryInfo GetProjectDirectory()
        {
            Project project = GetProject();
            FileInfo projectFile = new FileInfo(project.FileName);
            return projectFile.Directory;
        }

        public string GetSolutionDetails()
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
