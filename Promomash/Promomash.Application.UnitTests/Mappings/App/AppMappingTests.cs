using AutoMapper;

using Shouldly;

using Xunit;

using Promomash.Demo.App.Operations.Countries.Queries;
using Promomash.Demo.App.Operations.Provinces.Queries;
using Promomash.Demo.App.Operations.Users.Queries;
using Promomash.Demo.Common.Entities;

namespace Promomash.Application.UnitTests.Mappings.App
{
    /// <summary>
    /// App mapping tests
    /// </summary>
    public class AppMappingTests : IClassFixture<AppMappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fixture">App mapping tests fixture</param>
        public AppMappingTests(AppMappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        /// <summary>
        ///  Dry run all configured type maps and throw AutoMapper.AutoMapperConfigurationException for each problem
        /// </summary>
        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        /// <summary>
        /// User to UserVm mapping test
        /// </summary>
        [Fact]
        public void ShouldMapUserToUserVm()
        {
            var entity = new User();

            var result = _mapper.Map<UserVm>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<UserVm>();
        }

        /// <summary>
        /// Province to ProvinceLookupDto mapping test
        /// </summary>
        [Fact]
        public void ShouldMapProvinceToProvinceLookupDto()
        {
            var entity = new Province();

            var result = _mapper.Map<ProvinceLookupDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProvinceLookupDto>();
        }

        /// <summary>
        /// Country to CountryLookupDto mapping test
        /// </summary>
        [Fact]
        public void ShouldMapCountryToCountryLookupDto()
        {
            var entity = new Country();

            var result = _mapper.Map<CountryLookupDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<CountryLookupDto>();
        }
    }
}