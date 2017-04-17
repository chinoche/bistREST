using BIST.DataAccess.Repository;
using BIST.Dominio.Entidades;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using BIST.Common.DTO;
using BIST.DataAccess.Repository;

namespace BIST.DataAccess.UnitOfWork
{
    /// <summary>
    /// Class UnitOfWork.
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        /// <summary>
        /// The context
        /// </summary>
        internal BISTDbContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            Context = new BISTDbContext();
            Context.Configuration.ProxyCreationEnabled = false;
            Context.Configuration.LazyLoadingEnabled = true;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="proxyCreationEnabled">if set to <c>true</c> [proxy creation enabled].</param>
        /// <param name="lazyLoadingEnabled">if set to <c>true</c> [lazy loading enabled].</param>
        public UnitOfWork(bool proxyCreationEnabled, bool lazyLoadingEnabled)
        {
            Context = new BISTDbContext();
            Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            Context.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
        }
        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        ~UnitOfWork()
        {
            Context.Dispose();
        }

        #region Properties Variables
        /// <summary>
        /// The Opciones por rol repository
        /// </summary>
        GenericRepository<Accounts> _userRepository;

        GenericRepository<Roles> _rolesRepository;

        GenericRepository<Projects> _projeectsRepository;
        

        #endregion

        #region Properties
        /// <summary>
        /// Gets the opciones por rol repository.
        /// </summary>
        /// <value>The opciones por rol repository.</value>
        public GenericRepository<Accounts> AccountsRepository
        {
            get {
                return _userRepository ??
                       (_userRepository = new GenericRepository<Accounts>(Context));
            }
        }

        public GenericRepository<Roles> RolesRepository
        {
            get
            {
                return _rolesRepository ??
                       (_rolesRepository = new GenericRepository<Roles>(Context));
            }
        }

        public GenericRepository<Projects> ProjectsRepository
        {
            get
            {
                return _projeectsRepository ??
                       (_projeectsRepository = new GenericRepository<Projects>(Context));
            }
        }

        #endregion

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            try
            {                
                Context.CommitAndRefreshChanges();
            }
                
            catch (DbEntityValidationException dbex)
            {
// ReSharper disable once PossibleIntendedRethrow
                throw dbex;
            }
            catch (Exception ex)
            {   
// ReSharper disable PossibleIntendedRethrow
                throw ex;
// ReSharper restore PossibleIntendedRethrow
            }
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
// ReSharper disable RedundantAssignment
        public bool Save(ref string errors)
// ReSharper restore RedundantAssignment
        {
            errors = string.Empty;
            bool answer = false;
            try
            {
                answer = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbex)
            {
                string results = string.Empty;
                dbex.EntityValidationErrors.ToList().ForEach(e => {
                    string keyName = e.Entry.Entity.GetType().ToString();
                    string keyValues = string.Empty;
                    e.ValidationErrors.ToList().ForEach(v =>
                    {
                        keyValues = v.PropertyName + " : " + v.ErrorMessage + Environment.NewLine;
                    });
                    results += keyName + " : " + keyValues + Environment.NewLine;
                });
                errors = results;
                //throw dbex;
            }
            catch (Exception ex)
            {
                errors += ex.InnerException != null ? ex.Message + " Details: " + ex.InnerException : ex.Message;
                //throw ex;
            }
            return answer;
        }

        /// <summary>
        /// The disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

