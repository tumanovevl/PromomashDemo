using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Promomash.Demo.Common.Entities;
using Promomash.Demo.Common.Interfaces;

namespace Promomash.Demo.App.Operations.Users.Commands
{
    /// <summary>
    /// A Handler that processes the CreateUserCommand request and returns an id of created instance
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UOW</param>        
        public CreateUserCommandHandler(
            IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns an id of created user</returns>
        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Login = request.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CountryId = request.CountryId,
                ProvinceId = request.ProvinceId
            };

            entity = await unitOfWork.UserRepository.CreateAsync(entity);
            await unitOfWork.CommitAsync(cancellationToken);

            return entity.Id;
        }
    }
}
