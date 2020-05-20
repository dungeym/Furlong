namespace Furlong.UnitTests.Synchronous.ChainFactoryRequest.Domain
{
    public class MyLink1 : LinkBase<MyRequest>
    {
        private readonly bool _contiunue;

        public MyLink1(bool contiunue)
        {
            _contiunue = contiunue;
        }

        public override void Handle(MyRequest request)
        {
            request.Visited.Add(this.GetType().Name);

            if (_contiunue)
            {
                CallNext(request);
            }
        }
    }
}