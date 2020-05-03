using FluentAssertions;
using Furlong.Synchronous;
using Furlong.UnitTests.DependencyInjection.CustomInterface.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Furlong.UnitTests.DependencyInjection.CustomInterface
{
    public class CustomInterface_Tests
    {
        private static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton<MyDependency>()
                .AddTransient<MyLink1>()
                .AddTransient<MyLink2>()
                .AddTransient<MyLink3>()
                .AddTransient<IMyLink>(sp =>
                {
                    return ChainFactory<MyRequest>
                        .Initialize()
                        .StartWith(sp.GetRequiredService<MyLink1>())
                        .FollowedBy(sp.GetRequiredService<MyLink2>())
                        .FollowedBy(sp.GetRequiredService<MyLink3>())
                        .Build() as IMyLink;
                })
                .BuildServiceProvider();
        }

        [Fact]
        public void GivenChain_ThenLinksAreTransient()
        {
            var provider = BuildServiceProvider();

            var link1 = provider.GetRequiredService<IMyLink>();
            var link2 = provider.GetRequiredService<IMyLink>();

            var links = new List<IMyLink>();
            links.AddRange(ChainInspector.UnfoldChain(link1));
            links.AddRange(ChainInspector.UnfoldChain(link2));

            var uniqueLinks = links.Select(x => x.GetHashCode());

            uniqueLinks.Count().Should().Be(links.Count);
        }

        [Fact]
        public void GivenChain_ThenSequenceIsCorrect()
        {
            var provider = BuildServiceProvider();
            var link = provider.GetRequiredService<IMyLink>();

            var links = ChainInspector.UnfoldChain(link);

            links.Should()
                .NotBeEmpty()
                .And.HaveCount(3);

            links.ElementAt(0).Should().BeOfType(typeof(MyLink1));
            links.ElementAt(1).Should().BeOfType(typeof(MyLink2));
            links.ElementAt(2).Should().BeOfType(typeof(MyLink3));
        }

        [Fact]
        public void GivenChain_ThenWorksAsExpected()
        {
            var provider = BuildServiceProvider();
            var link = provider.GetRequiredService<IMyLink>();

            var request = new MyRequest();

            link.Handle(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(3)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name });

            var dependency = provider.GetRequiredService<MyDependency>();
            dependency.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(3)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name });
        }
    }
}