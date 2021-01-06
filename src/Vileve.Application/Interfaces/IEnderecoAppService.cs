using System;
using System.Threading.Tasks;

namespace Vileve.Application.Interfaces
{
    public interface IEnderecoAppService : IDisposable
    {
        Task<object> ObterEndereco(string cep);
    }
}