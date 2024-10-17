using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Input;

public class DealerIdQuery
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
