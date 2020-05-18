namespace Furlong
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IAsyncLocalChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IAsyncLocalChainFollowedBy<TRequest> StartWith(HandleAsync<TRequest> handler);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IAsyncLocalChartStartWith<TRequest, TResponse>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IAsyncLocalChainFollowedBy<TRequest, TResponse> StartWith(HandleAsync<TRequest, TResponse> handler);
    }
}