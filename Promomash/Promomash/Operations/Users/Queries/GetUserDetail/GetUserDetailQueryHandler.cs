using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using MediatR;

using Promomash.Demo.Common.Interfaces;
using Promomash.Demo.App.Common.Exceptions;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.App.Operations.Users.Queries
{
    /// <summary>
    /// A Handler that processes the GetUserDetailQuery request and returns UserVm
    /// </summary>
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UOW</param>
        /// <param name="mapper">AutoMapper instance</param>
        public GetUserDetailQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns a UserVm of requested user</returns>
        public async Task<UserVm> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await mapper
                .ProjectTo<UserVm>(unitOfWork.UserRepository.GetAll(x => x.Id == request.Id))
                .FirstOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return vm;
        }
    }
}
