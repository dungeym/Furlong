using System.Collections.Generic;

namespace Furlong.UnitTests.DependencyInjection.CustomInterface.Domain
{
    public class MyDependency
    {
        public List<string> Visited { get; } = new List<string>();
    }
}