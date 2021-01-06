using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class RepresentanteRepository : Repository<Representante>, IRepresentanteRepository
    {
        public RepresentanteRepository(VileveContext context)
            : base(context)
        {
        }
    }
}