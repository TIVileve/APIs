using System.Net.Http;

namespace Vileve.Infra.CrossCutting.Io.Http
{
    public interface IHttpAppService
    {
        HttpClient CreateClient(string baseUri);
    }
}