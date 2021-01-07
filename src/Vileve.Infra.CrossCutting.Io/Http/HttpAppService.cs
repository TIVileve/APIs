using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Vileve.Infra.CrossCutting.Io.Http
{
    public class HttpAppService : IHttpAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static ILogger<HttpAppService> _logger;

        public HttpAppService(
            IHttpClientFactory httpClientFactory,
            ILogger<HttpAppService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public HttpClient CreateClient(string baseUri)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<T> OnGet<T>(HttpClient httpClient, string route)
        {
            var response = await httpClient.GetAsync(route);
            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
            {
                responseContent,
                route
            }));

            if (!response.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Error, JsonSerializer.Serialize(new
                {
                    errors = responseContent,
                    route
                }));
                throw new HttpRequestException(responseContent);
            }

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> OnPost<T, TObject>(HttpClient httpClient, string route, TObject content)
        {
            var jsonInString = content is string
                ? content.ToString()
                : JsonConvert.SerializeObject(content, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            var response = await httpClient.PostAsync(route, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
            {
                parameters = jsonInString,
                responseContent,
                route
            }));

            if (!response.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Error, JsonSerializer.Serialize(new
                {
                    parameters = jsonInString,
                    errors = responseContent,
                    route
                }));
                throw new HttpRequestException(responseContent);
            }

            return await response.Content.ReadAsAsync<T>();
        }
    }
}