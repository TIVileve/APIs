using System;
using System.Threading.Tasks;

namespace Vileve.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<object> ObterClientePorId(Guid clienteId);

        Task<object> CadastrarCliente(string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId,
            long? inssNumeroBeneficio, double? inssSalario, int? inssEspecie, int? outrosDiaPagamento);

        void AtualizarCliente(Guid clienteId, string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId,
            long? inssNumeroBeneficio, double? inssSalario, int? inssEspecie, int? outrosDiaPagamento);

        Task<object> ObterProduto();
        void CadastrarProduto(Guid clienteId, string codigoProdutoItem);

        void CadastrarEndereco(Guid clienteId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64);

        void AtualizarEndereco(Guid clienteId, Guid enderecoId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64);

        Task<object> ObterDependente(Guid clienteId);
        Task<object> ObterDependentePorId(Guid clienteId, Guid dependenteId);

        void CadastrarDependente(Guid clienteId, string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado);

        void AtualizarDependente(Guid clienteId, Guid dependenteId, string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado);

        void DeletarDependente(Guid clienteId, Guid dependenteId);
        Task<object> ContratarProduto(Guid clienteId);
        void CadastrarPagamento(Guid clienteId);
    }
}