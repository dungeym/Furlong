namespace Furlong.UnitTests.DependencyInjection.ContextResolver.Domain
{
    public delegate IMyLink MyLinkResolver(string context);

    public class MyLinkFactory
    {
        private readonly MyLinkResolver _resolver;

        public MyLinkFactory(MyLinkResolver resolver)
        {
            _resolver = resolver;
        }

        public IMyLink Initialize(string tenant)
        {
            return _resolver(tenant);
        }
    }
}