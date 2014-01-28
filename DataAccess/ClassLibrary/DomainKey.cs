using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SmallSharpTools.DataAccess
{
    [Serializable]
    [DataContract]
    public class DomainKey<T> : IComparable<DomainKey<T>>
    {

        public DomainKey(T keyValue)
        {
            Value = keyValue;
        }

        private T _keyValue;

        [DataMember]
        public T Value
        {
            get { return _keyValue; }
            set
            {
                // check in the order which is most likely
                if (value is T)
                {
                    _keyValue = value;
                }
                else
                {
                    throw new Exception("Type not supported as a DomainKey: " + value.GetType());
                }
            }
        }

        public static DomainKey<T> Default
        {
            get
            {
                DomainKey<T> defaultKey = new DomainKey<T>(default(T));
                if (defaultKey.Value is int)
                {
                    object obj = -1;
                    defaultKey.Value = (T)obj;
                }
                else if (defaultKey.Value is long)
                {
                    object obj = -1L;
                    defaultKey.Value = (T)obj;
                }
                else if (defaultKey.Value is DateTime)
                {
                    object obj = DomainConfiguration.DefaultDateTime;
                    defaultKey.Value = (T)obj;
                }
                return defaultKey;
            }
        }

        #region "  Comparison Methods  "

        public int CompareTo(DomainKey<T> other)
        {
            if (Value is int)
            {
                object obj1 = Value;
                object obj2 = other.Value;
                int thisInt = (int)obj1;
                int otherInt = (int)obj2;
                return thisInt.CompareTo(otherInt);
            }
            return Value.ToString().CompareTo(other.Value.ToString());
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is DomainKey<T>)
            {
                DomainKey<T> other = (DomainKey<T>)obj;
                return Equals(other);
            }
            return false;
        }

        public bool Equals(DomainKey<T> other)
        {
            if (other != null && Value.Equals(other.Value))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region "  Overide Methods  "

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion

    }
}
