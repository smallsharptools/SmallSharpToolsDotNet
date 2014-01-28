namespace Sample
{
    using System;
    using Sample.DataContracts;
    
    
    public partial class Composite
    {
        
        private SummaryDataContract _summary;
        
        private DetailDataContract _detail;
        
        public Composite(SummaryDataContract summary, DetailDataContract detail)
        {
            _summary = summary;
            _detail = detail;
        }
        
        /// <summary>
        /// SummaryDataContract.FirstName
        /// </summary>
        public virtual string FirstName
        {
            get
            {
                return this._summary.FirstName;
            }
            set
            {
                this._summary.FirstName = value;
            }
        }
        
        /// <summary>
        /// SummaryDataContract.LastName
        /// </summary>
        public virtual string LastName
        {
            get
            {
                return this._summary.LastName;
            }
            set
            {
                this._summary.LastName = value;
            }
        }
        
        /// <summary>
        /// SummaryDataContract.Phone
        /// </summary>
        public virtual string Phone
        {
            get
            {
                return this._summary.Phone;
            }
            set
            {
                this._summary.Phone = value;
            }
        }
        
        /// <summary>
        /// SummaryDataContract.ID
        /// </summary>
        public virtual int ID
        {
            get
            {
                return this._summary.ID;
            }
        }
        
        /// <summary>
        /// SummaryDataContract.Created
        /// </summary>
        public virtual System.DateTime Created
        {
            get
            {
                return this._summary.Created;
            }
        }
        
        /// <summary>
        /// SummaryDataContract.Modified
        /// </summary>
        public virtual System.DateTime Modified
        {
            get
            {
                return this._summary.Modified;
            }
        }
        
        /// <summary>
        /// DetailDataContract.Address1
        /// </summary>
        public virtual string Address1
        {
            get
            {
                return this._detail.Address1;
            }
            set
            {
                this._detail.Address1 = value;
            }
        }
        
        /// <summary>
        /// DetailDataContract.Address2
        /// </summary>
        public virtual string Address2
        {
            get
            {
                return this._detail.Address2;
            }
            set
            {
                this._detail.Address2 = value;
            }
        }
        
        /// <summary>
        /// DetailDataContract.City
        /// </summary>
        public virtual string City
        {
            get
            {
                return this._detail.City;
            }
            set
            {
                this._detail.City = value;
            }
        }
        
        /// <summary>
        /// DetailDataContract.State
        /// </summary>
        public virtual string State
        {
            get
            {
                return this._detail.State;
            }
            set
            {
                this._detail.State = value;
            }
        }
        
        /// <summary>
        /// DetailDataContract.Zip
        /// </summary>
        public virtual string Zip
        {
            get
            {
                return this._detail.Zip;
            }
            set
            {
                this._detail.Zip = value;
            }
        }
        
        /// <summary>
        /// DetailDataContract.DetailID
        /// </summary>
        public virtual int DetailID
        {
            get
            {
                return this._detail.ID;
            }
        }
        
        /// <summary>
        /// DetailDataContract.DetailCreated
        /// </summary>
        public virtual System.DateTime DetailCreated
        {
            get
            {
                return this._detail.Created;
            }
        }
        
        /// <summary>
        /// DetailDataContract.DetailModified
        /// </summary>
        public virtual System.DateTime DetailModified
        {
            get
            {
                return this._detail.Modified;
            }
        }
    }
}

