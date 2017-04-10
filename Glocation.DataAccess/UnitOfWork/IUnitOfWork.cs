using System;
using System.Data.Entity;

namespace Glocation.DataAccess.UnitOfWork
{
    /// <summary>
    /// Interface IUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable, ISql
    {
        /// <summary>
        /// Returns a IDbSet instance for access to entities of the given type in the context,
        /// the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>DbSet&lt;TEntity&gt;.</returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// Attaches the specified item.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="item">The item.</param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Set object as modified
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="item">The entity item to set as modifed</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Apply current values in <paramref name="original" />
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        /// <remarks>If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown</remarks>
        void Commit();

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        /// <remarks>If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins</remarks>
        void CommitAndRefreshChanges();

        /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern
        /// </summary>
        void RollbackChanges();
    }
}
