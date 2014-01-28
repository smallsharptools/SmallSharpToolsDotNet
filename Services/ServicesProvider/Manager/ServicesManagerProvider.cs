using System;
using SmallSharpTools.Services.Provider;

namespace SmallSharpTools.Services.Manager
{
    class ServicesManagerProvider : ServicesProvider
    {
        public override Type GetServiceType()
        {
            return typeof (ServicesManager);
        }
    }
}
