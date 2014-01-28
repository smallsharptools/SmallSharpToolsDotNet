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
        
        [DataMember]
        public string Address1 { get; set; }
        
        [DataMember]
        public string Address2 { get; set; }
        
        [DataMember]
        public string City { get; set; }
        
        [DataMember]
        public string State { get; set; }
        
        [DataMember]
        public string Zip { get; set; }
        
        [DataMember]
        public string Notes { get; set; }

    }
}
