using Microsoft.Extensions.Logging;

using Promomash.Common.Infra.Context;
using Promomash.Common.Interfaces;
using Promomash.Demo.Common.Entities;
using Promomash.Demo.Common.Interfaces;

namespace Promomash.Demo.Infra.Context
{
    /// <summary>
    /// PromomashDemo's Unit of Work
    /// </summary>
    public class UnitOfWork : EFUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(PromomashDemoContext dbContext)
            : base(dbContext)
        {
            UserRepository = GetRepositoryEntity<User>();
            CountryRepository = GetRepositoryEntity<Country>();
            ProvinceRepository = GetRepositoryEntity<Province>();
        }

        public UnitOfWork(PromomashDemoContext dbContext, ILogger<UnitOfWork> logger)
            : base(dbContext, logger)
        {
            UserRepository = GetRepositoryEntity<User>();
            CountryRepository = GetRepositoryEntity<Country>();
            ProvinceRepository = GetRepositoryEntity<Province>();
        }

        /// <summary>
        /// User repository
        /// </summary>
        public IRepositoryGeneric<User> UserRepository { get; }

        /// <summary>
        /// Country repository
        /// </summary>
        public IRepositoryGeneric<Country> CountryRepository { get; }

        /// <summary>
        /// Province repository
        /// </summary>
        public IRepositoryGeneric<Province> ProvinceRepository { get; }
    }
}
