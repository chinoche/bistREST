using System.Collections.Generic;
using System.Web.Http;
using BIST.Aplicacion;
using BIST.Aplicacion.Servicios;
using BIST.Common.DTO;

namespace BIST.WebAPI.Controllers
{

    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/accounts")]
        public RespuestaWebAPI<List<UserDTO>> getUsers()
        {
            UserService userService = new UserService();
            return userService.getAllUsers();
        }

        [HttpPost]
        [Route("api/accounts/save")]
        public RespuestaWebAPI<UserDTO> insertUser(UserDTO user)
        {
            UserService userService = new UserService();
            return userService.insertUser(user);
        }

    }
}