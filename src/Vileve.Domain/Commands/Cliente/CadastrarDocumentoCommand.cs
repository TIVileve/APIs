using System;
using MediatR;
using Vileve.Domain.Enums;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarDocumentoCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarDocumentoCommand(Guid clienteId, string frenteBase64, string versoBase64, TipoDocumento tipoDocumento)
        {
            ClienteId = clienteId;
            FrenteBase64 = frenteBase64;
            VersoBase64 = versoBase64;
            TipoDocumento = tipoDocumento;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarDocumentoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}