using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class AtualizarEnderecoCommand : ClienteCommand, IRequest<bool>
    {
        public AtualizarEnderecoCommand(Guid clienteId, Guid enderecoId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64)
        {
            ClienteId = clienteId;
            EnderecoId = enderecoId;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            ComprovanteBase64 = comprovanteBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new AtualizarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}