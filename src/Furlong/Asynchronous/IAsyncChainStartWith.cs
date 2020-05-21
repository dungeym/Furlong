namespace Furlong
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IAsyncChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IAsyncChainFollowedBy<TRequest> StartWith(IAsyncChainLink<TRequest> link);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IAsyncChartStartWith<TRequest, TResponse>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IAsyncChainFollowedBy<TRequest, TResponse> StartWith(IAsyncChainLink<TRequest, TResponse> link);
    }
}