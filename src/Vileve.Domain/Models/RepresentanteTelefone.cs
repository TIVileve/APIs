using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class RepresentanteTelefone : Entity
    {
        public RepresentanteTelefone(Guid id, int tipoTelefone, string numero, Guid representanteId)
        {
            Id = id;
            TipoTelefone = tipoTelefone;
            Numero = numero;
            RepresentanteId = representanteId;
        }

        // Empty constructor for EF
        protected RepresentanteTelefone()
        {
        }

        public int TipoTelefone { get; set; }
        public string Numero { get; set; }

        public virtual Representante Representante { get; set; }
        public Guid RepresentanteId { get; set; }

        public RepresentanteTelefone Update(int tipoTelefone, string numero)
        {
            TipoTelefone = tipoTelefone;
            Numero = numero;

            return this;
        }
    }
}