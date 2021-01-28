using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarDependenteCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarDependenteCommand(Guid clienteId, string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado)
        {
            ClienteId = clienteId;
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
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarDependenteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}