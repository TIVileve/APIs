using System;
using System.Threading.Tasks;

namespace VilevePay.Application.Interfaces
{
    public interface IConsultorAppService : IDisposable
    {
        Task<object> ObterStatusOnboarding(string codigoConvite);
    }
}