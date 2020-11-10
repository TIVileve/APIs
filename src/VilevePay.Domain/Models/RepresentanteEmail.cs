using System;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Models
{
    public class RepresentanteEmail : Entity
    {
        public RepresentanteEmail(Guid id)
        {
            Id = id;
        }

        // Empty constructor for EF
        protected RepresentanteEmail()
        {
        }

        public int TipoEmail { get; set; }
        public string Email { get; set; }

        public virtual Representante Representante { get; set; }
        public Guid RepresentanteId { get; set; }

        public RepresentanteEmail Update()
        {
            return this;
        }
    }
}