using Glocation.Dominio.Entidades;
using Glocation.DataAccess.UnitOfWork;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;


namespace Glocation.DataAccess.Repository
{
    /// <summary>
    /// Class .
    /// </summary>
    public class GlocationDbContext : DbContext, IUnitOfWork
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GlocationDbContext"/> class.
        /// </summary>
        public GlocationDbContext()
            : base("name=GlocationDbContext_ConnectionString")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Obtiene las opciones del usuario basado en su rol
        /// </summary>
        /// <value>
        /// Coleccion con las opciones por Rol
        /// </value>
        public virtual DbSet<Globers> Globers { get; set; }

        

        #endregion


        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.</remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        #region UnitOfWork Implementation
        /// <summary>
        /// Returns a IDbSet instance for access to entities of the given type in the context,
        /// the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>DbSet&lt;TEntity&gt;.</returns>
        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// Attaches the specified item.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="item">The item.</param>
        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            //attach and set as unchanged
            Entry(item).State = EntityState.Unchanged;
        }

        /// <summary>
        /// Set object as modified
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="item">The entity item to set as modifed</param>
        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Apply current values in <paramref name="original" />
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        /// <remarks>If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown</remarks>
        public void Commit()
        {
            base.SaveChanges();
        }


        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        /// <remarks>If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins</remarks>
        public void CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);
        }

        /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern
        /// </summary>
        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">Dialect Query
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        /// </example></param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>Enumerable results</returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            int? timeout = Database.Connection.ConnectionTimeout;
            Database.CommandTimeout = timeout.Value;
            return Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, int commandTimeout, params object[] parameters)
        {
            int? timeout = Database.Connection.ConnectionTimeout;
            Database.CommandTimeout = timeout.Value;
            return Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        /// </example></param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            int? timeout = Database.Connection.ConnectionTimeout;
            Database.CommandTimeout = timeout.Value;
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commnadTimeout">The commnad timeout.</param>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteCommand(int commnadTimeout, string sqlCommand, params object[] parameters)
        {
            Database.CommandTimeout = commnadTimeout;
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
        #endregion
    }
}
