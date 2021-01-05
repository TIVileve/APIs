using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IAutorizacaoAppService : IDisposable
    {
        Task<object> Login(string email, string senha);

        void CadastrarSenha(string codigoConvite, string numeroCelular, string email, string senha,
            string confirmarSenha);

        Task ValidarCodigoConvite(string codigoConvite);
        Task ValidarTokenSms(string codigoConvite, string numeroCelular, string codigoToken);
        Task ValidarTokenEmail(string codigoConvite, string numeroCelular, string email, string codigoToken);
        Task EnviarTokenSms(string numeroCelular);
        Task EnviarTokenEmail(string email);
        Task ValidarSelfie(string codigoConvite, string numeroCelular, string fotoBase64);
    }
}