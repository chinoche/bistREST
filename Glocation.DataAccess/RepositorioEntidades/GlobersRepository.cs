using Glocation.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Glocation.DataAccess.RepositorioEntidades
{
    public class GlobersRepository
    {
        public UnitOfWork.UnitOfWork UnitOfWork { get; set; }

        public GlobersRepository(UnitOfWork.UnitOfWork uoWContext)
        {
            UnitOfWork = uoWContext;
        }

        public List<Globers> getGlobers()
        {
            List<Globers> globers= UnitOfWork.GlobersRepository.Get(
                orderBy: q => q.OrderBy(d => d.GloberId)).ToList();

            return globers;
        }

        public Globers getGlobersByUserName(string username)
        {
            Globers glober = UnitOfWork.GlobersRepository.Get(
                filter: g => g.UserName.Equals(username),
                includeProperties:"Projects, Roles, Position").FirstOrDefault();

            return glober;
        }

        public List<Globers> getGlobersByProject(int id)
        {
            List<Globers> globers = UnitOfWork.GlobersRepository.Get(
                filter: q => q.ProjectId.Equals(id)).ToList();

            return globers;
        }

        public Globers updateGlober (Globers glober)
        {
            return UnitOfWork.GlobersRepository.Update(glober);
        }
    }
}
