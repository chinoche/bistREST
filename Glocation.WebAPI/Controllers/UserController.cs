using System.Collections.Generic;
using System.Web.Http;
using Glocation.Aplicacion;
using Glocation.Aplicacion.Servicios;
using Glocation.Common.DTO;

namespace Glocation.WebAPI.Controllers
{

    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/Users/getTest")]
        public RespuestaWebAPI<List<UserDTO>> getUsers()
        {
            UserService userService = new UserService();
            return userService.getAllUsers();
        }
    }
}