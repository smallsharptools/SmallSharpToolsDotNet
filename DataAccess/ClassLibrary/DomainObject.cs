using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SmallSharpTools.DataAccess
{
    [Serializable]
    [DataContract]
    public abstract class DomainObject<T, I> : IComparable where T : DomainObject<T, I>
    {

        #region "  Load Methods  "

        /// <summary>
        /// Loads the values from the DataRow and works to match up column names
        /// with Property names.  For example <code>row["Name"] = this.Name</code>
        /// and <code>row["IsActive"] = this.IsActive</code>. The Property must 
        /// also be set as public, not protected and also be writeable.
        /// </summary>
        /// <param name="row"></param>
        public void Load(DataRow row)
        {
            List<string> mappings = GetMappings(row.Table);

            foreach (string name in mappings)
            {
                SetValue(name, row);
            }
            if (row["ID"] != null)
            {
                ID.Value = (I)row["ID"];
            }
        }

        public void Load(IDataReader dr)
        {
            List<string> mappings = GetMappings(dr);

            foreach (string name in mappings)
            {
                SetValue(name, dr);
            }
            if (dr["ID"] != null)
            {
                ID.Value = (I)dr["ID"];
            }
        }

        private static Dictionary<Type, List<string>> _mappings =
          new Dictionary<Type, List<string>>();

        public List<string> GetMappings(DataTable dataTable)
        {
            Type type = GetType();
            if (!_mappings.ContainsKey(type))
            {
                _mappings[type] = CreateMappings(dataTable);
            }
            return _mappings[type];
        }

        public List<string> GetMappings(IDataReader dr)
        {
            Type type = GetType();
            if (!_mappings.ContainsKey(type))
            {
                _mappings[type] = CreateMappings(dr);
            }
            return _mappings[type];
        }

        public List<string> CreateMappings(DataTable dataTable)
        {
            List<string> mappings = new List<string>();
            Type type = GetType();

            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.CanWrite &&
                    dataTable.Columns.Contains(pi.Name) &&
                    dataTable.Columns[pi.Name].DataType.Equals(pi.PropertyType))
                {
                    mappings.Add(pi.Name);
                }
            }
            return mappings;
        }

        public List<string> CreateMappings(IDataReader dr)
        {
            List<string> mappings = new List<string>();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                PropertyInfo pi = GetProperty(dr.GetName(i));
                if (pi != null &&
                    pi.CanWrite &&
                    pi.PropertyType.Equals(dr.GetFieldType(i)))
                {
                    mappings.Add(dr.GetName(i));
                }
            }

            return mappings;
        }

        private void SetValue(string name, DataRow row)
        {
            PropertyInfo pi = GetProperty(name);
            if (!DBNull.Value.Equals(row[name]))
            {
                pi.SetValue(this, row[name], null);
            }
        }

        private void SetValue(string name, IDataReader dr)
        {
            PropertyInfo pi = GetProperty(name);
            if (!DBNull.Value.Equals(dr[name]))
            {
                pi.SetValue(this, dr[name], null);
            }
        }

        private static Dictionary<string, PropertyInfo> _properties =
          new Dictionary<string, PropertyInfo>();

        private PropertyInfo GetProperty(string name)
        {
            if (!_properties.ContainsKey(name))
            {
                _properties[name] = GetType().GetProperty(name);
            }
            return _properties[name];
        }

        #endregion

        #region "  Properties  "

        private DomainKey<I> _domainKey = DomainKey<I>.Default;

        /// <summary>
        /// Object ID
        /// </summary>
        [DataMember]
        public DomainKey<I> ID
        {
            get { return _domainKey; }
            set { _domainKey = value; }
        }

        private DateTime _created = DomainConfiguration.DefaultDateTime;

        /// <summary>
        /// Object created time
        /// </summary>
        [DataMember]
        public DateTime Created
        {
            get
            {
                return _created;
            }
            set
            {
                _created = value;
            }
        }

        private DateTime _modified = DomainConfiguration.DefaultDateTime;

        /// <summary>
        /// Object modified time
        /// </summary>
        [DataMember]
        public DateTime Modified
        {
            get
            {
                return _modified;
            }
            set
            {
                _modified = value;
            }
        }

        #endregion

        #region "  Comparison Methods  "

        /// <summary>
        /// Base override
        /// </summary>
        public int CompareTo(Object obj)
        {
            int result = 0;
            DomainObject<T, I> otherDomainObject = obj as DomainObject<T, I>;
            if (otherDomainObject != null)
            {
                result = ID.CompareTo(otherDomainObject.ID);
                if (result == 0)
                {
                    result = Created.CompareTo(otherDomainObject.Created);
                    if (result == 0)
                    {
                        result = Modified.CompareTo(otherDomainObject.Modified);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Base override
        /// </summary>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        /// Base override
        /// </summary>
        public override bool Equals(object obj)
        {
            DomainObject<T, I> domainObject = obj as DomainObject<T, I>;
            if (domainObject != null)
            {
                if (ID.Equals(domainObject.ID))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region "  Overide Methods  "

        public override string ToString()
        {
            return GetType().Name + " (" + this.ID.Value.ToString() + ")";
        }

        #endregion

    }
}
