using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IConsultorAppService : IDisposable
    {
        void CadastrarDadosBancarios(string codigoConvite);
        void CadastrarEmail(string codigoConvite);
        void CadastrarEndereco(string codigoConvite);
        void CadastrarPessoaJuridica(string codigoConvite);
        void CadastrarTelefone(string codigoConvite);
        Task<object> ObterStatusOnboarding(string codigoConvite);
    }
}