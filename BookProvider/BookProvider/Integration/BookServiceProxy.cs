using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookProvider.ContractImplementation;
using BookShop.MessageContract;
using Newtonsoft.Json;

namespace BookProvider.Integration
{
    public sealed class BookServiceProxy : IBookServiceProxy
    {
        private readonly string _externalServiceUri;
        private readonly HttpClient _httpClient;

        public BookServiceProxy(HttpClient httpClient, string externalServiceUri)
        {
            _externalServiceUri = externalServiceUri;
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<IBookResponse>> GetBooks(int count)
        {
            var response = await _httpClient.GetAsync($"{_externalServiceUri}{count}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<JsonBook>>(content);
        }
    }
}