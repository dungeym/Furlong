namespace Furlong.UnitTests.DependencyInjection.ContextResolver.Domain
{
    public class MyLink3 : LinkBase<MyRequest>, IMyLink
    {
        private readonly MyDependency _dependency;

        public MyLink3(MyDependency dependency)
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