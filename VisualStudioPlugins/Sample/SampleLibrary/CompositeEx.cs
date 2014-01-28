using System;
using System.Collections.Generic;
using System.Text;
using Sample.DataContracts;

namespace Sample
{
    public partial class Composite
    {

        /// <summary>
        /// Full name (first and last name)
        /// </summary>
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
