using Promomash.Demo.Common.Models;

namespace Promomash.Demo.App.Operations.Countries.Queries
{
    /// <summary>
    /// View model of country list
    /// </summary>
    public class CountryListVm
    {
        /// <summary>
        /// The instance that divides the supplied superset into subsets the size of the supplied pageSize.
        /// The instance then only containes the objects contained in the subset specified by page index.
        /// </summary>
        public PagedList<CountryLookupDto> PagedList { get; set; }
    }
}
