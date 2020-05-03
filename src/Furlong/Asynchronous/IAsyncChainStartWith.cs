namespace Furlong.Asynchronous
{
    public interface IAsyncChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        IAsyncChainFollowedBy<TRequest> StartWith(IAsyncChainLink<TRequest> link);
    }

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