namespace Furlong.Asynchronous
{
    public interface IAsyncChainFollowedBy<TRequest>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        IAsyncLink<TRequest> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IAsyncChainFollowedBy<TRequest> FollowedBy(IAsyncChainLink<TRequest> link);
    }

    public interface IAsyncChainFollowedBy<TRequest, TResponse>
    {
        /// <summary>
        /// Complete the chain, return the first link.
        /// </summary>
        /// <returns></returns>
        IAsyncLink<TRequest, TResponse> Build();

        /// <summary>
        /// Set the next link, and subsequent links, in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IAsyncChainFollowedBy<TRequest, TResponse> FollowedBy(IAsyncChainLink<TRequest, TResponse> link);
    }
}