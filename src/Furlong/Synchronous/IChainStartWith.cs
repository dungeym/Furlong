namespace Furlong
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IChainFollowedBy<TRequest> StartWith(IChainLink<TRequest> link);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IChartStartWith<TRequest, TResponse>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IChainFollowedBy<TRequest, TResponse> StartWith(IChainLink<TRequest, TResponse> link);
    }
}