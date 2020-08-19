using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}