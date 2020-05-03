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

        private async Task<MyResponse> HandleAsync1(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(HandleAsync1));

            if (_checkpoint1)
            {
                source.Cancel();
                return await Task.FromResult(new MyResponse(nameof(HandleAsync1)));
            }

            return default;
        }

        private async Task<MyResponse> HandleAsync2(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(HandleAsync2));

            if (_checkpoint2)
            {
                source.Cancel();
                return await Task.FromResult(new MyResponse(nameof(HandleAsync2)));
            }

            return default;
        }

        private async Task<MyResponse> HandleAsync3(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(HandleAsync3));

            if (_checkpoint3)
            {
                source.Cancel();
                return await Task.FromResult(new MyResponse(nameof(HandleAsync3)));
            }

            return default;
        }

        [Fact]
        public async Task GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var chain = AsyncLocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(HandleAsync1)
                .FollowedBy(HandleAsync2)
                .FollowedBy(HandleAsync3)
                .Build();

            var request = new MyRequest();
            await chain.HandleAsync(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder(new[] { nameof(HandleAsync1), nameof(HandleAsync2), nameof(HandleAsync3) });
        }

        [Fact]
        public async Task GivenChain_WhenOnlyFirstOneShouldBeCalled_ThenOnlyFirstOneIsCalled()
        {
            _checkpoint1 = true;

            var chain = AsyncLocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(HandleAsync1)
                .FollowedBy(HandleAsync2)
                .FollowedBy(HandleAsync3)
                .Build();

            var request = new MyRequest();
            await chain.HandleAsync(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainInOrder(new[] { nameof(HandleAsync1) });
        }

        [Fact]
        public async Task GivenChain_WhenOnlyFirstTwoShouldBeCalled_ThenOnlyFirstTwoAreCalled()
        {
            _checkpoint2 = true;

            var chain = AsyncLocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(HandleAsync1)
                .FollowedBy(HandleAsync2)
                .FollowedBy(HandleAsync3)
                .Build();

            var request = new MyRequest();
            await chain.HandleAsync(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainInOrder(new[] { nameof(HandleAsync1), nameof(HandleAsync2) });
        }
    }
}