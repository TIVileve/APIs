using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class RepresentanteEmailRepository : Repository<RepresentanteEmail>, IRepresentanteEmailRepository
    {
        public RepresentanteEmailRepository(VileveContext context)
            : base(context)
        {
        }
    }
}