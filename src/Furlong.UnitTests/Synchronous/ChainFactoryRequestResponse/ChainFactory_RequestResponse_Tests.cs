using FluentAssertions;
using Furlong.UnitTests.Synchronous.ChainFactoryRequestResponse.Domain;
using Xunit;

namespace Furlong.UnitTests.Synchronous.ChainFactoryRequestResponse
{
    public class ChainFactory_RequestResponse_Tests
    {
        [Fact]
        public void GivenChain_ThenSequenceIsCorrect()
        {
            IChainLink<MyRequest, MyResponse> myLink1 = new MyLink1(false);
            IChainLink<MyRequest, MyResponse> myLink2 = new MyLink2(false);
            IChainLink<MyRequest, MyResponse> myLink3 = new MyLink3(false);

            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(myLink1)
                  .FollowedBy(myLink2)
                  .FollowedBy(myLink3)
                  .Build();

            var links = ChainInspector.UnfoldChain(link);

            links.Should()
               .NotBeEmpty()
               .And.HaveCount(3)
               .And.ContainInOrder(new[] { myLink1, myLink2, myLink3 });
        }

        [Fact]
        public void GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(true))
                  .FollowedBy(new MyLink3(true))
                  .Build();

            var request = new MyRequest();

            link.Handle(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(3)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name });
        }

        [Fact]
        public void GivenChain_WhenNoneShouldReturn_ThenResponseIsNull()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(true))
                  .FollowedBy(new MyLink3(true))
                  .Build();

            var request = new MyRequest();

            var response = link.Handle(request);

            response.Should().BeNull();
        }

        [Fact]
        public void GivenChain_WhenOnlyFirstOneShouldBeCalled_ThenOnlyFirstOneIsCalled()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(false))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            link.Handle(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(1)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name });
        }

        [Fact]
        public void GivenChain_WhenOnlyFirstTwoShouldBeCalled_ThenOnlyFirstTwoAreCalled()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            link.Handle(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(2)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name });
        }

        [Fact]
        public void GivenChain_WhenTheFirstLinkShouldReturn_ThenResponseIsAsExpected()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(false))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            var response = link.Handle(request);

            response.HandledBy.Should().BeOfType(typeof(MyLink1));
        }

        [Fact]
        public void GivenChain_WhenTheSecondLinkShouldReturn_ThenResponseIsAsExpected()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            var response = link.Handle(request);

            response.HandledBy.Should().BeOfType(typeof(MyLink2));
        }

        [Fact]
        public void GivenChain_WhenTheThirdLinkShouldReturn_ThenResponseIsAsExpected()
        {
            var link = ChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(true))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            var response = link.Handle(request);

            response.HandledBy.Should().BeOfType(typeof(MyLink3));
        }
    }
}