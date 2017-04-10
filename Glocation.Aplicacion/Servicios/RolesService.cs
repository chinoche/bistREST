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
    public class RolesService
    {
        public RespuestaWebAPI<List<RolesDTO>> getAllRoles()
        {
            RespuestaWebAPI<List<RolesDTO>> respuestaWebApi = new RespuestaWebAPI<List<RolesDTO>>();
            UnitOfWork unitOfWork = new UnitOfWork();

            try
            {
                RolesRepository rolesRepo = new RolesRepository(unitOfWork);
                List<Roles> roles = rolesRepo.getRoles();

                respuestaWebApi.Datos = roles.Select(role => new RolesDTO
                {
                    Description = role.Description
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
