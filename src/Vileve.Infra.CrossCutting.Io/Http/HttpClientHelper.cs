using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vileve.Infra.CrossCutting.Io.Http
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

        public static async Task<T> OnPost<T, TObject>(HttpClient httpClient, string route, TObject content)
        {
            var jsonInString = content is string
                ? content.ToString()
                : JsonConvert.SerializeObject(content, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            var response = await httpClient.PostAsync(route, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(responseContent);

            return await response.Content.ReadAsAsync<T>();
        }
    }
}