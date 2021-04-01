using System;
using System.Collections.Generic;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class Cliente : Entity
    {
        public Cliente(Guid id, string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId)
        {
            Id = id;
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Email = email;
            TelefoneFixo = telefoneFixo;
            TelefoneCelular = telefoneCelular;
            ConsultorId = consultorId;
        }

        // Empty constructor for EF
        protected Cliente()
        {
        }

        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }

        public virtual Consultor Consultor { get; set; }
        public Guid? ConsultorId { get; set; }

        public virtual ClienteFontePagadora FontePagadora { get; set; }
        public virtual ClienteProduto Produto { get; set; }
        public virtual ICollection<ClienteEndereco> Enderecos { get; set; }
        public virtual ICollection<ClienteDependente> Dependentes { get; set; }

        public Cliente Update(string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Email = email;
            TelefoneFixo = telefoneFixo;
            TelefoneCelular = telefoneCelular;
            ConsultorId = consultorId;

            return this;
        }
    }
}