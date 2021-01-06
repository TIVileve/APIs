using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class RepresentanteEmail : Entity
    {
        public RepresentanteEmail(Guid id, int tipoEmail, string email, Guid representanteId)
        {
            Id = id;
            TipoEmail = tipoEmail;
            Email = email;
            RepresentanteId = representanteId;
        }

        // Empty constructor for EF
        protected RepresentanteEmail()
        {
        }

        public int TipoEmail { get; set; }
        public string Email { get; set; }

        public virtual Representante Representante { get; set; }
        public Guid RepresentanteId { get; set; }

        public RepresentanteEmail Update(int tipoEmail, string email)
        {
            TipoEmail = tipoEmail;
            Email = email;

            return this;
        }
    }
}