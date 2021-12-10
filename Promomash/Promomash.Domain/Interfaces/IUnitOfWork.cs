using Promomash.Common.Interfaces;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.Common.Interfaces
{
    /// <summary>
    /// Interface of PromomashDemo's Unit of Work
    /// </summary>
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        /// <summary>
        /// User repository
        /// </summary>
        IRepositoryGeneric<User> UserRepository { get; }

        /// <summary>
        /// Country repository
        /// </summary>
        IRepositoryGeneric<Country> CountryRepository { get; }

        /// <summary>
        /// Province repository
        /// </summary>
        IRepositoryGeneric<Province> ProvinceRepository { get; }
    }
}
