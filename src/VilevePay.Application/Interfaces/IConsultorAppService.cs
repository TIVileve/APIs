using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IConsultorAppService : IDisposable
    {
        Task<object> ObterEndereco(string codigoConvite);
        Task<object> ObterEnderecoPorId(string codigoConvite, Guid enderecoId);

        void CadastrarEndereco(string codigoConvite, int tipoEndereco, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado, bool principal,
            string comprovanteBase64);

        void DeletarEndereco(string codigoConvite, Guid enderecoId);

        void CadastrarPessoaJuridica(string codigoConvite, string cnpj, string razaoSocial, string nomeFantasia, string inscricaoMunicipal,
            string inscricaoEstadual, string codigoBanco, string agencia, string contaSemDigito, string digito,
            int tipoConta, string contratoSocialBase64, string ultimaAlteracaoBase64);

        void CadastrarRepresentante(string codigoConvite);
        Task<object> ObterStatusOnboarding(string email);
    }
}