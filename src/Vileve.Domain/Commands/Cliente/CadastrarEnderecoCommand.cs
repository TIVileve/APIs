using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarEnderecoCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarEnderecoCommand(Guid clienteId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado)
        {
            ClienteId = clienteId;
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
            ValidationResult = new CadastrarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}