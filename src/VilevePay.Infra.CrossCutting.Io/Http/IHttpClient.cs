using System.Net.Http;

namespace VilevePay.Infra.CrossCutting.Io.Http
{
    public interface IHttpAppService
    {
        HttpClient CreateClient(string baseUri);
    }
}