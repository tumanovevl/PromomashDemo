using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using MediatR.Pipeline;

namespace Promomash.Demo.App.Common.Behaviours
{
    /// <summary>
    /// Logs all requests before any handlers are called, for demo purposes
    /// </summary>
    /// <typeparam name="TRequest">Request object passed in through IMediator.Send</typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<RequestLogger<TRequest>> logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="currentUser">Current user</param>
        public RequestLogger(
            ILogger<RequestLogger<TRequest>> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Process the request
        /// </summary>
        /// <param name="request">Request object passed in through IMediator.Send</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            logger.LogInformation($"PromomashDemo request: {name} {request}");

            return Task.CompletedTask;
        }
    }
}
