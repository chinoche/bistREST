using Glocation.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glocation.DataAccess.RepositorioEntidades
{
    public class UserRepository
    {
        public UnitOfWork.UnitOfWork UnitOfWork { get; set; }

        public UserRepository(UnitOfWork.UnitOfWork uoWContext)
        {
            UnitOfWork = uoWContext;
        }

        public List<Accounts> getUsers()
        {
            IQueryable<Accounts> accounts= UnitOfWork.Context.Accounts.Include("Roles");
            return accounts.ToList();
        }

        public Accounts insertAccounts(Accounts account)
        {
            return UnitOfWork.AccountsRepository.Insert(account);
        }
    }
}
