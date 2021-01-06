using System;
using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class DeletarEnderecoCommand : ConsultorCommand, IRequest<bool>
    {
        public DeletarEnderecoCommand(string codigoConvite, string numeroCelular, Guid enderecoId)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            EnderecoId = enderecoId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeletarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}