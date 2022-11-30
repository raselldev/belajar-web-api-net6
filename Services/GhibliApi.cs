using BelajarWebApi.External.Contracts.Ghibli;
using System.Text.Json;

namespace BelajarWebApi.Services
{
    public class GhibliApi : IGhibliApi
    {
        private readonly HttpClient _httpClient;

        public GhibliApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Film> GetFilm(Guid id)
        {
            var response = await _httpClient.GetAsync(string.Format("https://ghibliapi.herokuapp.com/films/{0}", id));
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Film>(responseAsString);

            return result;
        }

        public async Task<IEnumerable<Film>> GetFilms()
        {
            var response = await _httpClient.GetAsync("https://ghibliapi.herokuapp.com/films");
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();

            var results = JsonSerializer.Deserialize<List<Film>>(responseAsString);

            return results;
        }
    }
}
