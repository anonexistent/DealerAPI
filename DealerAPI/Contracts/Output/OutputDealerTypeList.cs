using DealerAPI.Model;
using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Output;

public class OutputDealerTypeList
{
    [JsonPropertyName("dealerTypes")]
    public OutputDealerType[] DealerTypes { get; set; }

    public OutputDealerTypeList(ICollection<DealerType> ds)
    {
        DealerTypes = ds.Select(x => new OutputDealerType(x)).ToArray();
    }
}
