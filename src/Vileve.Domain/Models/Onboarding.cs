using System;
using Vileve.Domain.Core.Models;
using Vileve.Domain.Enums;

namespace Vileve.Domain.Models
{
    public class Onboarding : Entity
    {
        public Onboarding(Guid id)
        {
            Id = id;
        }

        // Empty constructor for EF
        protected Onboarding()
        {
        }

        public string CodigoConvite { get; set; }
        public string NumeroCelular { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public StatusOnboarding StatusOnboarding { get; set; }
        public virtual Consultor Consultor { get; set; }

        public Onboarding Update()
        {
            return this;
        }
    }
}