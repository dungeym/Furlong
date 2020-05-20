namespace Furlong.UnitTests.DependencyInjection.ContextResolver.Domain
{
    public class MyLink2 : LinkBase<MyRequest>, IMyLink
    {
        private readonly MyDependency _dependency;

        public MyLink2(MyDependency dependency)
        {
            _dependency = dependency;
        }

        public override void Handle(MyRequest request)
        {
            request.Visited.Add(this.GetType().Name);
            _dependency.Visited.Add(this.GetType().Name);

            CallNext(request);
        }
    }
}