namespace Furlong.Synchronous.Local
{
    public interface ILocalChainFollowedBy<TRequest>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        ILocalLink<TRequest> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        ILocalChainFollowedBy<TRequest> FollowedBy(Handle<TRequest> handler);
    }

    public interface ILocalChainFollowedBy<TRequest, TResponse>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        ILocalChain<TRequest, TResponse> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        ILocalChainFollowedBy<TRequest, TResponse> FollowedBy(Handle<TRequest, TResponse> handler);
    }
}