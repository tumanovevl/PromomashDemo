using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Promomash.Common.Infra.EntityMappers;
using Promomash.Common.Interfaces;

namespace Promomash.Common.Infra.Context
{
    /// <summary>
    ///  Infrastructure context
    /// </summary>
    public class EFContext : DbContext
    {
        protected readonly ILogger _logger;
        protected readonly IDateTime _dateTime;
        
        public EFContext(DbContextOptions options, IDateTime dateTime)
            : base(options)
        {
            this._dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        public EFContext(DbContextOptions options, ILogger logger, IDateTime dateTime) : base(options)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyMappingConfigurationFromAssembly(modelBuilder, Assembly.GetExecutingAssembly());
        }

        protected void ApplyMappingConfigurationFromAssembly(ModelBuilder modelBuilder, Assembly assembly)
        {
            var entityMapperTypes = assembly.GetTypes()
                .Where(type =>
                    !string.IsNullOrEmpty(type.Namespace)
                    && type.BaseType?.IsGenericType == true
                    && type.BaseType.GetGenericTypeDefinition() == typeof(AbstractEntityMapper<>));

            // Add all fluent API configurations
            foreach (var entityMapperType in entityMapperTypes)
            {
                dynamic instance = Activator.CreateInstance(entityMapperType);
                modelBuilder.ApplyConfiguration(instance);

                var modelType = entityMapperType.BaseType.GetGenericArguments().First();
                if (typeof(ISoftDeletable).IsAssignableFrom(modelType))
                {
                    var property = typeof(ISoftDeletable).GetProperty("IsDeleted");
                    var parameter = Expression.Parameter(modelType, "p");
                    var filter = Expression.Lambda(Expression.Not(Expression.Property(parameter, property)), parameter);

                    modelBuilder.Entity(modelType).HasQueryFilter(filter);
                }
            }
        }

        /// <summary>
        /// Process added/modified/deleted entities of custom type before they are saved
        /// </summary>
        private void ProcessCustomChanges()
        {
            ChangeTracker.DetectChanges();

            var softlyDeletedItems = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted && x.Entity is ISoftDeletable)
                .ToList();

            foreach (var item in softlyDeletedItems)
            {
                item.State = EntityState.Modified;
                ((ISoftDeletable)item.Entity).IsDeleted = true;
            }

            var addedAuditableItems = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added && x.Entity is IAuditable)
                .ToList();

            foreach (var item in addedAuditableItems)
            {
                ((IAuditable)item.Entity).CreatedAt = _dateTime.UtcNow;
                ((IAuditable)item.Entity).EditedAt = _dateTime.UtcNow;
            }

            var updatedAuditableItems = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified && x.Entity is IAuditable)
                .ToList();

            foreach (var item in updatedAuditableItems)
            {
                ((IAuditable)item.Entity).EditedAt = _dateTime.UtcNow;
            }
        }

        /// <summary>
        /// Applies IAuditable and ISoftDeletable logic and saves all changes made in this context to the underlying database
        /// </summary>
        public override int SaveChanges()
        {
            ProcessCustomChanges();

            return base.SaveChanges();
        }

        /// <summary>
        /// Applies IAuditable and ISoftDeletable logic and saves all changes made in this context to the underlying database async
        /// </summary>
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ProcessCustomChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
