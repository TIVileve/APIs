using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IConsultorAppService : IDisposable
    {
        void CadastrarArquivo(string codigoConvite);
        void CadastrarDadosBancarios(string codigoConvite);
        void CadastrarDocumento(string codigoConvite);
        void CadastrarEmail(string codigoConvite);
        Task<object> ObterEndereco(string codigoConvite);
        Task<object> ObterEnderecoPorId(string codigoConvite, Guid enderecoId);
        void CadastrarEndereco(string codigoConvite);
        void DeletarEndereco(string codigoConvite, Guid enderecoId);
        void CadastrarPessoaJuridica(string codigoConvite);
        void CadastrarRepresentante(string codigoConvite);
        void CadastrarTelefone(string codigoConvite);
        Task<object> ObterStatusOnboarding(string email);
    }
}