using Glocation.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glocation.DataAccess.RepositorioEntidades
{
    public class ProjectsRepository
    {
        public UnitOfWork.UnitOfWork UnitOfWork { get; set; }

        public ProjectsRepository(UnitOfWork.UnitOfWork uoWContext)
        {
            UnitOfWork = uoWContext;
        }

        public List<Projects> getProjects()
        {
            List<Projects> globers = UnitOfWork.ProjectsRepository.Get(
                orderBy: q => q.OrderBy(d => d.ProjectId)).ToList();

            return globers;
        }

        public Projects insertProject(Projects project)
        {
            return UnitOfWork.ProjectsRepository.Insert(project);

        }
    }
}
