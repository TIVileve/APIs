using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IAutorizacaoAppService : IDisposable
    {
        Task<object> Login(string email, string senha);
        void ValidarCodigoConvite(string codigoConvite);
        void ValidarSms(string codigoConvite, string numeroCelular, string codigoToken);
        void ValidarEmail(string codigoConvite, string email, string codigoToken);
        void EnviarVerificadorSms(string codigoConvite, string numeroCelular);
        void EnviarVerificadorEmail(string codigoConvite, string email);
    }
}