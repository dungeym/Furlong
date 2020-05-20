using System.Collections.Generic;

namespace Furlong.UnitTests.Synchronous.LocalChainFactoryRequest.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}