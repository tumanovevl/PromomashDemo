using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Promomash.Demo.App.Operations.Countries.Queries;

namespace Promomash.Demo.App.Controllers
{
    /// <summary>
    /// Country controller
    /// </summary>
    public class CountryController : BaseController
    {
        /// <summary>
        /// Get countries list filtered by query params
        /// </summary>
        /// <param name="query">List of the country filters</param>
        /// <returns>Returns a subset of the countries filtered by query params the size of the supplied pageSize and page index</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryListVm>> GetFilteredCountryList([FromBody] GetFilteredCountryListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
