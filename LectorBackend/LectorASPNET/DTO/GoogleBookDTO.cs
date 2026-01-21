using System.Text.Json.Serialization;

namespace LectorASPNET.DTO
{
    public class GoogleBooksResponse
    {
        [JsonPropertyName("items")]
        public List<GoogleBookItem>? Items { get; set; }
    }

    public class GoogleBookItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; } = new();
    }

    public class VolumeInfo
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("authors")]
        public List<string>? Authors { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("imageLinks")]
        public ImageLinks? ImageLinks { get; set; }
        
        [JsonPropertyName("publishedDate")]
        public string? PublishedDate { get; set; }
        
        [JsonPropertyName("pageCount")]
        public int? PageCount { get; set; }
    }

    public class ImageLinks
    {
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
    }
}
