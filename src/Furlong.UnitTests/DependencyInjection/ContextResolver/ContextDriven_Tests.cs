using FluentAssertions;
using Furlong.Synchronous;
using Furlong.UnitTests.DependencyInjection.ContextResolver.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Furlong.UnitTests.DependencyInjection.ContextResolver
{
    public class ContextDriven_Tests
    {
        private static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton<MyDependency>()
                .AddTransient<MyLink1>()
                .AddTransient<MyLink2>()
                .AddTransient<MyLink3>()
                .AddTransient<MyLinkFactory>()
                .AddTransient<MyLinkResolver>(sp => context =>
                {
                    switch (context.ToUpperInvariant())
                    {
                        case "DOUBLE":
                            return ChainFactory<MyRequest>
                                .Initialize()
                                .StartWith(sp.GetRequiredService<MyLink1>())
                                .FollowedBy(sp.GetRequiredService<MyLink2>())
                                .FollowedBy(sp.GetRequiredService<MyLink3>())
                                .FollowedBy(sp.GetRequiredService<MyLink1>())
                                .FollowedBy(sp.GetRequiredService<MyLink2>())
                                .FollowedBy(sp.GetRequiredService<MyLink3>())
                                .Build() as IMyLink;

                        default:
                            return ChainFactory<MyRequest>
                                .Initialize()
                                .StartWith(sp.GetRequiredService<MyLink1>())
                                .FollowedBy(sp.GetRequiredService<MyLink2>())
                                .FollowedBy(sp.GetRequiredService<MyLink3>())
                                .Build() as IMyLink;
                    }
                })
                .BuildServiceProvider();
        }

        [Fact]
        public void GivenChain_ThenLinksAreTransient()
        {
            var provider = BuildServiceProvider();
            var factory = provider.GetRequiredService<MyLinkFactory>();

            var link1 = factory.Initialize("SINGLE");
            var link2 = factory.Initialize("SINGLE");

            var links = new List<IMyLink>();
            links.AddRange(ChainInspector.UnfoldChain(link1));
            links.AddRange(ChainInspector.UnfoldChain(link2));

            var uniqueLinks = links.Select(x => x.GetHashCode());

            uniqueLinks.Count().Should().Be(links.Count);
        }

        [Fact]
        public void GivenChain_WhenDefault_ThenSequenceIsCorrect()
        {
            var provider = BuildServiceProvider();
            var factory = provider.GetRequiredService<MyLinkFactory>();
            var link = factory.Initialize("SINGLE");

            var links = ChainInspector.UnfoldChain(link);

            links.Should()
                .NotBeEmpty()
                .And.HaveCount(3);

            links.ElementAt(0).Should().BeOfType(typeof(MyLink1));
            links.ElementAt(1).Should().BeOfType(typeof(MyLink2));
            links.ElementAt(2).Should().BeOfType(typeof(MyLink3));
        }

        [Fact]
        public void GivenChain_WhenDefault_ThenWorksAsExpected()
        {
            var provider = BuildServiceProvider();
            var factory = provider.GetRequiredService<MyLinkFactory>();
            var link = factory.Initialize("SINGLE");

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

        [Fact]
        public void GivenChain_WhenDouble_ThenSequenceIsCorrect()
        {
            var provider = BuildServiceProvider();
            var factory = provider.GetRequiredService<MyLinkFactory>();
            var link = factory.Initialize("DOUBLE");

            var links = ChainInspector.UnfoldChain(link);

            links.Should()
                .NotBeEmpty()
                .And.HaveCount(6);

            links.ElementAt(0).Should().BeOfType(typeof(MyLink1));
            links.ElementAt(1).Should().BeOfType(typeof(MyLink2));
            links.ElementAt(2).Should().BeOfType(typeof(MyLink3));

            links.ElementAt(3).Should().BeOfType(typeof(MyLink1));
            links.ElementAt(4).Should().BeOfType(typeof(MyLink2));
            links.ElementAt(5).Should().BeOfType(typeof(MyLink3));
        }

        [Fact]
        public void GivenChain_WhenDouble_ThenWorksAsExpected()
        {
            var provider = BuildServiceProvider();
            var factory = provider.GetRequiredService<MyLinkFactory>();
            var link = factory.Initialize("DOUBLE");

            var request = new MyRequest();

            link.Handle(request);

            request.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(6)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name, typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name });

            var dependency = provider.GetRequiredService<MyDependency>();
            dependency.Visited.Should()
               .NotBeEmpty()
               .And.HaveCount(6)
               .And.ContainInOrder(new[] { typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name, typeof(MyLink1).Name, typeof(MyLink2).Name, typeof(MyLink3).Name });
        }
    }
}