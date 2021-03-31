using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class ClienteFontePagadora : Entity
    {
        public ClienteFontePagadora(Guid id, long? inssNumeroBeneficio, double? inssSalario, int? inssEspecie, int? outrosDiaPagamento,
            Guid clienteId)
        {
            Id = id;
            InssNumeroBeneficio = inssNumeroBeneficio;
            InssSalario = inssSalario;
            InssEspecie = inssEspecie;
            OutrosDiaPagamento = outrosDiaPagamento;
            ClienteId = clienteId;
        }

        // Empty constructor for EF
        protected ClienteFontePagadora()
        {
        }

        public long? InssNumeroBeneficio { get; set; }
        public double? InssSalario { get; set; }
        public int? InssEspecie { get; set; }
        public int? OutrosDiaPagamento { get; set; }

        public virtual Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }

        public ClienteFontePagadora Update(long? inssNumeroBeneficio, double? inssSalario, int? inssEspecie, int? outrosDiaPagamento)
        {
            InssNumeroBeneficio = inssNumeroBeneficio;
            InssSalario = inssSalario;
            InssEspecie = inssEspecie;
            OutrosDiaPagamento = outrosDiaPagamento;

            return this;
        }
    }
}