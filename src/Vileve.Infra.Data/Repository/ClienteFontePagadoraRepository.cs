using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ClienteFontePagadoraRepository : Repository<ClienteFontePagadora>, IClienteFontePagadoraRepository
    {
        public ClienteFontePagadoraRepository(VileveContext context)
            : base(context)
        {
        }
    }
}