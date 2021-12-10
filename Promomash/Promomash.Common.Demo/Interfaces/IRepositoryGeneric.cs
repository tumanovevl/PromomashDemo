using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Promomash.Common.Interfaces
{
    /// <summary>
    /// Interface of generic repository
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface IRepositoryGeneric<TEntity>
    {
        /// <summary>
        /// Add new entity to DB set
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Added entity</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Add new entity to DB set async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Added entity</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Attach entity to DB context and mark it as modified
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>        
        TEntity Update(TEntity entity);

        /// <summary>
        /// Attach entity to DB context and mark it as modified async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>  
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Mark entity as deleted
        /// </summary>
        /// <param name="id">ID of the entity</param>
        void Delete(long id);

        /// <summary>
        /// Mark entity as deleted async
        /// </summary>
        /// <param name="id">ID of the entity</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Mark entity as deleted
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Mark entity as deleted async
        /// </summary>
        /// <param name="entity">Entity</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete all entities matching condition
        /// </summary>
        /// <param name="condition">Condition</param>
        void DeleteAll(
            Expression<Func<TEntity, bool>> condition = null
        );

        /// <summary>
        /// Find one entity matching condition
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entity be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entity be tracked</param>        
        /// <returns>Matching entity</returns>
        TEntity FindOne(
            Expression<Func<TEntity, bool>> filter,
            bool withDeleted = false,
            bool trackChanges = false
        );

        /// <summary>
        /// Find one entity matching condition async
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="withDeleted">Should soft-deleted entity be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entity be tracked</param>        
        /// <returns>Matching entity</returns>
        Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default,
            bool withDeleted = false,
            bool trackChanges = false
        );

        /// <summary>
        /// Find all entities matching condition
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>Matching entities</returns>
        IEnumerable<TEntity> FindAll(
            Expression<Func<TEntity, bool>> filter,
            bool withDeleted = false,
            bool trackChanges = false
        );

        /// <summary>
        /// Find all entities matching condition async
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>Matching entities</returns>
        Task<IEnumerable<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default,
            bool withDeleted = false,
            bool trackChanges = false
        );

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>All entities</returns>
        IQueryable<TEntity> GetAll(
            bool withDeleted = false,
            bool trackChanges = false
        );

        /// <summary>
        /// Get all entities
        /// </summary>        
        /// <param name="filter">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter,
            bool withDeleted = false,
            bool trackChanges = false
        );

        /// <summary>
        /// Get count of entities matching condition
        /// </summary>
        /// <param name="condition">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <returns>Number of matching entities</returns>
        long GetCount(
            Expression<Func<TEntity, bool>> condition = null,
            bool withDeleted = false
        );

        /// <summary>
        /// Get count of entities matching condition
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="condition">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <returns>Number of matching entities</returns>
        Task<long> GetCountAsync(
            CancellationToken cancellationToken = default,
            Expression<Func<TEntity, bool>> condition = null,
            bool withDeleted = false
        );

        /// <summary>
        /// Detach entity from DB set
        /// </summary>
        /// <param name="entity">Entity</param>
        void Detach(TEntity entity);
    }
}
