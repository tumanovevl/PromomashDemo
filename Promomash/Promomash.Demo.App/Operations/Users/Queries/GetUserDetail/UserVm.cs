using Newtonsoft.Json;

using AutoMapper;

using Promomash.Demo.App.Common.Mappings;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.App.Operations.Users.Queries
{
    /// <summary>
    /// User view model
    /// </summary>
    public class UserVm : IMapFrom<User>
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Country title
        /// </summary>
        [JsonProperty("countryTitle")]
        public string CountryTitle { get; set; }

        /// <summary>
        /// Province title
        /// </summary>
        [JsonProperty("provinceTitle")]
        public string ProvinceTitle { get; set; }

        /// <summary>
        /// Profile that maps User to UserVm
        /// </summary>
        /// <param name="profile">AutoMapper profile</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVm>();
        }
    }
}
