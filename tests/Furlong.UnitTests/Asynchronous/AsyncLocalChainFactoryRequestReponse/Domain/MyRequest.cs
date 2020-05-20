using System.Collections.Generic;

namespace Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequestReponse.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}