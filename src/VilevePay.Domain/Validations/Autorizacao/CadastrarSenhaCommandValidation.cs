using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class CadastrarSenhaCommandValidation : AutorizacaoValidation<CadastrarSenhaCommand>
    {
        public CadastrarSenhaCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateEmail();
            ValidateSenha();
            ValidateConfirmarSenha();
        }
    }
}