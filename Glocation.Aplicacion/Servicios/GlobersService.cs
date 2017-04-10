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
    public class GlobersService
    {

        public RespuestaWebAPI<GloberDTO> getGloberByUserName(string username)
        {
            RespuestaWebAPI<GloberDTO> respuestaWebApi = new RespuestaWebAPI<GloberDTO>();
            UnitOfWork unitOfWork = new UnitOfWork();

            try
            {
                GlobersRepository globersRepo = new GlobersRepository(unitOfWork);
                Globers glober= globersRepo.getGlobersByUserName(username);

                GloberDTO response = new GloberDTO
                {
                    GloberId = glober.GloberId,
                    UserName = glober.UserName
                };

                respuestaWebApi.Datos = response;

            }
            catch (Exception exception)
            {
                respuestaWebApi.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            }
            return respuestaWebApi;
        }

        public RespuestaWebAPI<List<GloberDTO>> getGloberByProject(int id)
        {
            RespuestaWebAPI<List<GloberDTO>> respuestaWebApi = new RespuestaWebAPI<List<GloberDTO>>();
            UnitOfWork unitOfWork = new UnitOfWork();

            try
            {
                GlobersRepository globersRepo = new GlobersRepository(unitOfWork);
                List<Globers> globers = globersRepo.getGlobersByProject(id);

                List<GloberDTO> response = globers.Select(glober => new GloberDTO
                {
                    GloberId = glober.GloberId,
                    UserName = glober.UserName
                }).ToList();

                respuestaWebApi.Datos = response;

            }
            catch (Exception exception)
            {
                respuestaWebApi.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            }
            return respuestaWebApi;
        }

        public RespuestaWebAPI<GloberDTO> updateGlober(GloberDTO glober)
        {
            RespuestaWebAPI<GloberDTO> respuestaWebApi = new RespuestaWebAPI<GloberDTO>();
            UnitOfWork unitOfWork = new UnitOfWork();
            try
            {
                GlobersRepository globersRepo = new GlobersRepository(unitOfWork);
                Globers globerToUpdate = new Globers
                {
                    GloberId = 2,
                    UserName = glober.UserName,
                    RoleId = 1,
                    FirstName = "Lalo",
                    LastName = "Landa",
                    MiddleName = "Jimeno",
                    PositionId = glober.PositionId,
                    ProjectId = 1                    
                };

                globersRepo.updateGlober(globerToUpdate);
                unitOfWork.Save();

            }
            catch (Exception exception)
            {
                respuestaWebApi.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            }
            return respuestaWebApi;
        }
    }
}
