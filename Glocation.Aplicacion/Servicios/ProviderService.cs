using System;
using BIST.DataAccess.RepositorioEntidades;
using BIST.DataAccess.UnitOfWork;
using BIST.Dominio.Entidades;

namespace BIST.Aplicacion.Servicios
{
    public class ProviderService
    {
        public RespuestaWebAPI<Provider> RegistrarProvider(Provider prov)
        {
            RespuestaWebAPI<Provider> respuestaWebAPI = new RespuestaWebAPI<Provider>();
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                ProviderRepository repositorioProveedor = new ProviderRepository(unitOfWork);

                repositorioProveedor.InsertarProveedor(prov);
                unitOfWork.Save();

            }
            catch (Exception exception)
            {
                respuestaWebAPI.RegistrarExcepcion(GetType(), System.Reflection.MethodBase.GetCurrentMethod().Name,
                    exception);
            }
            return respuestaWebAPI;
        }
    }
}
