using DealerAPI.Model;
using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Output;

public class OutputDealerType
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }

    public OutputDealerType(DealerType d)
    {
        Id = d.Id.ToString();
        Name = d.Name;
    }
}
