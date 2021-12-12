using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Promomash.Demo.Common.Interfaces;
using Promomash.Infra.Extensions;

namespace Promomash.Demo.App.Operations.Provinces.Queries
{
    /// <summary>
    /// A Handler that processes the GetFilteredProvinceListQuery request and returns ProvinceListVm
    /// </summary>
    public class GetFilteredCountryListQueryHandler : IRequestHandler<GetFilteredProvinceListQuery, ProvinceListVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UOW</param>
        /// <param name="mapper">AutoMapper instance</param>
        public GetFilteredCountryListQueryHandler(
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
        /// <returns>Returns a subset of the projects filtered by query params the size of the supplied pageSize and page index</returns>
        public async Task<ProvinceListVm> Handle(GetFilteredProvinceListQuery request, CancellationToken cancellationToken)
        {
            var provinceFilter = ProvincesFilterBuilder.Create(request);

            var result = await mapper
                .ProjectTo<ProvinceLookupDto>(unitOfWork.ProvinceRepository.GetAll(provinceFilter).OrderBy(x => x.Title))
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            var vm = new ProvinceListVm
            {
                PagedList = result
            };

            return vm;
        }
    }
}
