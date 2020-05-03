using Furlong.Asynchronous;
using System.Threading.Tasks;

namespace Furlong
{
    /// <summary>
    /// Implementation of <c>IAsyncChainLink&lt;TRequest&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">An object that contains the data to be handled.</typeparam>
    public abstract class AsyncLinkBase<TRequest> : IAsyncChainLink<TRequest>
    {
        private IAsyncChainLink<TRequest> _successor;

        /// <summary>
        /// Call the next link in chain (if one exists).
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        protected async Task CallNextAsync(TRequest request)
        {
            if (_successor != null)
            {
                await _successor.HandleAsync(request);
            }
        }

        /// <summary>
        /// Handle the request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        public abstract Task HandleAsync(TRequest request);

        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void IAsyncChainLink<TRequest>.SetNext(IAsyncChainLink<TRequest> link)
        {
            _successor = link;
        }
    }

    /// <summary>
    /// Implementation of <c>IAsyncChainLink&lt;TRequest,TResponse&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public abstract class AsyncLinkBase<TRequest, TResponse> : IAsyncChainLink<TRequest, TResponse>
    {
        private IAsyncChainLink<TRequest, TResponse> _successor;

        /// <summary>
        /// Call the next link in chain (if one exists).
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        protected async Task<TResponse> CallNextAsync(TRequest request)
        {
            if (_successor != null)
            {
                return await _successor.HandleAsync(request);
            }

            return default;
        }

        /// <summary>
        /// Handle the request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        public abstract Task<TResponse> HandleAsync(TRequest request);

        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void IAsyncChainLink<TRequest, TResponse>.SetNext(IAsyncChainLink<TRequest, TResponse> link)
        {
            _successor = link;
        }
    }
}