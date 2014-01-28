using System.ServiceModel;
using SmallSharpTools.Services.Provider;

namespace SmallSharpTools.Services.Manager
{
    [ServiceContract]
    interface IServicesManager
    {
        [OperationContract]
        HostedService[] GetHostedServices();

        [OperationContract]
        void StartService(string name);

        [OperationContract]
        void StopService(string name);
    }

    public class ServicesManager : IServicesManager
    {
        public HostedService[] GetHostedServices()
        {
            HostedServiceCollection hostedServices = new HostedServiceCollection();
            foreach (ServicesProvider provider in ServicesAdmin.Instance.Providers)
            {
                HostedService hostedService = 
                    new HostedService(provider.Name, provider.Description, 
                        provider.IsActive, provider.IsManager);
                hostedServices.Add(hostedService);
            }
            return hostedServices.ToArray();
        }

        public void StartService(string name)
        {
            ServicesProvider provider = ServicesAdmin.Instance.GetProvider(name);
            if (provider != null)
            {
                provider.Start();
            }
        }

        public void StopService(string name)
        {
            ServicesProvider provider = ServicesAdmin.Instance.GetProvider(name);
            if (provider != null)
            {
                provider.Stop();
            }
        }
    }
}
