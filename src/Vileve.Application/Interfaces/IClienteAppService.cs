using System;
using System.Threading.Tasks;

namespace Vileve.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<object> CadastrarCliente(string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId);

        Task<object> ObterProduto();
        void CadastrarProduto(Guid clienteId, string codigoProduto);

        void CadastrarEndereco(Guid clienteId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64);

        Task<object> ObterDependente(Guid clienteId);
        Task<object> ObterDependentePorId(Guid clienteId, Guid dependenteId);
        void CadastrarDependente(Guid clienteId);
        void DeletarDependente(Guid clienteId, Guid dependenteId);
        void CadastrarPagamento(Guid clienteId);
        Task<object> ObterCalculoMensal(Guid clienteId);
    }
}