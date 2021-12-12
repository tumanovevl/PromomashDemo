using AutoMapper;

using Newtonsoft.Json;

using Promomash.Demo.App.Common.Mappings;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.App.Operations.Provinces.Queries
{
    /// <summary>
    /// Province lookup dto view model
    /// </summary>
    public class ProvinceLookupDto : IMapFrom<Province>
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Profile that maps Province to ProvinceLookupDto
        /// </summary>
        /// <param name="profile">AutoMapper profile</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Province, ProvinceLookupDto>();
        }
    }
}
