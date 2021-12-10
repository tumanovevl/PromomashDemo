using AutoMapper;

using Newtonsoft.Json;

using Promomash.Demo.App.Common.Mappings;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.App.Operations.Countries.Queries
{
    /// <summary>
    /// Country lookup dto view model
    /// </summary>
    public class CountryLookupDto : IMapFrom<Country>
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
        /// Profile that maps Country to CountryLookupDto
        /// </summary>
        /// <param name="profile">AutoMapper profile</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryLookupDto>();
        }
    }
}
