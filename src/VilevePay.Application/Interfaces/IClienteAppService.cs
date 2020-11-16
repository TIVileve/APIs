using System;

namespace VilevePay.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        void CadastrarCliente();
    }
}