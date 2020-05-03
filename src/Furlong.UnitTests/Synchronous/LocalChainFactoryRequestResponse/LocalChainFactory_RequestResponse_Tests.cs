﻿using FluentAssertions;
using Furlong.Synchronous.Local;
using Furlong.UnitTests.Synchronous.LocalChainFactoryRequestResponse.Domain;
using Xunit;

namespace Furlong.UnitTests.Synchronous.LocalChainFactoryRequestResponse
{
    public class LocalChainFactory_RequestResponse_Tests
    {
        private bool _checkpoint1;
        private bool _checkpoint2;
        private bool _checkpoint3;

        private MyResponse TryHandle1(MyRequest request, out bool cancel)
        {
            cancel = _checkpoint1;

            request.Visited.Add(nameof(TryHandle1));

            if (_checkpoint1)
            {
                return new MyResponse(nameof(TryHandle1));
            }

            return default;
        }

        private MyResponse TryHandle2(MyRequest request, out bool cancel)
        {
            cancel = _checkpoint2;

            request.Visited.Add(nameof(TryHandle2));

            if (_checkpoint2)
            {
                return new MyResponse(nameof(TryHandle2));
            }

            return default;
        }

        private MyResponse TryHandle3(MyRequest request, out bool cancel)
        {
            cancel = _checkpoint3;

            request.Visited.Add(nameof(TryHandle3));

            if (_checkpoint3)
            {
                return new MyResponse(nameof(TryHandle3));
            }

            return default;
        }

        [Fact]
        public void GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder(new[] { nameof(TryHandle1), nameof(TryHandle2), nameof(TryHandle3) });
        }

        [Fact]
        public void GivenChain_WhenNoLocalShouldReturn_ThenResponseIsAsExpected()
        {
            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            var response = chain.Handle(request);

            response.Should().BeNull();
        }

        [Fact]
        public void GivenChain_WhenOnlyFirstOneShouldBeCalled_ThenOnlyFirstOneIsCalled()
        {
            _checkpoint1 = true;

            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainInOrder(new[] { nameof(TryHandle1) });
        }

        [Fact]
        public void GivenChain_WhenOnlyFirstTwoShouldBeCalled_ThenOnlyFirstTwoAreCalled()
        {
            _checkpoint2 = true;

            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            chain.Handle(request);

            request.Visited.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainInOrder(new[] { nameof(TryHandle1), nameof(TryHandle2) });
        }

        [Fact]
        public void GivenChain_WhenTheFirstLocalShouldReturn_ThenResponseIsAsExpected()
        {
            _checkpoint1 = true;

            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            var response = chain.Handle(request);

            response.HandledBy.Should().Be(nameof(TryHandle1));
        }

        [Fact]
        public void GivenChain_WhenTheSecondLocalShouldReturn_ThenResponseIsAsExpected()
        {
            _checkpoint2 = true;

            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            var response = chain.Handle(request);

            response.HandledBy.Should().Be(nameof(TryHandle2));
        }

        [Fact]
        public void GivenChain_WhenTheThirdLocalShouldReturn_ThenResponseIsAsExpected()
        {
            _checkpoint3 = true;

            var chain = LocalChainFactory<MyRequest, MyResponse>
                .Initialize()
                .StartWith(TryHandle1)
                .FollowedBy(TryHandle2)
                .FollowedBy(TryHandle3)
                .Build();

            var request = new MyRequest();
            var response = chain.Handle(request);

            response.HandledBy.Should().Be(nameof(TryHandle3));
        }
    }
}