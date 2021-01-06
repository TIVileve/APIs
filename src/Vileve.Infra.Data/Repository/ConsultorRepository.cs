using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class ConsultorRepository : Repository<Consultor>, IConsultorRepository
    {
        public ConsultorRepository(VileveContext context)
            : base(context)
        {
        }
    }
}