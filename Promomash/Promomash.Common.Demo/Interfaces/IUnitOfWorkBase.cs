using System;
using System.Threading;
using System.Threading.Tasks;

namespace Promomash.Common.Interfaces
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWorkBase : IDisposable
    {
        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits all changes async
        /// </summary>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Get generic repository based on IEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepositoryGeneric<TEntity> GetRepositoryEntity<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Get generic repository
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepositoryGeneric<TEntity> GetRepositoryGeneric<TEntity>() where TEntity : class, IEntity;
    }
}
