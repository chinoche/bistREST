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

        public List<User> getUsers()
        {
            List<User> users = new List<User>();
            users.Add(new User(1, "lalo landa"));
            users.Add(new User(1, "Pechugas Laru"));

            return users;
        }
    }
}
