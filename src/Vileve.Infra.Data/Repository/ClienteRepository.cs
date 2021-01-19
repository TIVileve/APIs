using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(VileveContext context)
            : base(context)
        {
        }
    }
}