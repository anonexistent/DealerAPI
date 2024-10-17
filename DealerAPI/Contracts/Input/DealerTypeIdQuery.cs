using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealerAPI.Contracts.Input;

public class DealerTypeIdQuery
{
    [Required]
    [JsonPropertyName("dealerTypeId")]
    public string DealerTypeId { get; set; }
}
