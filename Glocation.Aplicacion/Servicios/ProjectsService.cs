using BIST.Common.DTO;
using BIST.DataAccess.RepositorioEntidades;
using BIST.DataAccess.UnitOfWork;
using BIST.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIST.Aplicacion.Servicios
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

        public RespuestaWebAPI<ProjectsDTO> insertProject(ProjectsDTO projectDTO)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ProjectsRepository projectRepository = new ProjectsRepository(unitOfWork);

            RespuestaWebAPI<ProjectsDTO> respuestaWebApi = new RespuestaWebAPI<ProjectsDTO>();
            try
            {
                Projects project = new Projects()
                {
                    ProjectName = projectDTO.ProjectName,
                    ShippingAddress = projectDTO.Address
                };
                projectRepository.insertProject(project);
                unitOfWork.Save();
            }
            catch (Exception exception)
            {
                respuestaWebApi.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name,
                    exception);
            }

            return respuestaWebApi;

        }

    }
}
