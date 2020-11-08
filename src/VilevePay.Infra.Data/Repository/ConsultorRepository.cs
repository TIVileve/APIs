using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class ConsultorRepository : Repository<Consultor>, IConsultorRepository
    {
        public ConsultorRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}