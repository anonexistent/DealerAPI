using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Input;

public class CreateDealerBody
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
    [JsonPropertyName("description")]
    public string Description { get; init; }
    [JsonPropertyName("typeId")]
    public string TypeId { get; init; }

}
