namespace Furlong.Asynchronous
{
    /// <summary>
    /// Represents a link in a Chain (of Responsibility).
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public interface IAsyncChainLink<TRequest> : IAsyncLink<TRequest>
    {
        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void SetNext(IAsyncChainLink<TRequest> link);
    }

    /// <summary>
    /// Represents a link in a Chain (of Responsibility).
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public interface IAsyncChainLink<TRequest, TResponse> : IAsyncLink<TRequest, TResponse>
    {
        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void SetNext(IAsyncChainLink<TRequest, TResponse> link);
    }
}