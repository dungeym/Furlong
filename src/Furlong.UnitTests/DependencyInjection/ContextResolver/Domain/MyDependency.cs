using System.Collections.Generic;

namespace Furlong.UnitTests.DependencyInjection.ContextResolver.Domain
{
    public class MyDependency
    {
        public List<string> Visited { get; } = new List<string>();
    }
}