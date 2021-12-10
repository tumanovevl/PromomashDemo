using MediatR;

using Newtonsoft.Json;

namespace Promomash.Demo.App.Operations.Users.Commands
{
    /// <summary>
    /// User creation command
    /// </summary>
    public class CreateUserCommand : IRequest<long>
    {
        /// <summary>
        /// Login
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Country id
        /// </summary>
        [JsonProperty("countryId")]
        public long CountryId { get; set; }

        /// <summary>
        /// Province id
        /// </summary>
        [JsonProperty("provinceId")]
        public long ProvinceId{ get; set; }
    }
}
