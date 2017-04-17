using BIST.Dominio.Entidades;

namespace BIST.DataAccess.RepositorioEntidades
{
    public class ProviderRepository
    {
        public UnitOfWork.UnitOfWork UnitOfWork { get; set; }

        public ProviderRepository(UnitOfWork.UnitOfWork uoWContext)
        {
            UnitOfWork = uoWContext;
        }


        public Provider InsertarProveedor(Provider provider)
        {
            return UnitOfWork.ProviderRepository.Insert(provider);
        }

    }
}