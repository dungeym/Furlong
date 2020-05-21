using System.Threading;
using System.Threading.Tasks;

namespace Furlong.UnitTests.Asynchronous.AsyncChainFactoryRequest.Domain
{
    public class MyLink2 : AsyncLinkBase<MyRequest>
    {
        private readonly bool _contiunue;

        public MyLink2(bool contiunue)
        {
            _contiunue = contiunue;
        }

        public override async Task HandleAsync(MyRequest request, CancellationToken cancellationToken = default)
        {
            request.Visited.Add(this.GetType().Name);

            if (_contiunue)
            {
                await CallNextAsync(request, cancellationToken);
            }
        }
    }
}