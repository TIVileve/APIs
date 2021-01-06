using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Repository
{
    public class OnboardingRepository : Repository<Onboarding>, IOnboardingRepository
    {
        public OnboardingRepository(VileveContext context)
            : base(context)
        {
        }
    }
}