using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IAutorizacaoAppService : IDisposable
    {
        Task<object> Login(string email, string senha);
        void CadastrarSenha(string codigoConvite, string email, string senha, string confirmarSenha);
        void ValidarCodigoConvite(string codigoConvite);
        void ValidarTokenSms(string codigoConvite, string numeroCelular, string codigoToken);
        void ValidarTokenEmail(string codigoConvite, string email, string codigoToken);
        void EnviarTokenSms(string codigoConvite, string numeroCelular);
        void EnviarTokenEmail(string codigoConvite, string email);
        void ValidarSelfie(string codigoConvite, string fotoBase64);
    }
}