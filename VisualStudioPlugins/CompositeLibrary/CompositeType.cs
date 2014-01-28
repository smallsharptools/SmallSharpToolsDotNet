using System;
using System.Collections.ObjectModel;
using System.Text;

namespace SmallSharpTools.VisualStudioExtensions
{
    public class CompositeType
    {

        #region "  Constructors  "

        public CompositeType()
        {
        }

        public CompositeType(string typeName, string className, string theNamespace)
        {
            _typeName = typeName;
            _className = className;
            _namespace = theNamespace;
        }

        #endregion

        #region "  Methods  "

        public string GetParameterName()
        {
            return CompositeHelper.GetParameterName(TypeName);
        }

        public string GetFieldName()
        {
            return CompositeHelper.GetFieldName(TypeName);
        }

        #endregion

        #region "  Properties  "

        private string _typeName;

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        private string _className;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        private string _namespace;

        public string Namespace
        {
            get { return _namespace; }
            set { _namespace = value; }
        }

        private CompositePropertyCollection _properties = new CompositePropertyCollection();

        public CompositePropertyCollection Properties
        {
            get { return _properties; }
        }

        #endregion

    }

    #region "  Collection  "

    public class CompositeTypeCollection : Collection<CompositeType>
    {
    }

    #endregion
}
