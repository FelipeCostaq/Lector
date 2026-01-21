using System.Text.Json;
using LectorASPNET.DTO;

namespace LectorASPNET.Services
{
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;

        public GoogleBooksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GoogleBookItem>> SearchBooksAsync(string query)
        {

            var url = $"https://www.googleapis.com/books/v1/volumes?q={query}&maxResults=10";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<GoogleBookItem>();
            }

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GoogleBooksResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Items ?? new List<GoogleBookItem>();
        }
    }
}