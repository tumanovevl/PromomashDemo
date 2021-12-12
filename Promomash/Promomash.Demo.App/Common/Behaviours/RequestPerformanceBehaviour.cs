using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Extensions.Logging;

using MediatR;

namespace Promomash.Demo.App.Common.Behaviours
{
    /// <summary>
    /// Track request duration
    /// </summary>
    /// <typeparam name="TRequest">Request object passed in through IMediator.Send</typeparam>
    /// <typeparam name="TResponse">Async continuation for the next action in the behavior chain</typeparam>
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch timer;
        private readonly ILogger<RequestPerformanceBehaviour<TRequest, TResponse>> logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Loger</param>
        public RequestPerformanceBehaviour(
            ILogger<RequestPerformanceBehaviour<TRequest, TResponse>> logger
            )
        {
            this.timer = new Stopwatch();
            this.logger = logger;
        }

        /// <summary>
        /// Pipeline handler
        /// </summary>
        /// <param name="request">Request object passed in through IMediator.Send</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="next">RequestHandlerDelegate</param>        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            timer.Start();

            var response = await next();

            timer.Stop();

            if (timer.ElapsedMilliseconds > 3000)
            {
                var name = typeof(TRequest).Name;

                logger.LogWarning($"Oops, PromomashDemo Long Running Request: {name} ({timer.ElapsedMilliseconds} milliseconds) {request}");
            }

            return response;
        }
    }
}
