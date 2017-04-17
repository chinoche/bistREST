using System.Web.Http;
using BIST.Aplicacion;
using BIST.Aplicacion.Servicios;
using BIST.Dominio.Entidades;

namespace BIST.WebAPI.Controllers
{
    public class ProviderController: ApiController
    {
        [HttpPost]
        [Route("api/provider/registrar")]
        public RespuestaWebAPI<Provider> RegistrarComponente(Provider provider)
        {
            ProviderService serviciosProveedor = new ProviderService();
            return serviciosProveedor.RegistrarProvider(provider);
        }
    }
}