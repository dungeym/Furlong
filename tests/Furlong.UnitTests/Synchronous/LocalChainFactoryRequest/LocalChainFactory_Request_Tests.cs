using FluentAssertions;
using Furlong.UnitTests.Synchronous.LocalChainFactoryRequest.Domain;
using Xunit;

namespace Furlong.UnitTests.Synchronous.LocalChainFactoryRequest
{
    public class LocalChainFactory_Request_Tests
    {
        private bool _checkpoint1;
        private bool _checkpoint2;

        private void Handle1(MyRequest request, out bool cancel)
        {
            cancel = _checkpoint1;
            request.Visited.Add(nameof(Handle1));
        }

        private void Handle2(MyRequest request, out bool cancel)
        {
            cancel = _checkpoint2;
            request.Visited.Add(nameof(Handle2));
        }

        private void Handle3(MyRequest request, out bool cancel)
        {
            cancel = false;
            request.Visited.Add(nameof(Handle3));
        }

        [Fact]
        public void GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var chain = LocalChainFactory<MyRequest>
                .Initialize()
                .StartWith(Handle1)
                .FollowedBy(Handle2)
                .FollowedBy(Handle3)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder(new[] { nameof(Handle1), nameof(Handle2), nameof(Handle3) });
        }

        [Fact]
        public void GivenChain_WhenContainsDuplicateLinks_ThenAllAreCalled()
        {
            var chain = LocalChainFactory<MyRequest>
                .Initialize()
                .StartWith(Handle1)
                .FollowedBy(Handle2)
                .FollowedBy(Handle1)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder(new[] { nameof(Handle1), nameof(Handle2), nameof(Handle1) });
        }

        [Fact]
        public void GivenChain_WhenOnlyFirstOneShouldBeCalled_ThenOnlyFirstOneIsCalled()
        {
            _checkpoint1 = true;

            var chain = LocalChainFactory<MyRequest>
                .Initialize()
                .StartWith(Handle1)
                .FollowedBy(Handle2)
                .FollowedBy(Handle3)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainInOrder(new[] { nameof(Handle1) });
        }

        [Fact]
        public void GivenChain_WhenOnlyFirstTwoShouldBeCalled_ThenOnlyFirstTwoAreCalled()
        {
            _checkpoint2 = true;

            var chain = LocalChainFactory<MyRequest>
                .Initialize()
                .StartWith(Handle1)
                .FollowedBy(Handle2)
                .FollowedBy(Handle3)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainInOrder(new[] { nameof(Handle1), nameof(Handle2) });
        }
    }
}