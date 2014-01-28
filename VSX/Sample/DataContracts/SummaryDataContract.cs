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

        [DataMember]
        public string FirstName { get; set; }
        
        [DataMember]
        public string LastName { get; set; }
        
        [DataMember]
        public string Email { get; set; }
        
        [DataMember]
        public string Phone { get; set; }

    }
}
