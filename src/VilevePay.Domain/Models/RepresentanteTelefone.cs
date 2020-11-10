using System;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Models
{
    public class RepresentanteTelefone : Entity
    {
        public RepresentanteTelefone(Guid id)
        {
            Id = id;
        }

        // Empty constructor for EF
        protected RepresentanteTelefone()
        {
        }

        public int TipoTelefone { get; set; }
        public string Numero { get; set; }

        public virtual Representante Representante { get; set; }
        public Guid RepresentanteId { get; set; }

        public RepresentanteTelefone Update()
        {
            return this;
        }
    }
}