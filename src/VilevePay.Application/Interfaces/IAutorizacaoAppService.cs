using System;

namespace VilevePay.Application.Interfaces
{
    public interface IAutorizacaoAppService : IDisposable
    {
        void ValidarCodigoConvite(string codigoConvite);
        void ValidarCodigoToken(string codigoConvite, string codigoToken);
        void ValidarEmail(string codigoConvite, string email);
    }
}