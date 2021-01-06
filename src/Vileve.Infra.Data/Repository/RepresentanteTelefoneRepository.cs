using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class RepresentanteTelefoneRepository : Repository<RepresentanteTelefone>, IRepresentanteTelefoneRepository
    {
        public RepresentanteTelefoneRepository(VileveContext context)
            : base(context)
        {
        }
    }
}