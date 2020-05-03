using FluentAssertions;
using Furlong.Asynchronous.Local;
using Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequestReponse.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequestReponse
{
    public class AsyncLocalChainFactory_RequestResponse_Tests
    {
        private readonly bool _checkpoint3 = false;
        private bool _checkpoint1;
        private bool _checkpoint2;

        private async Task<MyResponse> TryHandleAsync1(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(TryHandleAsync1));

            if (_checkpoint1)
            {
                source.Cancel();
                return await Task.FromResult(new MyResponse(nameof(TryHandleAsync1)));
            }

            return default;
        }

        private async Task<MyResponse> TryHandleAsync2(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(TryHandleAsync2));

            if (_checkpoint2)
            {
                source.Cancel();
                return await Task.FromResult(new MyResponse(nameof(TryHandleAsync2)));
            }

            return default;
        }

        private async Task<MyResponse> TryHandleAsync3(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(TryHandleAsync3));

            if (_checkpoint3)
            {
                source.Cancel();
                return await Task.FromResult(new MyResponse(nameof(TryHandleAsync3)));
            }

            return default;
        }

        [Fact]
        public async Task GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var chain = AsyncLocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandleAsync1)
                .FollowedBy(TryHandleAsync2)
                .FollowedBy(TryHandleAsync3)
                .Build();

            var request = new MyRequest();
            await chain.HandleAsync(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder(new[] { nameof(TryHandleAsync1), nameof(TryHandleAsync2), nameof(TryHandleAsync3) });
        }

        [Fact]
        public async Task GivenChain_WhenOnlyFirstOneShouldBeCalled_ThenOnlyFirstOneIsCalled()
        {
            _checkpoint1 = true;

            var chain = AsyncLocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandleAsync1)
                .FollowedBy(TryHandleAsync2)
                .FollowedBy(TryHandleAsync3)
                .Build();

            var request = new MyRequest();
            await chain.HandleAsync(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainInOrder(new[] { nameof(TryHandleAsync1) });
        }

        [Fact]
        public async Task GivenChain_WhenOnlyFirstTwoShouldBeCalled_ThenOnlyFirstTwoAreCalled()
        {
            _checkpoint2 = true;

            var chain = AsyncLocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandleAsync1)
                .FollowedBy(TryHandleAsync2)
                .FollowedBy(TryHandleAsync3)
                .Build();

            var request = new MyRequest();
            await chain.HandleAsync(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainInOrder(new[] { nameof(TryHandleAsync1), nameof(TryHandleAsync2) });
        }
    }
}