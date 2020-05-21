using System.Threading;
using System.Threading.Tasks;

namespace Furlong.UnitTests.Asynchronous.AsyncChainFactoryRequestResponse.Domain
{
    public class MyLink2 : AsyncLinkBase<MyRequest, MyResponse>
    {
        private readonly bool _contiunue;

        public MyLink2(bool contiunue)
        {
            _contiunue = contiunue;
        }

        public override async Task<MyResponse> HandleAsync(MyRequest request, CancellationToken cancellationToken = default)
        {
            request.Visited.Add(this.GetType().Name);

            if (_contiunue)
            {
                return await CallNextAsync(request, cancellationToken);
            }
            else
            {
                return new MyResponse(this);
            }
        }
    }
}