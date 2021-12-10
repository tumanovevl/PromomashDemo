using Promomash.Demo.Common.Models;

namespace Promomash.Demo.App.Operations.Provinces.Queries
{
    /// <summary>
    /// View model of province list
    /// </summary>
    public class ProvinceListVm
    {
        /// <summary>
        /// The instance that divides the supplied superset into subsets the size of the supplied pageSize.
        /// The instance then only containes the objects contained in the subset specified by page index.
        /// </summary>
        public PagedList<ProvinceLookupDto> PagedList { get; set; }
    }
}
