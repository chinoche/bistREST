using Glocation.DataAccess.UnitOfWork;
using Glocation.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Glocation.DataAccess.Repository;

namespace Glocation.DataAccess.Repository
{
    /// <summary>
    /// Class GenericRepository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The database set
        /// </summary>
        internal DbSet<TEntity> dbSet;
        /// <summary>
        /// The unit of work
        /// </summary>
        protected readonly IUnitOfWork unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// </summary>
        public GenericRepository()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="UoWContext">The uo w context.</param>
        /// <exception cref="System.ArgumentNullException">unitOfWork</exception>
        public GenericRepository(IUnitOfWork UoWContext)
        {
            if (UoWContext == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");
            this.unitOfWork = UoWContext;
            this.dbSet = UoWContext.CreateSet<TEntity>();
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Gets the specified filter as an IQueryable.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        public virtual IQueryable<TEntity> GetQ(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                if (orderBy != null)
                {
                    return orderBy(query);
                }
                else
                {
                    return query;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TEntity.</returns>
        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void DeleteById(object id, object id2)
        {
            TEntity entityToDelete = dbSet.Find(id, id2);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete != default(TEntity))
            {
                dbSet.Attach(entityToDelete);
                dbSet.Remove(entityToDelete);
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public virtual TEntity Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            unitOfWork.SetModified(entityToUpdate);
            return entityToUpdate;
        }

        /// <summary>
        /// Track entity into this repository, really in UnitOfWork.In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
        public void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
                unitOfWork.Attach<TEntity>(item);
        }

        /// <summary>
        /// Sets modified entity into the repository. When calling Commit() method in UnitOfWork these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        public void Merge(TEntity persisted, TEntity current)
        {
            unitOfWork.ApplyCurrentValues(persisted, current);
        }

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns>TEntity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual TEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Get(filter: null, orderBy: null, includeProperties: string.Empty);
        }

        /// <summary>
        /// Gets the paged.
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>
        /// IEnumerable&lt;TEntity&gt;.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                             .Skip(pageSize * pageIndex)
                             .Take(pageSize);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                             .Skip(pageSize * pageIndex)
                             .Take(pageSize);
            }
        }

