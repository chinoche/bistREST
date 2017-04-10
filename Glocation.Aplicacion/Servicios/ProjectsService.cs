using Glocation.Common.DTO;
using Glocation.DataAccess.RepositorioEntidades;
using Glocation.DataAccess.UnitOfWork;
using Glocation.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glocation.Aplicacion.Servicios
{
    public class ProjectsService
    {
        
        public RespuestaWebAPI<List<ProjectsDTO>> getProjects()
        {
            RespuestaWebAPI<List<ProjectsDTO>> respuestaWebApi = new RespuestaWebAPI<List<ProjectsDTO>>();
            UnitOfWork unitOfWork = new UnitOfWork();

            try
            {
                ProjectsRepository projectsRepo = new ProjectsRepository(unitOfWork);
                List<Projects> projects = projectsRepo.getProjects();

                respuestaWebApi.Datos = projects.Select(project => new ProjectsDTO
                {
                    ProjectId = project.ProjectId
                
                }).ToList();

            }
            catch (Exception exception)
            {
                respuestaWebApi.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            }
            return respuestaWebApi;
        }
    }
}
