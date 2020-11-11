using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class RepresentanteEmailRepository : Repository<RepresentanteEmail>, IRepresentanteEmailRepository
    {
        public RepresentanteEmailRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}