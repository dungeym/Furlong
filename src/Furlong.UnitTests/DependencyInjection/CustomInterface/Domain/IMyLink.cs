using Furlong.Synchronous;

namespace Furlong.UnitTests.DependencyInjection.CustomInterface.Domain
{
    public interface IMyLink : IChainLink<MyRequest>
    {
    }
}