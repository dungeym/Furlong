using Furlong.Synchronous;

namespace Furlong.UnitTests.DependencyInjection.ContextResolver.Domain
{
    public interface IMyLink : IChainLink<MyRequest>
    {
    }
}