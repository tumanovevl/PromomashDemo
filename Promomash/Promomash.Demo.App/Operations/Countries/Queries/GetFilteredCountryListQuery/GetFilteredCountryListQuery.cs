using MediatR;

namespace Promomash.Demo.App.Operations.Countries.Queries
{
    /// <summary>
    /// Filtered project list query
    /// </summary>
    public class GetFilteredCountryListQuery : IRequest<CountryListVm>
    {
        /// <summary>
        /// The part of the country title to search or null to ignore
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The one-based index of the subset of objects to be contained by this instance.
        /// Default value is 1.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The maximum size of any individual subset
        /// Default value is 20.
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
