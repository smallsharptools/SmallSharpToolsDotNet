using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sample.DataContracts
{
    [DataContract]
    public class DetailDataContract : DomainObject
    {
        private string _address1;
        
        [DataMember]
        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        private string _address2;
        
        [DataMember]
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        private string _city;
        
        [DataMember]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state;
        
        [DataMember]
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _zip;
        
        [DataMember]
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        private string _notes;
        
        [DataMember]
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

    }
}
