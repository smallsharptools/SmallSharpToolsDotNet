using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace SmallSharpTools.VSX.Composite
{
    internal class AssemblyHelper
    {

        internal static string GetAssemblyDetails(string path)
        {
            StringBuilder sb = new StringBuilder();
            Assembly assembly = LoadAssembly(path);

            List<Type> types = new List<Type>();
            Module[] modules = assembly.GetLoadedModules();
            foreach (Module module in modules)
            {
                foreach (Type type in module.GetTypes())
                {
                    sb.AppendLine("/* Name: " + type.Name + " */");
                    sb.AppendLine("/* FullName: " + type.FullName + " */");
                    sb.AppendLine(String.Empty);
                    sb.AppendLine("/* Properties */");
                    sb.AppendLine(String.Empty);
                    foreach (PropertyInfo pi in type.GetProperties())
                    {
                        sb.AppendLine("/* Name: " + pi.Name + " */");
                        sb.AppendLine("/* PropertyType: " + pi.PropertyType.Name + " */");
                        sb.AppendLine("/* CanRead: " + pi.CanRead + " */");
                        sb.AppendLine("/* CanWrite: " + pi.CanWrite + " */");
                        sb.AppendLine(String.Empty);
                    }
                }
            }

            return sb.ToString();
        }

        internal static Assembly LoadAssembly(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[4096];
                using (MemoryStream memStream = new MemoryStream())
                {
                    while (stream.Read(buffer, 0, buffer.Length) > 0)
                    {
                        memStream.Write(buffer, 0, buffer.Length);
                    }
                    Assembly assembly = Assembly.Load(memStream.ToArray());
                    return assembly;
                }
            }
        }

    }
}
