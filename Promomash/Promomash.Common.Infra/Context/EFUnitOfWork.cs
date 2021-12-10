using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Promomash.Common.Infra.Repositories;
using Promomash.Common.Interfaces;

namespace Promomash.Common.Infra.Context
{
    /// <summary>
    /// Unit of work
    /// </summary>
    public class EFUnitOfWork : IUnitOfWorkBase
    {
        protected readonly EFContext dbContext;
        protected readonly ILogger logger;

        /// <summary>
        /// EF UOW to provide testing
        /// </summary>
        /// <param name="dbContext"></param>
        public EFUnitOfWork(EFContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException();
            }

            this.dbContext = dbContext;
        }

        public EFUnitOfWork(EFContext dbContext, ILogger logger)
        {
            if (dbContext == null || logger == null)
            {
                throw new ArgumentNullException();
            }

            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IRepositoryGeneric<TEntity> GetRepositoryEntity<TEntity>() where TEntity : class, IEntity
        {
            return new EFGenericRepository<TEntity>(dbContext);
        }

        public IRepositoryGeneric<TEntity> GetRepositoryGeneric<TEntity>() where TEntity : class, IEntity
        {
            return new EFGenericRepository<TEntity>(dbContext);
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database
        /// </summary>
        public void Commit()
        {
            var changedEntriesCopy = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                    || e.State == EntityState.Modified
                    || e.State == EntityState.Deleted)
                .ToList();

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database</returns>
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            //Multiple active operations on the same context instance are not supported.
            //Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
