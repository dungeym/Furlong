using System.Threading;
using System.Threading.Tasks;

namespace Furlong
{
    /// <summary>
    /// Represents a link in a Chain (of Responsibility).
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    public interface IAsyncLink<in TRequest>
    {
        /// <summary>
        /// Handle a request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task HandleAsync(TRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Handle a request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        Task HandleAsync(TRequest request);
    }

    /// <summary>
    /// Represents a link in a Chain (of Responsibility).
    /// </summary>
    /// <typeparam name="TRequest">The type of the object that contains the data to be handled.</typeparam>
    /// <typeparam name="TResponse">The type of the object returned.</typeparam>
    public interface IAsyncLink<in TRequest, TResponse>
    {
        /// <summary>
        /// Handle a request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Handle a request.
        /// </summary>
        /// <param name="request">An object that contains the data to be handled.</param>
        /// <returns></returns>
        Task<TResponse> HandleAsync(TRequest request);
    }
}