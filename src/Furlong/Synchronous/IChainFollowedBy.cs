namespace Furlong.Synchronous
{
    public interface IChainFollowedBy<TRequest>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        ILink<TRequest> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IChainFollowedBy<TRequest> FollowedBy(IChainLink<TRequest> link);
    }

    public interface IChainFollowedBy<TRequest, TResponse>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        ILink<TRequest, TResponse> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IChainFollowedBy<TRequest, TResponse> FollowedBy(IChainLink<TRequest, TResponse> link);
    }
}