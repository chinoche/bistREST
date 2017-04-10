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
    }
}