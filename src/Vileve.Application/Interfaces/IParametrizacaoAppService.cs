using System;
using System.Threading.Tasks;

namespace Vileve.Application.Interfaces
{
    public interface IParametrizacaoAppService : IDisposable
    {
        Task<object> ObterEstadoCivil();
        Task<object> ObterNacionalidade();
        Task<object> ObterPerfilUsuario();
        Task<object> ObterTipoTelefone();
        Task<object> ObterTipoEmail();
        Task<object> ObterTipoEndereco();
        Task<object> ObterBanco();
        Task<object> ObterOperacaoBancaria();
        Task<object> ObterSexo();
    }
}