using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Promomash.Demo.Common.Interfaces;
using Promomash.Infra.Extensions;

namespace Promomash.Demo.App.Operations.Countries.Queries
{
    /// <summary>
    /// A Handler that processes the GetFilteredCountryListQuery request and returns ProjectListVm
    /// </summary>
    public class GetFilteredCountryListQueryHandler : IRequestHandler<GetFilteredCountryListQuery, CountryListVm>
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
        public async Task<CountryListVm> Handle(GetFilteredCountryListQuery request, CancellationToken cancellationToken)
        {
            var countryFilter = CountriesFilterBuilder.Create(request);

            var result = await mapper
                .ProjectTo<CountryLookupDto>(unitOfWork.CountryRepository.GetAll(countryFilter).OrderBy(x => x.Title))
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            var vm = new CountryListVm
            {
                PagedList = result
            };

            return vm;
        }
    }
}
