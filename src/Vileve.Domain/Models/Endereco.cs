using System;
using Vileve.Domain.Core.Models;
using Vileve.Domain.Enums;

namespace Vileve.Domain.Models
{
    public class Endereco : Entity
    {
        public Endereco(Guid id, TipoEndereco tipoEndereco, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado, bool principal,
            string comprovanteBase64, Guid consultorId)
        {
            Id = id;
            TipoEndereco = tipoEndereco;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Principal = principal;
            ComprovanteBase64 = comprovanteBase64;
            ConsultorId = consultorId;
        }

        // Empty constructor for EF
        protected Endereco()
        {
        }

        public TipoEndereco TipoEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Principal { get; set; }
        public string ComprovanteBase64 { get; set; }

        public virtual Consultor Consultor { get; set; }
        public Guid ConsultorId { get; set; }

        public Endereco Update(TipoEndereco tipoEndereco, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado, bool principal,
            string comprovanteBase64)
        {
            TipoEndereco = tipoEndereco;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Principal = principal;
            ComprovanteBase64 = comprovanteBase64;

            return this;
        }
    }
}