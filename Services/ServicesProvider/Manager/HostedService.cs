using System;
using System.Runtime.Serialization;

namespace SmallSharpTools.Services.Manager
{
    [DataContract]
    public class HostedService
    {
        public HostedService(string name, string description, bool isActive, bool isManager)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            IsManager = isManager;
        }

        private string _name = String.Empty;

        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
        private string _description = String.Empty;

        [DataMember]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private bool _isActive = false;

        [DataMember]
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        private bool _isManager = false;

        [DataMember]
        public bool IsManager
        {
            get
            {
                return _isManager;
            }
            set
            {
                _isManager = value;
            }
        }

    }
}
