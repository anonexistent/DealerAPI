using DealerAPI.Model;
using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Output;

public class OutputDealer
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("dealerType")]
    public OutputDealerType DealerType { get; set; }

    public OutputDealer(Dealer d)
    {
        Id = d.Id.ToString();
        Name = d.Name.ToString();
        Description = d.Description.ToString();
        DealerType = new OutputDealerType(d.DealerType);
    }

}
