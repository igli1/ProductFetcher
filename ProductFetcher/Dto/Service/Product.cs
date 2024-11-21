using System.Text.Json.Serialization;

namespace ProductFetcher.Dto.Service;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
}