using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class RepresentanteRepository : Repository<Representante>, IRepresentanteRepository
    {
        public RepresentanteRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}