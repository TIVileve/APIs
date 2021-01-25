using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ClienteEnderecoRepository : Repository<ClienteEndereco>, IClienteEnderecoRepository
    {
        public ClienteEnderecoRepository(VileveContext context)
            : base(context)
        {
        }
    }
}