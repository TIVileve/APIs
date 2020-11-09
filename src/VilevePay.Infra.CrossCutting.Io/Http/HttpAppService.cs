using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace VilevePay.Infra.CrossCutting.Io.Http
{
    public class HttpAppService : IHttpAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpAppService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateClient(string baseUri)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}