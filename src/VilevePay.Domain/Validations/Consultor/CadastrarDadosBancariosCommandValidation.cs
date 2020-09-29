using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarDadosBancariosCommandValidation : ConsultorValidation<CadastrarDadosBancariosCommand>
    {
        public CadastrarDadosBancariosCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}