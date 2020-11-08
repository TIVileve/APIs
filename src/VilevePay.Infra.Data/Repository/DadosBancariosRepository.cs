using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class DadosBancariosRepository : Repository<DadosBancarios>, IDadosBancariosRepository
    {
        public DadosBancariosRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}