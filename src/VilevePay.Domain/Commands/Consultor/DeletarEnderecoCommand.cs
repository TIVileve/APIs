using System;
using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class DeletarEnderecoCommand : ConsultorCommand, IRequest<bool>
    {
        public DeletarEnderecoCommand(string codigoConvite, Guid enderecoId)
        {
            CodigoConvite = codigoConvite;
            EnderecoId = enderecoId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeletarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}