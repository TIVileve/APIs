using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IEnderecoAppService : IDisposable
    {
        Task<object> ObterEndereco(string cep);
    }
}