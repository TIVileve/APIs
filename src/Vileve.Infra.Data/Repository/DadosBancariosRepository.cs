using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class DadosBancariosRepository : Repository<DadosBancarios>, IDadosBancariosRepository
    {
        public DadosBancariosRepository(VileveContext context)
            : base(context)
        {
        }
    }
}