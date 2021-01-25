using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ClienteProdutoRepository : Repository<ClienteProduto>, IClienteProdutoRepository
    {
        public ClienteProdutoRepository(VileveContext context)
            : base(context)
        {
        }
    }
}