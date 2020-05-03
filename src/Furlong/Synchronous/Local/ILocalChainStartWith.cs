namespace Furlong.Synchronous.Local
{
    public interface ILocalChainStartWith<TRequest>
    {
        /// <summary>
        /// Set the first link in the chain.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        ILocalChainFollowedBy<TRequest> StartWith(Handle<TRequest> handler);
    }

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