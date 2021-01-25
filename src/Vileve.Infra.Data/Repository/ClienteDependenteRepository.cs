using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ClienteDependenteRepository : Repository<ClienteDependente>, IClienteDependenteRepository
    {
        public ClienteDependenteRepository(VileveContext context)
            : base(context)
        {
        }
    }
}