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
    public class UserService
    {
        public RespuestaWebAPI<List<UserDTO>> getAllUsers()
        {
            RespuestaWebAPI<List<UserDTO>> respuestaWebApi = new RespuestaWebAPI<List<UserDTO>>();
            UnitOfWork unitOfWork = new UnitOfWork();

            try
            {
                UserRepository userRepo = new UserRepository(unitOfWork);
                List<User> users = userRepo.getUsers();

                respuestaWebApi.Datos = users.Select(user => new UserDTO
                {
                    Name = user.Name
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
