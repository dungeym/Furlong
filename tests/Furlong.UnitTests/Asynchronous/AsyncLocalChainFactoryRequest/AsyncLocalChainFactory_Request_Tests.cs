using FluentAssertions;
using Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequest.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequest
{
    public class AsyncLocalChainFactory_Request_Tests
    {
        private bool _checkpoint1;
        private bool _checkpoint2;

        private async Task HandleAsync1(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(HandleAsync1));

            if (_checkpoint1)
            {
                source.Cancel();
            }

            await Task.CompletedTask;
        }

        private async Task HandleAsync2(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(HandleAsync2));

            if (_checkpoint2)
            {
                source.Cancel();
            }

            await Task.CompletedTask;
        }

        private async Task HandleAsync3(MyRequest request, CancellationTokenSource source)
        {
            request.Visited.Add(nameof(HandleAsync3));

            await Task.CompletedTask;
        }

        [Fact]
        public async Task GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var chain = AsyncLocalChainFactory<MyRequest>
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

            var chain = AsyncLocalChainFactory<MyRequest>
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

            var chain = AsyncLocalChainFactory<MyRequest>
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