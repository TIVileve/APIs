using System.Net.Http;
using System.Threading.Tasks;

namespace Vileve.Infra.CrossCutting.Io.Http
{
    public interface IHttpAppService
    {
        HttpClient CreateClient(string baseUri);
        Task<T> OnGet<T>(HttpClient httpClient, string route);
        Task<T> OnPost<T, TObject>(HttpClient httpClient, string route, TObject content);
    }
}