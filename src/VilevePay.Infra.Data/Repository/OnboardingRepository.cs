using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository
{
    public class OnboardingRepository : Repository<Onboarding>, IOnboardingRepository
    {
        public OnboardingRepository(VilevePayContext context)
            : base(context)
        {
        }
    }
}