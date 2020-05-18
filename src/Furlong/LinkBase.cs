
namespace Furlong
{
    /// <summary>
    /// Implementation of <c>IChainLink&lt;TRequest&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public abstract class LinkBase<TRequest> : IChainLink<TRequest>
    {
        private IChainLink<TRequest> _successor;

        /// <summary>
        /// Call the next link in chain (if one exists).
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        protected void CallNext(TRequest request)
        {
            _successor?.Handle(request);
        }

        /// <summary>
        /// Handle the request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        public abstract void Handle(TRequest request);

        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void IChainLink<TRequest>.SetNext(IChainLink<TRequest> link)
        {
            _successor = link;
        }
    }

    /// <summary>
    /// Implementation of <c>IChainLink&lt;TRequest,TResponse&gt;</c>
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public abstract class LinkBase<TRequest, TResponse> : IChainLink<TRequest, TResponse>
    {
        private IChainLink<TRequest, TResponse> _successor;

        /// <summary>
        /// Call the next link in chain (if one exists).
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        protected TResponse CallNext(TRequest request)
        {
            if (_successor == null)
            {
                return default;
            }

            return _successor.Handle(request);
        }

        /// <summary>
        /// Handle the request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        public abstract TResponse Handle(TRequest request);

        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void IChainLink<TRequest, TResponse>.SetNext(IChainLink<TRequest, TResponse> link)
        {
            _successor = link;
        }
    }
}