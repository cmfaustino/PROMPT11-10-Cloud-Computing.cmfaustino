using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace EchoService
{
    public interface IEchoChannel : IEchoContract, IClientChannel { }
}
