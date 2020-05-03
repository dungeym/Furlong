namespace Furlong.Asynchronous
{
    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link implements <c>IAsyncChainLink&lt;TRequest&gt;</c>.
    /// <para>Supports the async/await pattern.</para>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public class AsyncChainFactory<TRequest> : IAsyncChainStartWith<TRequest>, IAsyncChainFollowedBy<TRequest>
    {
        private IAsyncChainLink<TRequest> _first;
        private IAsyncChainLink<TRequest> _previous;

        private AsyncChainFactory()
        {
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static IAsyncChainStartWith<TRequest> Initialize()
        {
            return new AsyncChainFactory<TRequest>();
        }

        IAsyncLink<TRequest> IAsyncChainFollowedBy<TRequest>.Build()
        {
            return _first;
        }

        IAsyncChainFollowedBy<TRequest> IAsyncChainFollowedBy<TRequest>.FollowedBy(IAsyncChainLink<TRequest> link)
        {
            _previous.SetNext(link);
            _previous = link;
            return this;
        }

        IAsyncChainFollowedBy<TRequest> IAsyncChainStartWith<TRequest>.StartWith(IAsyncChainLink<TRequest> link)
        {
            _first = link;
            _previous = link;
            return this;
        }
    }

    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link implements <c>IAsyncChainLink&lt;TRequest,TResponse&gt;</c>.
    /// <para>Supports the async/await pattern.</para>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public class AsyncChainFactory<TRequest, TResponse> : IAsyncChartStartWith<TRequest, TResponse>, IAsyncChainFollowedBy<TRequest, TResponse>
    {
        private IAsyncChainLink<TRequest, TResponse> _first;
        private IAsyncChainLink<TRequest, TResponse> _previous;

        private AsyncChainFactory()
        {
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static IAsyncChartStartWith<TRequest, TResponse> Initialize()
        {
            return new AsyncChainFactory<TRequest, TResponse>();
        }

        IAsyncLink<TRequest, TResponse> IAsyncChainFollowedBy<TRequest, TResponse>.Build()
        {
            return _first;
        }

        IAsyncChainFollowedBy<TRequest, TResponse> IAsyncChainFollowedBy<TRequest, TResponse>.FollowedBy(IAsyncChainLink<TRequest, TResponse> link)
        {
            _previous.SetNext(link);
            _previous = link;
            return this;
        }

        IAsyncChainFollowedBy<TRequest, TResponse> IAsyncChartStartWith<TRequest, TResponse>.StartWith(IAsyncChainLink<TRequest, TResponse> link)
        {
            _first = link;
            _previous = link;
            return this;
        }
    }
}