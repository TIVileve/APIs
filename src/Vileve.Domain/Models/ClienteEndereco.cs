using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class ClienteEndereco : Entity
    {
        public ClienteEndereco(Guid id, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64, Guid clienteId)
        {
            Id = id;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            ComprovanteBase64 = comprovanteBase64;
            ClienteId = clienteId;
        }

        // Empty constructor for EF
        protected ClienteEndereco()
        {
        }

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string ComprovanteBase64 { get; set; }

        public virtual Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }

        public ClienteEndereco Update(string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64)
        {
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            ComprovanteBase64 = comprovanteBase64;

            return this;
        }
    }
}