using System;
using System.Collections.ObjectModel;
using System.Text;

namespace SmallSharpTools.VisualStudioExtensions
{
    public class CompositeProperty
    {

        #region "  Constructors  "

        public CompositeProperty()
        {
        }

        public CompositeProperty(string parentClassName, string propertyName, Type type)
        {
            _parentClassName = parentClassName;
            _propertyName = propertyName;
            _type = type;
        }

        #endregion

        #region "  Properties  "

        private string _parentClassName;

        public string ParentClassName
        {
            get { return _parentClassName; }
            set { _parentClassName = value; }
        }

        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        private string _alias;

        /// <summary>
        /// Alternate name for the property to avoid naming conflicts
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        private Type _type;

        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string FieldName
        {
            get 
            { 
                return CompositeHelper.GetFieldName(ParentClassName) + "." + PropertyName;
            }
        }

        private bool _isReadOnly = false;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }

        private bool _isHidden = false;

        public bool IsHidden
        {
            get { return _isHidden; }
            set { _isHidden = value; }
        }

        /// <summary>
        /// Returns the Alias if it is defined otherwise it returns PropertyName
        /// </summary>
        public string EffectivePropertyName
        {
            get
            {
                if (String.IsNullOrEmpty(_alias))
                {
                    return _propertyName;
                }
                else
                {
                    return _alias;
                }
            }
        }

        #endregion

        #region "  Equality Methods  "

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CompositeProperty other = obj as CompositeProperty;
            if (other == null)
            {
                return false;
            }
            return ParentClassName.Equals(other.ParentClassName) &&
                PropertyName.Equals(other.PropertyName) && 
                Type.Equals(other.Type) &&
                FieldName.Equals(other.FieldName);
        }

        #endregion

    }

    #region "  Collection  "

    public class CompositePropertyCollection : Collection<CompositeProperty>
    {

        public CompositeProperty FindCompositeProperty(string parentClassName, string propertyName)
        {
            foreach (CompositeProperty property in this)
            {
                if (property.ParentClassName.Equals(parentClassName) && 
                    property.PropertyName.Equals(propertyName))
                {
                    return property;
                }
            }
            return null;
        }

    }

    #endregion
}
