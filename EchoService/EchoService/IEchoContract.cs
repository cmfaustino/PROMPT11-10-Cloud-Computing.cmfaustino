using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace EchoService
{
    [ServiceContract(Name = "IEchoContract", Namespace = "http://samples.com/ServiceModel/Relay/")] // apagar espaco antes de http
    public interface IEchoContract
    {
        [OperationContract]
        String Echo(string text);
    }
}
