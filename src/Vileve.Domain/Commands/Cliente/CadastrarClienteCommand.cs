using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarClienteCommand : ClienteCommand, IRequest<object>
    {
        public CadastrarClienteCommand(string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Email = email;
            TelefoneFixo = telefoneFixo;
            TelefoneCelular = telefoneCelular;
            ConsultorId = consultorId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}