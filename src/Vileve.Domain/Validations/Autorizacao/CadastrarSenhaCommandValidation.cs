using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
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