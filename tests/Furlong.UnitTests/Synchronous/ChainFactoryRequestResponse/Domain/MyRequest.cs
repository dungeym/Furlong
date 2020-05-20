using System.Collections.Generic;

namespace Furlong.UnitTests.Synchronous.ChainFactoryRequestResponse.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}