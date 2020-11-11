using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class RepresentanteTelefoneRepository : Repository<RepresentanteTelefone>, IRepresentanteTelefoneRepository
    {
        public RepresentanteTelefoneRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}