using System;

using Xunit;

using AutoMapper;

using Promomash.Application.Tests.Common;
using Promomash.Demo.Common.Interfaces;
using Promomash.Demo.Infra.Context;
using Promomash.Demo.App.Common.Mappings;

namespace Promomash.Application.UnitTests.Common
{
    /// <summary>
    /// Fixture class to perform test on queries
    /// </summary>    
    public class QueryTestFixture : MocksBase, IDisposable
    {
        /// <summary>
        /// AutoMapper instance
        /// </summary>
        public IMapper Mapper { get; private set; }

        /// <summary>
        /// PromomashDemo UOW
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }

        private readonly PromomashDemoContext _context;

        /// <summary>
        /// Constructor that initialize a DB connext, AutoMapper profiles and UOW
        /// </summary>
        public QueryTestFixture()
        {
            _context = PromomashDemoContextFactory.Create(base.DateTimeMock.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

            UnitOfWork = UnitOfWorkFactory.Create(_context);
        }

        /// <summary>
        /// Destructor that release a DB context
        /// </summary>
        public void Dispose()
        {
            PromomashDemoContextFactory.Destroy(_context);
        }
    }

    /// <summary>
    /// Query fixture collection
    /// </summary>
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}