using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ClienteDocumentoRepository : Repository<ClienteDocumento>, IClienteDocumentoRepository
    {
        public ClienteDocumentoRepository(VileveContext context)
            : base(context)
        {
        }
    }
}