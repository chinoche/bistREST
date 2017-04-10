using System.Collections.Generic;
using System.Web.Http;
using Glocation.Aplicacion;
using Glocation.Aplicacion.Servicios;
using Glocation.Common.DTO;

namespace Glocation.WebAPI.Controllers
{
    public class GlobersController : ApiController
    {
        [HttpPost]
        [Route("api/globers/getGlober/username/")]
        public RespuestaWebAPI<GloberDTO> GetGloberByUserName(GloberDTO globerDTO)
        {
            GlobersService globerService = new GlobersService();
            return globerService.getGloberByUserName(globerDTO.UserName);
        }

        [HttpPost]
        [Route("api/globers/update/")]
        public RespuestaWebAPI<GloberDTO> UpdateGlober(GloberDTO globerDTO)
        {
            GlobersService globerService = new GlobersService();
            return globerService.updateGlober(globerDTO);
            
        }

        [HttpPost]
        [Route("api/globers/getGlober/project/")]
        public RespuestaWebAPI<List<GloberDTO>> GetGloberByProject(ProjectsDTO project)
        {
            GlobersService globerService = new GlobersService();
            return globerService.getGloberByProject(project.ProjectId);
        }
    }
}