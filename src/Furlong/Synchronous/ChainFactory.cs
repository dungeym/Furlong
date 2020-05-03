namespace Furlong.Synchronous
{
    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link implements <c>IChainLink&lt;TRequest&gt;</c>.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public class ChainFactory<TRequest> : IChainStartWith<TRequest>, IChainFollowedBy<TRequest>
    {
        private IChainLink<TRequest> _first;
        private IChainLink<TRequest> _previous;

        private ChainFactory()
        {
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static IChainStartWith<TRequest> Initialize()
        {
            return new ChainFactory<TRequest>();
        }

        ILink<TRequest> IChainFollowedBy<TRequest>.Build()
        {
            return _first;
        }

        IChainFollowedBy<TRequest> IChainFollowedBy<TRequest>.FollowedBy(IChainLink<TRequest> link)
        {
            _previous.SetNext(link);
            _previous = link;
            return this;
        }

        IChainFollowedBy<TRequest> IChainStartWith<TRequest>.StartWith(IChainLink<TRequest> link)
        {
            _first = link;
            _previous = link;
            return this;
        }
    }

    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link implements <c>IChainLink&lt;TRequest,TResponse&gt;</c>.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public class ChainFactory<TRequest, TResponse> : IChartStartWith<TRequest, TResponse>, IChainFollowedBy<TRequest, TResponse>
    {
        private IChainLink<TRequest, TResponse> _first;
        private IChainLink<TRequest, TResponse> _previous;

        private ChainFactory()
        {
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static IChartStartWith<TRequest, TResponse> Initialize()
        {
            return new ChainFactory<TRequest, TResponse>();
        }

        ILink<TRequest, TResponse> IChainFollowedBy<TRequest, TResponse>.Build()
        {
            return _first;
        }

        IChainFollowedBy<TRequest, TResponse> IChainFollowedBy<TRequest, TResponse>.FollowedBy(IChainLink<TRequest, TResponse> link)
        {
            _previous.SetNext(link);
            _previous = link;
            return this;
        }

        IChainFollowedBy<TRequest, TResponse> IChartStartWith<TRequest, TResponse>.StartWith(IChainLink<TRequest, TResponse> link)
        {
            _first = link;
            _previous = link;
            return this;
        }
    }
}