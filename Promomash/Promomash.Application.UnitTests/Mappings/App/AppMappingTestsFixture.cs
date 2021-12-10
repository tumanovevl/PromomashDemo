using AutoMapper;

using Promomash.Demo.App.Common.Mappings;

namespace Promomash.Application.UnitTests.Mappings.App
{
    /// <summary>
    /// Fixture for testing application layer mappings
    /// </summary>
    public class AppMappingTestsFixture
    {
        /// <summary>
        /// AutoMapper configuration provider
        /// </summary>
        public IConfigurationProvider ConfigurationProvider { get; }

        /// <summary>
        /// Automapper instance
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AppMappingTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }
    }
}