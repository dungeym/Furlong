using System.Collections.Generic;

namespace Furlong.UnitTests.DependencyInjection.CustomInterface.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}