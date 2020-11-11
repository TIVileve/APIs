using System;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Models
{
    public class DadosBancarios : Entity
    {
        public DadosBancarios(Guid id, string codigoBanco, string agencia, string contaSemDigito, string digito,
            string tipoConta, Guid consultorId)
        {
            Id = id;
            CodigoBanco = codigoBanco;
            Agencia = agencia;
            ContaSemDigito = contaSemDigito;
            Digito = digito;
            TipoConta = tipoConta;
            ConsultorId = consultorId;
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

        public DadosBancarios Update(string codigoBanco, string agencia, string contaSemDigito, string digito,
            string tipoConta)
        {
            CodigoBanco = codigoBanco;
            Agencia = agencia;
            ContaSemDigito = contaSemDigito;
            Digito = digito;
            TipoConta = tipoConta;

            return this;
        }
    }
}