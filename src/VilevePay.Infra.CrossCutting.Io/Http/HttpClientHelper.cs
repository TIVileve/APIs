using System.Net.Http;
using System.Threading.Tasks;

namespace VilevePay.Infra.CrossCutting.Io.Http
{
    public class HttpClientHelper
    {
        public static async Task<T> OnGet<T>(HttpClient httpClient, string route)
        {
            var response = await httpClient.GetAsync(route);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(responseContent);

            return await response.Content.ReadAsAsync<T>();
        }
    }
}