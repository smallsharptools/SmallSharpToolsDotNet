using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sample.DataContracts
{
    [DataContract]
    public class SummaryDataContract : DomainObject
    {
        private string _firstName;

        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        
        private string _lastName;
        
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _email;
        
        [DataMember]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;
        
        [DataMember]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

    }
}
