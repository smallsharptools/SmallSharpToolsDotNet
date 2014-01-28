using System.ServiceModel;

namespace SmallSharpTools.Services.Generic
{

    [ServiceContract(Namespace = "http://services.smallsharptools.com/GenericService/")]
    interface IGenericService
    {
        [OperationContract]
        string HelloWorld(string name);
    }
    
    [ServiceBehavior]
    public class GenericService : IGenericService
    {
        [OperationBehavior]
        public string HelloWorld(string name)
        {
            return "Hello " + name;
        }
    }
}
