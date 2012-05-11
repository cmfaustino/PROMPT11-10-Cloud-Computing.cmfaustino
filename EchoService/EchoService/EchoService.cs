using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace EchoService
{
    [ServiceBehavior(Name = "EchoService", Namespace = "http://samples.com/ServiceModel/Relay/")] // apagar espaco antes de http
    class EchoService : IEchoContract
    {
        public string Echo(string text)
        {
            Console.WriteLine("Echoing: {0}", text);
            return text;
        }
    }
}
