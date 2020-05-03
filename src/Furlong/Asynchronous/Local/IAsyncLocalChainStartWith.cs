namespace Furlong.Asynchronous.Local
{
    public interface IAsyncLocalChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IAsyncLocalChainFollowedBy<TRequest> StartWith(HandleAsync<TRequest> handler);
    }

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