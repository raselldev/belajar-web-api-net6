using System.Text.Json.Serialization;

namespace BelajarWebApi.External.Contracts.Ghibli
{
    public class Film
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("original_title")]
        public string? OriginalTitle { get; set; }
    }
}
