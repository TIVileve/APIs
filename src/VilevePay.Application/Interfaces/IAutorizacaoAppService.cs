using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IAutorizacaoAppService : IDisposable
    {
        Task<object> Login(string email, string senha);
        void CadastrarSenha(string codigoConvite, string email, string senha, string confirmarSenha);
        Task ValidarCodigoConvite(string codigoConvite);
        void ValidarTokenSms(string codigoConvite, string numeroCelular, string codigoToken);
        Task ValidarTokenEmail(string codigoConvite, string email, string codigoToken);
        Task EnviarTokenSms(string codigoConvite, string numeroCelular);
        Task EnviarTokenEmail(string codigoConvite, string email);
        void ValidarSelfie(string codigoConvite, string fotoBase64);
    }
}