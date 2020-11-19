using System;
using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class ObterEnderecoPorIdCommand : ConsultorCommand, IRequest<object>
    {
        public ObterEnderecoPorIdCommand(string codigoConvite, string numeroCelular, Guid enderecoId)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            EnderecoId = enderecoId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterEnderecoPorIdCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}