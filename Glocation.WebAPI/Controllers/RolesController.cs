using System.Collections.Generic;
using System.Web.Http;
using Glocation.Aplicacion;
using Glocation.Aplicacion.Servicios;
using Glocation.Common.DTO;

namespace Glocation.WebAPI.Controllers
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