using System.Collections.Generic;

namespace Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequest.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}