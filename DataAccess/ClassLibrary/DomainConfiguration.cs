using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SmallSharpTools.DataAccess
{
    public static class DomainConfiguration
    {

        public static readonly DateTime DefaultDateTime = new DateTime(1754, 1, 1);

    }
}
