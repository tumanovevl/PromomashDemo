using MediatR;

using Newtonsoft.Json;

namespace Promomash.Demo.App.Operations.Users.Queries
{
    /// <summary>
    /// User detail query
    /// </summary>
    public class GetUserDetailQuery : IRequest<UserVm>
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
