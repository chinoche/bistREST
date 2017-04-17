using BIST.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BIST.DataAccess.Repository
{
    /// <summary>
    /// Interface IRepository
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
	public interface IRepository<TEntity> : IDisposable
    where TEntity : BaseEntity
	{
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteById(object id);

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        TEntity Update(TEntity entityToUpdate);

        /// <summary>
        /// Track entity into this repository, really in UnitOfWork.
        /// In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
		void TrackItem(TEntity item);

        /// <summary>
        /// Sets modified entity into the repository.
        /// When calling Commit() method in UnitOfWork
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
		void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns>TEntity.</returns>
		TEntity Get(int id);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
		IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
		IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <param name="totalRows">the total amount of rows</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending, out int totalRows);

        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
		IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the paged filtered.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        IEnumerable<TEntity> GetPagedFiltered<KProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, KProperty>> orderBy, Expression<Func<TEntity, bool>> filter, bool ascending);

        /// <summary>
        /// Gets the paged filtered.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="startsWith">The starts with.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        IEnumerable<TEntity> GetPagedFiltered<KProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, KProperty>> orderBy, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> startsWith, bool ascending);        
	}
}
