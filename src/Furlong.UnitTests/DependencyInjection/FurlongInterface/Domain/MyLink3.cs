namespace Furlong.UnitTests.DependencyInjection.FurlongInterface.Domain
{
    public class MyLink3 : LinkBase<MyRequest>
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