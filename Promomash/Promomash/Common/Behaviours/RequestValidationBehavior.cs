using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using FluentValidation;
using ValidationException = Promomash.Demo.App.Common.Exceptions.ValidationException;

using MediatR;

namespace Promomash.Demo.App.Common.Behaviours
{
    /// <summary>
    /// Validate request, for demo purposes
    /// </summary>
    /// <typeparam name="TRequest">Request object passed in through IMediator.Send</typeparam>
    /// <typeparam name="TResponse">Async continuation for the next action in the behavior chain</typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validators">List of request validators</param>
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        /// <summary>
        /// Pipeline handler
        /// </summary>
        /// <param name="request">Request object passed in through IMediator.Send</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="next">RequestHandlerDelegate</param>  
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = this.validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}
