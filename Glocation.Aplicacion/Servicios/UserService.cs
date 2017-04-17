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
    public class UserService
    {
        public RespuestaWebAPI<List<UserDTO>> getAllUsers()
        {
            RespuestaWebAPI<List<UserDTO>> respuestaWebApi = new RespuestaWebAPI<List<UserDTO>>();
            UnitOfWork unitOfWork = new UnitOfWork();

            try
            {
                UserRepository userRepo = new UserRepository(unitOfWork);
                List<Accounts> users = userRepo.getUsers();

                respuestaWebApi.Datos = users.Select(user => new UserDTO
                {
                    Name = user.Name,
                    Roles = user.Roles

                }).ToList();
                
            }
            catch (Exception exception)
            {
                respuestaWebApi.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            }
            return respuestaWebApi;
        }

        public RespuestaWebAPI<UserDTO> insertUser(UserDTO userDto)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            UserRepository userRepository = new UserRepository(unitOfWork);

            RespuestaWebAPI<UserDTO> respuestaWebApi = new RespuestaWebAPI<UserDTO>();
            try
            {
                Accounts account = new Accounts()
                {
                    AccountId = userDto.UserId,
                    Name = userDto.Name,
                    Email = userDto.Name,
                    Roles = userDto.Roles,
                    Projects = userDto.Projects
                };
                userRepository.insertAccounts(account);
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
