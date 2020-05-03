namespace Furlong.Synchronous
{
    public interface IChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IChainFollowedBy<TRequest> StartWith(IChainLink<TRequest> link);
    }

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