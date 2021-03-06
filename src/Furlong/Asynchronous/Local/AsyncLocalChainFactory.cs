﻿using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Furlong
{
    /// <summary>
    /// Method signature for <c>AsyncLocalChainFactory&lt;TRequest&gt;</c>
    /// <para>Supports the async/await pattern.</para>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <param name="request">An object that contains the data to be handled.</param>
    /// <param name="cancellationTokenSource">A cancellation source that can be used to signal to cancellation token that it should be canceled.</param>
    /// <returns></returns>
    public delegate Task HandleAsync<in TRequest>(TRequest request, CancellationTokenSource cancellationTokenSource);

    /// <summary>
    /// Method signature for <c>AsyncLocalChainFactory&lt;TRequest, TResponse&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    /// <param name="request">An object that contains the data to be handled.</param>
    /// <param name="cancellationTokenSource">A cancellation source that can be used to signal to cancellation token that it should be canceled.</param>
    /// <returns></returns>
    public delegate Task<TResponse> HandleAsync<in TRequest, TResponse>(TRequest request, CancellationTokenSource cancellationTokenSource);

    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link matches the <c>HandleAsync&lt;TRequest&gt;</c> delegate.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public class AsyncLocalChainFactory<TRequest> : IAsyncLocalLink<TRequest>, IAsyncLocalChainStartWith<TRequest>, IAsyncLocalChainFollowedBy<TRequest>
    {
        private readonly ConcurrentQueue<HandleAsync<TRequest>> _handlers;

        private AsyncLocalChainFactory()
        {
            _handlers = new ConcurrentQueue<HandleAsync<TRequest>>();
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static IAsyncLocalChainStartWith<TRequest> Initialize()
        {
            return new AsyncLocalChainFactory<TRequest>();
        }

        IAsyncLocalLink<TRequest> IAsyncLocalChainFollowedBy<TRequest>.Build()
        {
            return this;
        }

        IAsyncLocalChainFollowedBy<TRequest> IAsyncLocalChainFollowedBy<TRequest>.FollowedBy(HandleAsync<TRequest> handler)
        {
            _handlers.Enqueue(handler);
            return this;
        }

        async Task IAsyncLocalLink<TRequest>.HandleAsync(TRequest request, CancellationTokenSource cancellationTokenSource)
        {
            var source = cancellationTokenSource ?? new CancellationTokenSource();

            while (_handlers.TryDequeue(out var handler))
            {
                await handler(request, source).ConfigureAwait(false);
                if (source.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        IAsyncLocalChainFollowedBy<TRequest> IAsyncLocalChainStartWith<TRequest>.StartWith(HandleAsync<TRequest> handler)
        {
            _handlers?.Enqueue(handler);
            return this;
        }
    }

    /// <summary>
    /// Factory to construct a Chain (of Responsibility) where each link matches the <c>HandleAsync&lt;TRequest,TResponse&gt;</c> delegate.
    /// <para>Supports the async/await pattern.</para>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public class AsyncLocalChainFactory<TRequest, TResponse> : IAsyncLocalChain<TRequest, TResponse>, IAsyncLocalChartStartWith<TRequest, TResponse>, IAsyncLocalChainFollowedBy<TRequest, TResponse>
    {
        private readonly ConcurrentQueue<HandleAsync<TRequest, TResponse>> _handlers;

        private AsyncLocalChainFactory()
        {
            _handlers = new ConcurrentQueue<HandleAsync<TRequest, TResponse>>();
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <returns></returns>
        public static IAsyncLocalChartStartWith<TRequest, TResponse> Initialize()
        {
            return new AsyncLocalChainFactory<TRequest, TResponse>();
        }

        IAsyncLocalChain<TRequest, TResponse> IAsyncLocalChainFollowedBy<TRequest, TResponse>.Build()
        {
            return this;
        }

        IAsyncLocalChainFollowedBy<TRequest, TResponse> IAsyncLocalChainFollowedBy<TRequest, TResponse>.FollowedBy(HandleAsync<TRequest, TResponse> handler)
        {
            _handlers.Enqueue(handler);
            return this;
        }

        async Task<TResponse> IAsyncLocalChain<TRequest, TResponse>.HandleAsync(TRequest request, CancellationTokenSource cancellationTokenSource)
        {
            var source = cancellationTokenSource ?? new CancellationTokenSource();

            while (_handlers.TryDequeue(out var handler))
            {
                var response = await handler(request, source).ConfigureAwait(false);
                if (source.IsCancellationRequested)
                {
                    return response;
                }
            }

            return default;
        }

        IAsyncLocalChainFollowedBy<TRequest, TResponse> IAsyncLocalChartStartWith<TRequest, TResponse>.StartWith(HandleAsync<TRequest, TResponse> handler)
        {
            _handlers?.Enqueue(handler);
            return this;
        }
    }
}