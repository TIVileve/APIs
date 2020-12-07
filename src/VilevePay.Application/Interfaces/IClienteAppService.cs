using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        void CadastrarCliente();
        Task<object> ObterProduto();
        void CadastrarProduto();
    }
}