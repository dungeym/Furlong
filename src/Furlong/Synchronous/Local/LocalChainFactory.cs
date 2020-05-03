using System.Collections.Generic;

namespace Furlong.Synchronous.Local
{
    /// <summary>
    /// Method signature for <c>LocalChainFactory&lt;TRequest&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <param name="request">The request info.</param>
    /// <param name="cancel">True to exit the chain.</param>
    public delegate void Handle<in TRequest>(TRequest request, out bool cancel);

    /// <summary>
    /// Method signature for <c>LocalChainFactory&lt;TRequest,TResponse&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    /// <param name="request">The request info.</param>
    /// <param name="cancel">True to exit the chain.</param>
    /// <returns></returns>
    public delegate TResponse Handle<in TRequest, out TResponse>(TRequest request, out bool cancel);

    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link matches the <c>Handle&lt;TRequest&gt;</c> delegate.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public class LocalChainFactory<TRequest> : ILocalLink<TRequest>, ILocalChainStartWith<TRequest>, ILocalChainFollowedBy<TRequest>
    {
        private readonly Dictionary<Handle<TRequest>, Handle<TRequest>> _handlers;

        private LocalChainFactory()
        {
            _handlers = new Dictionary<Handle<TRequest>, Handle<TRequest>>();
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static ILocalChainStartWith<TRequest> Initialize()
        {
            return new LocalChainFactory<TRequest>();
        }

        ILocalLink<TRequest> ILocalChainFollowedBy<TRequest>.Build()
        {
            return this;
        }

        ILocalChainFollowedBy<TRequest> ILocalChainFollowedBy<TRequest>.FollowedBy(Handle<TRequest> handler)
        {
            _handlers.Add(handler, handler);
            return this;
        }

        void ILocalLink<TRequest>.Handle(TRequest request)
        {
            using (var en = _handlers.Values.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    en.Current(request, out var cancel);

                    if (cancel)
                    {
                        break;
                    }
                }
            }
        }

        ILocalChainFollowedBy<TRequest> ILocalChainStartWith<TRequest>.StartWith(Handle<TRequest> handler)
        {
            _handlers?.Add(handler, handler);
            return this;
        }
    }

    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link matches the <c>Handle&lt;TRequest,TResponse&gt;</c> delegate.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public class LocalChainFactory<TRequest, TResponse> : ILocalChain<TRequest, TResponse>, ILocalChartStartWith<TRequest, TResponse>, ILocalChainFollowedBy<TRequest, TResponse>
    {
        private readonly Dictionary<Handle<TRequest, TResponse>, Handle<TRequest, TResponse>> _handlers;

        private LocalChainFactory()
        {
            _handlers = new Dictionary<Handle<TRequest, TResponse>, Handle<TRequest, TResponse>>();
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static ILocalChartStartWith<TRequest, TResponse> Initialize()
        {
            return new LocalChainFactory<TRequest, TResponse>();
        }

        ILocalChain<TRequest, TResponse> ILocalChainFollowedBy<TRequest, TResponse>.Build()
        {
            return this;
        }

        ILocalChainFollowedBy<TRequest, TResponse> ILocalChainFollowedBy<TRequest, TResponse>.FollowedBy(Handle<TRequest, TResponse> handler)
        {
            _handlers.Add(handler, handler);
            return this;
        }

        TResponse ILocalChain<TRequest, TResponse>.Handle(TRequest request)
        {
            using (var en = _handlers.Values.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    var response = en.Current(request, out var cancel);
                    if (cancel)
                    {
                        return response;
                    }
                }
            }

            return default;
        }

        ILocalChainFollowedBy<TRequest, TResponse> ILocalChartStartWith<TRequest, TResponse>.StartWith(Handle<TRequest, TResponse> handler)
        {
            _handlers?.Add(handler, handler);
            return this;
        }
    }
}