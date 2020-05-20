using System.Collections.Generic;

namespace Furlong.UnitTests.Asynchronous.AsyncChainFactoryRequestResponse.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}