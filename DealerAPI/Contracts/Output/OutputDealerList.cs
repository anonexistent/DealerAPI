using DealerAPI.Model;
using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Output;

public class OutputDealerList
{
    [JsonPropertyName("dealers")]
    public OutputDealer[] Dealers{ get; set; }

    public OutputDealerList(ICollection<Dealer> ds)
    {
        Dealers = ds.Select(x=>new OutputDealer(x)).ToArray();
    }
}
