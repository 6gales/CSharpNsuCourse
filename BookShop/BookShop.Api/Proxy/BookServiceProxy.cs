using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.Api.JsonModels;
using Newtonsoft.Json;

namespace BookShop.Api.Proxy
{
    public class BookServiceProxy : IBookServiceProxy
    {
        private const string Uri = "https://getbooksrestapi.azurewebsites.net/api/books/";
        private readonly HttpClient _httpClient;

        public BookServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<JsonBook>> GetBooks(int count)
        {
            var response = await _httpClient.GetAsync($"{Uri}{count}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IList<JsonBook>>(content);
        }
    }
}