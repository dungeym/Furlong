namespace Furlong
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface ILocalChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        ILocalChainFollowedBy<TRequest> StartWith(Handle<TRequest> handler);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface ILocalChartStartWith<TRequest, TResponse>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        ILocalChainFollowedBy<TRequest, TResponse> StartWith(Handle<TRequest, TResponse> handler);
    }
}