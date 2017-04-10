using Glocation.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glocation.DataAccess.RepositorioEntidades
{
    public class RolesRepository
    {
        public UnitOfWork.UnitOfWork UnitOfWork { get; set; }

        public RolesRepository(UnitOfWork.UnitOfWork uoWContext)
        {
            UnitOfWork = uoWContext;
        }

        public List<Roles> getRoles()
        {
            List<Roles> roles= UnitOfWork.RolesRepository.Get(
                orderBy: q=> q.OrderBy(d => d.RoleId)).ToList();

            return roles;
        }

    }
}
