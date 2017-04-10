using System.Collections.Generic;
using System.Web.Http;
using Glocation.Aplicacion;
using Glocation.Aplicacion.Servicios;
using Glocation.Common.DTO;

namespace Glocation.WebAPI.Controllers
{
    public class ProjectsController : ApiController
    {
        [HttpGet]
        [Route("api/projects/")]
        public RespuestaWebAPI<List<ProjectsDTO>> getProjects()
        {
            ProjectsService projectsService = new ProjectsService();
            return projectsService.getProjects();
        }

        [HttpPost]
        [Route("api/projects/save")]
        public RespuestaWebAPI<ProjectsDTO> insertProject(ProjectsDTO project)
        {
            ProjectsService projectsService = new ProjectsService();
            return projectsService.insertProject(project);
        }

        [HttpGet]
        [Route("api/projects/")]
        public RespuestaWebAPI<List<ProjectsDTO>> getProjectsById()
        {
            ProjectsService projectsService = new ProjectsService();
            return projectsService.getProjects();
        }
    }
}