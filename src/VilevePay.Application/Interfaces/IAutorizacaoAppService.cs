using System;

namespace VilevePay.Application.Interfaces
{
    public interface IAutorizacaoAppService : IDisposable
    {
        void ValidarCodigoConvite(string codigoConvite);
        void ValidarCodigoToken(string codigoConvite, string numeroCelular, string codigoToken);
        void ValidarEmail(string codigoConvite, string email, string codigoToken);
        void EnviarSmsToken(string codigoConvite, string numeroCelular);
        void EnviarVerificadorEmail(string codigoConvite, string email);
    }
}