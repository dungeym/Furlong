namespace Furlong.Synchronous
{
    /// <summary>
    /// Represents a link in a Chain (of Responsibility).
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public interface IChainLink<TRequest> : ILink<TRequest>
    {
        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void SetNext(IChainLink<TRequest> link);
    }

    /// <summary>
    /// Represents a link in a Chain (of Responsibility).
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public interface IChainLink<TRequest, TResponse> : ILink<TRequest, TResponse>
    {
        /// <summary>
        /// Set the next link in the chain.
        /// </summary>
        /// <param name="link"></param>
        void SetNext(IChainLink<TRequest, TResponse> link);
    }
}