using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Input;

public class CreateDealerTypeBody
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
