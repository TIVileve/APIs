using System;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Models
{
    public class DadosBancarios : Entity
    {
        public DadosBancarios(Guid id)
        {
            Id = id;
        }

        // Empty constructor for EF
        protected DadosBancarios()
        {
        }

        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string ContaSemDigito { get; set; }
        public string Digito { get; set; }
        public string TipoConta { get; set; }

        public virtual Consultor Consultor { get; set; }
        public Guid ConsultorId { get; set; }

        public DadosBancarios Update()
        {
            return this;
        }
    }
}