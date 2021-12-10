using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Promomash.Demo.App.Operations.Users.Queries;
using Promomash.Demo.App.Operations.Users.Commands;

namespace Promomash.Demo.App.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id">User id</param>        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserVm>> Get(long id)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery { Id = id }));
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="command">User creation command</param>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<long>> Create([FromBody] CreateUserCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
}
