namespace Furlong
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IAsyncLocalChainFollowedBy<TRequest>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        IAsyncLocalLink<TRequest> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IAsyncLocalChainFollowedBy<TRequest> FollowedBy(HandleAsync<TRequest> handler);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IAsyncLocalChainFollowedBy<TRequest, TResponse>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        IAsyncLocalChain<TRequest, TResponse> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IAsyncLocalChainFollowedBy<TRequest, TResponse> FollowedBy(HandleAsync<TRequest, TResponse> handler);
    }
}