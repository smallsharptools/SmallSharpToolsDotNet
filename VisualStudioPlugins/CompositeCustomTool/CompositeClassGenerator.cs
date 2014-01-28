using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

using Microsoft.CSharp;
using Microsoft.VisualBasic;

using SmallSharpTools.VisualStudioExtensions;

namespace SmallSharpTools.VisualStudioPlugins.CustomTools
{
    public class CompositeClassGenerator
    {
        
        #region "  Private Variables  "

        private const string CompositeXPath = "/composite";
        private const string TypesXPath = "/composite/types/type";
        private const string PropertiesXPath = "properties/add";

        private VsHelper _vsHelper;
        private ICodeGenerator _codeGenerator;
        private string _inputFileName;
        private Assembly[] _assemblies;
        private DirectoryInfo _projectDirectory;

        #endregion

        #region "  Methods  "

        public CompositeClassGenerator(
            BaseCustomTool customTool, ICodeGenerator codeGenerator, string inputFileName)
        {
            _codeGenerator = codeGenerator;
            _inputFileName = inputFileName;
            _vsHelper = new VsHelper();
            EnvDTE.Project project = _vsHelper.GetProject();
            _assemblies = _vsHelper.GetReferencedAssemblies(customTool, false);
            _projectDirectory = _vsHelper.GetProjectDirectory();
        }

        public string GetCode()
        {
            CompositeClass compositeClass = ReadSourceFile();
            CodeCompileUnit unit = CreateCodeCompileUnit(compositeClass);
            return CovertCodeCompileUnit(unit);
        }

        public CodeCompileUnit CreateCodeCompileUnit(CompositeClass compositeClass)
        {   
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace(GetNamespace(compositeClass.RootNamespace));
            foreach (string newNamespace in compositeClass.Namespaces)
            {
                CodeNamespaceImport import = new CodeNamespaceImport(newNamespace);
                ns.Imports.Add(import);
            }
            unit.Namespaces.Add(ns);

            CodeTypeDeclaration generatedClass = new CodeTypeDeclaration(GetClassName());
            generatedClass.IsClass = true;
            generatedClass.IsPartial = true;
            generatedClass.TypeAttributes = TypeAttributes.Public;
            ns.Types.Add(generatedClass);
            
            // add private fields
            foreach (CompositeType compositeType in compositeClass.Types)
            {
                CodeMemberField privateVariable = new CodeMemberField(compositeType.ClassName, compositeType.GetFieldName());
                privateVariable.Attributes = MemberAttributes.Private;
                generatedClass.Members.Add(privateVariable);
            }

            // add constructor
            CodeConstructor ctor = new CodeConstructor();
            ctor.Attributes = MemberAttributes.Public;

            // add the parameters and field assignments
            foreach (CompositeType compositeType in compositeClass.Types)
            {
                CodeParameterDeclarationExpression param = new CodeParameterDeclarationExpression(
                    compositeType.ClassName, compositeType.GetParameterName());
                ctor.Parameters.Add(param);

                // add field assignment
                CodeFieldReferenceExpression leftRef;
                leftRef = new CodeFieldReferenceExpression();
                //leftRef.TargetObject = new CodeThisReferenceExpression();
                leftRef.FieldName = compositeType.GetFieldName();

                CodeFieldReferenceExpression rightRef;
                rightRef = new CodeFieldReferenceExpression();
                //rightRef.TargetObject = new CodeThisReferenceExpression();
                rightRef.FieldName = compositeType.GetParameterName();

                CodeAssignStatement assign = new CodeAssignStatement();
                assign.Left = leftRef;
                assign.Right = rightRef;

                ctor.Statements.Add(assign);
            }
            generatedClass.Members.Add(ctor);

            // add properties
            foreach (CompositeType compositeType in compositeClass.Types)
            {
                foreach (CompositeProperty property in compositeType.Properties)
                {
                    AttachProperty(generatedClass, compositeType, property);
                }
            }

            return unit;
        }

