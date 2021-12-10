using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Promomash.Common.Infra.Context;
using Promomash.Common.Interfaces;

namespace Promomash.Common.Infra.Repositories
{
    /// <summary>
    /// Generic repository implementation
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public class EFGenericRepository<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class
    {
        protected readonly EFContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public EFGenericRepository(EFContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = this._dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Add new entity to DB set
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Added entity</returns>
        public virtual TEntity Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return _dbSet.Add(entity).Entity;
        }

        /// <summary>
        /// Add new entity to DB set async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Added entity</returns>
        public virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            return Task.FromResult(Create(entity));
        }

        /// <summary>
        /// Attach entity to DB context and mark it as modified
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>        
        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        /// <summary>
        /// Attach entity to DB context and mark it as modified async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>  
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);

            return Task.FromResult(entity);
        }

        /// <summary>
        /// Mark entity as deleted
        /// </summary>
        /// <param name="id">ID of the entity</param>
        public virtual void Delete(long id)
        {
            var res = _dbSet.Find(id);

            if (res == null)
            {
                throw new KeyNotFoundException();
            }

            Delete(res);
        }

        /// <summary>
        /// Mark entity as deleted async
        /// </summary>
        /// <param name="id">ID of the entity</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        public virtual async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            Delete(entity);
        }

        /// <summary>
        /// Mark entity as deleted
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            AttachIfNot(entity);
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Mark entity as deleted async
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Delete all entities matching condition
        /// </summary>
        /// <param name="condition">Condition</param>
        public virtual void DeleteAll(
            Expression<Func<TEntity, bool>> condition = null
            )
        {
            var itemsToDelete = condition != null
                ? FindAll(condition)
                : GetAll();

            if (itemsToDelete.Any())
            {
                foreach (var item in itemsToDelete)
                {
                    Delete(item);
                }
            }
        }

        /// <summary>
        /// Find one entity matching condition
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entity be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entity be tracked</param>        
        /// <returns>Matching entity</returns>
        public virtual TEntity FindOne(
            Expression<Func<TEntity, bool>> filter,
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            var query = PrepareQuery(withDeleted, trackChanges);

            return query.FirstOrDefault(filter);
        }

        /// <summary>
        /// Find one entity matching condition async
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="withDeleted">Should soft-deleted entity be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entity be tracked</param>        
        /// <returns>Matching entity</returns>
        public virtual async Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default,
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            var query = PrepareQuery(withDeleted, trackChanges);

            return await query.FirstOrDefaultAsync(filter, cancellationToken);
        }

        /// <summary>
        /// Find all entities matching condition
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>Matching entities</returns>
        public virtual IEnumerable<TEntity> FindAll(
            Expression<Func<TEntity, bool>> filter,
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            var query = PrepareQuery(withDeleted, trackChanges);

            return query.Where(filter).ToList();
        }

        /// <summary>
        /// Find all entities matching condition async
        /// </summary>
        /// <param name="filter">Condition</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>Matching entities</returns>
        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default,
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            var query = PrepareQuery(withDeleted, trackChanges);

            return await query.Where(filter).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>All entities</returns>
        public virtual IQueryable<TEntity> GetAll(
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            return PrepareQuery(withDeleted, trackChanges);
        }

        /// <summary>
        /// Get all entities
        /// </summary>        
        /// <param name="filter">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        public virtual IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter,
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            var query = PrepareQuery(withDeleted, trackChanges);

            return query.Where(filter);
        }

        /// <summary>
        /// Get count of entities matching condition
        /// </summary>
        /// <param name="condition">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <returns>Number of matching entities</returns>
        public virtual long GetCount(
            Expression<Func<TEntity, bool>> condition = null,
            bool withDeleted = false)
        {
            var query = PrepareQuery(withDeleted, trackChanges: false);

            return condition != null
                ? query.Count(condition)
                : query.Count();
        }

        /// <summary>
        /// Get count of entities matching condition
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <param name="condition">Condition</param>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <returns>Number of matching entities</returns>
        public virtual async Task<long> GetCountAsync(
            CancellationToken cancellationToken = default,
            Expression<Func<TEntity, bool>> condition = null,
            bool withDeleted = false
            )
        {
            var query = PrepareQuery(withDeleted, trackChanges: false);

            return condition != null
                ? await query.CountAsync(condition, cancellationToken)
                : await query.CountAsync(cancellationToken);
        }

        /// <summary>
        /// Detach entity from DB set
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Detach(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Attach entity to DB set if it is not tracked
        /// </summary>
        /// <param name="entity">Entity</param>
        private void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            _dbSet.Attach(entity);
        }

        /// <summary>
        /// Creates IQueryable object with custom settings
        /// </summary>
        /// <param name="withDeleted">Should soft-deleted entities be included in result</param>
        /// <param name="trackChanges">Should changes made to matching entities be tracked</param>        
        /// <returns>Returns IQueryable object with supplied settings</returns>
        private IQueryable<TEntity> PrepareQuery(
            bool withDeleted = false,
            bool trackChanges = false
            )
        {
            var query = trackChanges
                ? _dbSet.AsTracking()
                : _dbSet.AsNoTracking();

            if (withDeleted)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }
    }
}
