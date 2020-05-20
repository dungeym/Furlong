using System.Collections.Generic;

namespace Furlong.UnitTests.Synchronous.LocalChainFactoryRequestResponse.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}