        private CompositeClass ReadSourceFile()
        {
            CompositeClass compositeClass = new CompositeClass();
            compositeClass.Namespaces.Add("System");
            
            XmlDocument document = new XmlDocument();
            document.Load(_inputFileName);

            XmlNode compositeNode = document.SelectSingleNode(CompositeXPath);
            compositeClass.RootNamespace = GetNodeValue(compositeNode, "@rootNamespace");

            XmlNodeList types = document.SelectNodes(TypesXPath);
            foreach (XmlNode typeNode in types)
            {
                string name = GetNodeValue(typeNode, "@name");
                string className = GetNodeValue(typeNode, "@className");
                string theNamespace = GetNodeValue(typeNode, "@namespace");
                if (!compositeClass.Namespaces.Contains(theNamespace))
                {
                    compositeClass.Namespaces.Add(theNamespace);
                }

                CompositeType compositeType = new CompositeType(name, className, theNamespace);
                compositeClass.Types.Add(compositeType);
                Type actualType = FindType(className, theNamespace);

                foreach (PropertyInfo pi in actualType.GetProperties())
                {
                    if (pi.CanRead && pi.CanWrite)
                    {
                        CompositeProperty property = new CompositeProperty(compositeType.TypeName, pi.Name, pi.PropertyType);
                        compositeType.Properties.Add(property);
                    }
                }

                XmlNodeList hiddenNodes = typeNode.SelectNodes(PropertiesXPath);
                foreach (XmlNode propertyNode in hiddenNodes)
                {
                    string propertyName = GetNodeValue(propertyNode, "@name");
                    string alias = GetNodeValue(propertyNode, "@alias");
                    bool isReadOnly = GetBooleanNodeValue(propertyNode, "@isReadOnly");
                    bool isHidden = GetBooleanNodeValue(propertyNode, "@isHidden");

                    CompositeProperty property = compositeType.Properties.FindCompositeProperty(name, propertyName);

                    if (property != null)
                    {
                        if (!String.IsNullOrEmpty(alias))
                        {
                            property.Alias = alias;
                        }
                        property.IsReadOnly = isReadOnly;
                        property.IsHidden = isHidden;
                    }
                }
            }

            return compositeClass;
        }

        private string CovertCodeCompileUnit(CodeCompileUnit unit)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CodeNamespace ns in unit.Namespaces)
            {
                sb.AppendLine(GetNamespaceCode(ns));
            }

            return sb.ToString();
        }

        private String GetNamespace(string rootNamespace)
        {
            if (String.IsNullOrEmpty(rootNamespace))
            {
                rootNamespace = "Generated";
            }
            FileInfo inputFile = new FileInfo(_inputFileName);
            DirectoryInfo directory = inputFile.Directory;
            Queue<string> directoryNames = new Queue<string>();
            while (directory != null && directory.Parent.Equals(_projectDirectory))
            {
                directoryNames.Enqueue(directory.Name);
                directory = directory.Parent;
            }
            
            StringBuilder sb = new StringBuilder();
            sb.Append(rootNamespace);

            foreach (string directoryName in directoryNames)
            {
                sb.Append(".");
                sb.Append(directoryName);
            }

            return sb.ToString();
        }

        private String GetClassName()
        {
            FileInfo inputFile = new FileInfo(_inputFileName);
            return inputFile.Name.Substring(0, inputFile.Name.Length - inputFile.Extension.Length);
        }

        private string GetNamespaceCode(CodeNamespace ns)
        {
            CodeGeneratorOptions opt = new CodeGeneratorOptions();
            opt.BracingStyle = "C";
            StringWriter sw = new StringWriter();
            CodeWriter.GenerateCodeFromNamespace( ns, sw, opt );

            return sw.ToString();
        }

        private void AttachProperty(
            CodeTypeDeclaration generatedClass, CompositeType compositeType, CompositeProperty property)
        {
            if (property.IsHidden)
            {
                return;
            }

            CodeMemberField field = new CodeMemberField(property.Type, property.FieldName);

            // public property
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = property.EffectivePropertyName;
            prop.Type = new CodeTypeReference(property.Type);
            prop.Attributes = MemberAttributes.Public;
            CodeCommentStatement commentStatement = 
                new CodeCommentStatement("<summary>\r\n " + compositeType.ClassName + "." +
                    property.EffectivePropertyName + "\r\n </summary>", true);
            prop.Comments.Add(commentStatement);

            CodeFieldReferenceExpression fieldRef;
            fieldRef = new CodeFieldReferenceExpression();
            fieldRef.TargetObject = new CodeThisReferenceExpression();
            fieldRef.FieldName = property.FieldName;

            // property getter
            CodeMethodReturnStatement ret;
            ret = new CodeMethodReturnStatement(fieldRef);
            prop.GetStatements.Add(ret);

            if (!property.IsReadOnly)
            {
                // property setter
                CodeAssignStatement assign = new CodeAssignStatement();
                assign.Left = fieldRef;
                assign.Right = new CodePropertySetValueReferenceExpression();
                prop.SetStatements.Add(assign);
            }

            generatedClass.Members.Add(prop);
        }

        private Type FindType(string typeName, string namespaceValue)
        {
            Type type = null;
            foreach (Assembly assembly in _assemblies)
            {
                type = assembly.GetType(namespaceValue + "." + typeName);
                if (type != null)
                {
                    break;
                }
            }
            return type;
        }

        private string GetNodeValue(XmlNode node, string xpath)
        {
            XmlNode valueNode = node.SelectSingleNode(xpath);
            if (valueNode != null)
            {
                return valueNode.Value;
            }
            else
            {
                return String.Empty;
            }
        }

        private bool GetBooleanNodeValue(XmlNode node, string xpath)
        {
            string value = GetNodeValue(node, xpath);
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return bool.Parse(value);
        }

        private ICodeGenerator CodeWriter
        {
            get
            {
                return _codeGenerator;
            }
        }

        #endregion

    }
}
