using FluentAssertions;
using Furlong.Asynchronous;
using Furlong.UnitTests.Asynchronous.AsyncChainFactoryRequestResponse.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Furlong.UnitTests.Asynchronous.AsyncChainFactoryRequestResponse
{
    public class AsyncChainFactory_RequestResponse_Tests
    {
        [Fact]
        public void GivenChain_ThenSequenceIsCorrect()
        {
            IAsyncChainLink<MyRequest, MyResponse> myLink1 = new MyLink1(false);
            IAsyncChainLink<MyRequest, MyResponse> myLink2 = new MyLink2(false);
            IAsyncChainLink<MyRequest, MyResponse> myLink3 = new MyLink3(false);

            var link = AsyncChainFactory<MyRequest, MyResponse>
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
        public async Task GivenChain_WhenAllShouldBeCalled_ThenAllAreCalled()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(true))
                  .FollowedBy(new MyLink3(true))
                  .Build();

            var request = new MyRequest();

            await link.HandleAsync(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(3)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name });
        }

        [Fact]
        public async Task GivenChain_WhenNoneShouldReturn_ThenResponseIsNull()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(true))
                  .FollowedBy(new MyLink3(true))
                  .Build();

            var request = new MyRequest();

            var response = await link.HandleAsync(request);

            response.Should().BeNull();
        }

        [Fact]
        public async Task GivenChain_WhenOnlyFirstOneShouldBeCalled_ThenOnlyFirstOneIsCalled()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(false))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            await link.HandleAsync(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(1)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name });
        }

        [Fact]
        public async Task GivenChain_WhenOnlyFirstTwoShouldBeCalled_ThenOnlyFirstTwoAreCalled()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            await link.HandleAsync(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(2)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name });
        }

        [Fact]
        public async Task GivenChain_WhenTheFirstLinkShouldReturn_ThenResponseIsAsExpected()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(false))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            var response = await link.HandleAsync(request);

            response.HandledBy.Should().BeOfType(typeof(MyLink1));
        }

        [Fact]
        public async Task GivenChain_WhenTheSecondLinkShouldReturn_ThenResponseIsAsExpected()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(false))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            var response = await link.HandleAsync(request);

            response.HandledBy.Should().BeOfType(typeof(MyLink2));
        }

        [Fact]
        public async Task GivenChain_WhenTheThirdLinkShouldReturn_ThenResponseIsAsExpected()
        {
            var link = AsyncChainFactory<MyRequest, MyResponse>
                  .Initialize()
                  .StartWith(new MyLink1(true))
                  .FollowedBy(new MyLink2(true))
                  .FollowedBy(new MyLink3(false))
                  .Build();

            var request = new MyRequest();

            var response = await link.HandleAsync(request);

            response.HandledBy.Should().BeOfType(typeof(MyLink3));
        }
    }
}