        /// <summary>
        /// Gets the paged.
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <param name="totalRows">The total rows.</param>
        /// <returns>
        /// IEnumerable&lt;TEntity&gt;.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending, out int totalRows)
        {
            var set = GetSet();
            totalRows = set.Count();
            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                             .Skip(pageSize * pageIndex)
                             .Take(pageSize);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                             .Skip(pageSize * pageIndex)
                             .Take(pageSize);
            }
        }

        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// Gets the paged filtered.
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>
        /// IEnumerable&lt;TEntity&gt;.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetPagedFiltered<KProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, KProperty>> orderBy, Expression<Func<TEntity, bool>> filter, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.Where(filter)
                            .OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
            else
            {
                return set.Where(filter)
                            .OrderByDescending(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
        }

        /// <summary>
        /// Gets the paged filtered.
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="startsWith">The starts with.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>
        /// IEnumerable&lt;TEntity&gt;.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetPagedFiltered<KProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, KProperty>> orderBy, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> startsWith, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.Where(filter)
                            .Where(startsWith)
                            .OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
            else
            {
                return set.Where(filter)
                            .Where(startsWith)
                            .OrderByDescending(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
        }


        /// <summary>
        /// Gets the paged filtered.
        /// </summary>
        /// <typeparam name="KProperty">The type of the k property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <param name="totaRows">The tota rows.</param>
        /// <returns>
        /// IEnumerable&lt;TEntity&gt;.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<TEntity> GetPagedFiltered<KProperty>(int pageIndex, int pageSize,
                                                       Expression<Func<TEntity, KProperty>> orderBy,
                                                       Expression<Func<TEntity, bool>> filter, bool ascending, out int totaRows)
        {
            var set = GetSet().AsNoTracking().Where(filter);
            pageIndex = pageIndex - 1;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            totaRows = set.Count();

            int totalPages = totaRows / pageSize;

            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            if (pageSize < 0)
            {
                pageIndex = 0;
                pageSize = totaRows;
            }

            if (ascending)
            {
                return set.OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
            else
            {
                return set.OrderByDescending(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
        }


        /// <summary>
        /// Returns the a list of paged items filtered by the expression 
        /// </summary>
        /// <param name="pageIndex">Number of the page to show</param>
        /// <param name="pageSize">Number of items in a page</param>
        /// <param name="orderBy">Order by expression</param>
        /// <param name="filter">Filter expression</param>
        /// <param name="startsWith">Additional filter if you need to filter whit a starst with</param>
        /// <param name="ascending">True if the data soul be order in ascending mode</param>
        /// <param name="totalRows">the total amount of rows</param>
        /// <returns>Filtered list with the results</returns>		
        public virtual IEnumerable<TEntity> GetPagedFiltered<KProperty>(int pageIndex, int pageSize,
                                                        Expression<Func<TEntity, KProperty>> orderBy,
                                                        Expression<Func<TEntity, bool>> filter,
                                                        Expression<Func<TEntity, bool>> startsWith, bool ascending, out int totalRows)
        {
            var set = GetSet().Where(filter).Where(startsWith);
            //TODO: FIND A BETTER WAY TO GET THE TOTAL ROWS
            totalRows = set.Count();
            if (ascending)
            {
                return set.OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
            else
            {
                return set.OrderByDescending(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
        }

        /// <summary>
        /// Returns the a list of paged items filtered by the expression
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="pageIndex">Number of the page to show</param>
        /// <param name="pageSize">Number of items in a page</param>
        /// <param name="orderBy">Order by expression</param>
        /// <param name="filter">Filter expression</param>
        /// <param name="ascending">True if the data soul be order in ascending mode</param>
        /// <param name="totaRows">The tota rows.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// Filtered list with the results
        /// </returns>
        public virtual IQueryable<TEntity> GetPagedFilteredQ<KProperty>(int pageIndex, int pageSize,
                                                        System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderBy,
                                                        System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, bool ascending, out int totaRows, string includeProperties = "")
        {
            var set = GetSet().AsNoTracking().Where(filter);
            pageIndex = pageIndex - 1;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            totaRows = set.Count();

            int totalPages = totaRows / pageSize;

            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            if (pageSize < 0)
            {
                pageIndex = 0;
                pageSize = totaRows;
            }

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                set = set.Include(includeProperty);
            }

            if (ascending)
            {
                return set.OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize).AsNoTracking();
            }
            else
            {
                return set.OrderByDescending(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize);
            }
        }

        public virtual List<TEntity> GetPagedFilteredDynamicOrder<KProperty>(int pageIndex, int pageSize,
                                                      string orderBy, System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, bool ascending,
                                                       out int totaRows, string includeProperties = "")
        {

            var set = GetSet().AsNoTracking().Where(filter);

            pageIndex = pageIndex - 1;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            totaRows = set.Count();

            int totalPages = totaRows / pageSize;

            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            if (pageSize < 0)
            {
                pageIndex = 0;
                pageSize = totaRows;
            }

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                set = set.Include(includeProperty);
            }

            if (ascending)
            {
                return set.OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize).ToList();
            }
            else
            {
                return set.OrderBy(orderBy + " descending")
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize).ToList();
            }
        }

        public virtual List<TEntity> GetPagedFilteredDynamic<KProperty>(int pageIndex, int pageSize,
                                                       string orderBy, string filter, bool ascending,
                                                        string includeProperties = "")
        {

            var set = GetSet().AsNoTracking();

            if (!string.IsNullOrEmpty(filter))
            {
                set = set.Where(filter);
            }
            pageIndex = pageIndex - 1;
            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            var totaRows = set.Count();

            int totalPages = totaRows / pageSize;

            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            if (pageSize < 0)
            {
                pageIndex = 0;
                pageSize = totaRows;
            }

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                set = set.Include(includeProperty);
            }

            if (ascending)
            {
                return set.OrderBy(orderBy)
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize).ToList();
            }
            else
            {
                return set.OrderBy(orderBy + " descending")
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize).ToList();
            }
        }



        public virtual int GetPagedFilteredDynamicCount<KProperty>(string filter)
        {
            var set = GetSet().AsNoTracking();

            if (!string.IsNullOrEmpty(filter))
            {
                set = set.Where(filter);
            }

            return set.Count();
        }

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the set.
        /// </summary>
        /// <returns>IDbSet&lt;TEntity&gt;.</returns>
        IDbSet<TEntity> GetSet()
        {
            return unitOfWork.CreateSet<TEntity>();
        }
    }
}
