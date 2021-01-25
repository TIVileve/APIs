using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class ClienteDependente : Entity
    {
        public ClienteDependente(Guid id, string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado, Guid clienteId)
        {
            Id = id;
            CodigoParentesco = codigoParentesco;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Email = email;
            TelefoneCelular = telefoneCelular;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            ClienteId = clienteId;
        }

        // Empty constructor for EF
        protected ClienteDependente()
        {
        }

        public string CodigoParentesco { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TelefoneCelular { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public virtual Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }

        public ClienteDependente Update(string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado)
        {
            CodigoParentesco = codigoParentesco;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Email = email;
            TelefoneCelular = telefoneCelular;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;

            return this;
        }
    }
}