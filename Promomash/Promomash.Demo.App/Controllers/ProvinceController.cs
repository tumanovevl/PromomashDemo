using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Promomash.Demo.App.Operations.Provinces.Queries;

namespace Promomash.Demo.App.Controllers
{
    /// <summary>
    /// Province controller
    /// </summary>
    public class ProvinceController : BaseController
    {
        /// <summary>
        /// Get provinces list filtered by query params
        /// </summary>
        /// <param name="query">List of the province filters</param>
        /// <returns>Returns a subset of the provinces filtered by query params the size of the supplied pageSize and page index</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProvinceListVm>> GetFilteredProvinceList([FromBody] GetFilteredProvinceListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
