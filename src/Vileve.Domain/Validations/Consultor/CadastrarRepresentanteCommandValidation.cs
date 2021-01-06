using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public class CadastrarRepresentanteCommandValidation : ConsultorValidation<CadastrarRepresentanteCommand>
    {
        public CadastrarRepresentanteCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}