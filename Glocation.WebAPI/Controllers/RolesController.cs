using System.Collections.Generic;
using System.Web.Http;
using BIST.Aplicacion;
using BIST.Aplicacion.Servicios;
using BIST.Common.DTO;

namespace BIST.WebAPI.Controllers
{
    public class RolesController : ApiController
    {
        [HttpGet]
        [Route("api/roles/")]
        public RespuestaWebAPI<List<RolesDTO>> getRoles()
        {
            RolesService rolesService = new RolesService();
            return rolesService.getAllRoles();
        }
    }
}