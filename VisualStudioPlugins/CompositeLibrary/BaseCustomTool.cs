using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using EnvDTE;
using EnvDTE80;
using VSLangProj;
using VSLangProj2;
using VSLangProj80;

using Microsoft.CustomTool;

namespace SmallSharpTools.VisualStudioExtensions
{
    public abstract class BaseCustomTool : BaseCodeGeneratorWithSite
    {

        protected abstract override byte[] GenerateCode(string inputFileName, string inputFileContent);

        internal Project GetProject()
        {
            ProjectItem projectItem = (ProjectItem) this.GetService(typeof(ProjectItem));
            return projectItem.ContainingProject;
        }

        /// <summary>
        /// Gets the assemblies referenced by the project
        /// </summary>
        /// <param name="isInclusive">Include assemblies from C:\Windows or not</param>
        /// <returns></returns>
        internal Assembly[] GetReferencedAssemblies(bool isInclusive)
        {
            VsHelper vsHelper = new VsHelper();
            return vsHelper.GetReferencedAssemblies(isInclusive);
        }

        internal string GetSolutionDetails()
        {
            VsHelper vsHelper = new VsHelper();
            return vsHelper.GetSolutionDetails();
        }

    }
}
