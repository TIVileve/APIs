using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<object> CadastrarCliente();
        Task<object> ObterProduto();
        void CadastrarProduto(Guid clienteId);

        void CadastrarEndereco(Guid clienteId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado);

        void CadastrarDependente(Guid clienteId);
    }
}