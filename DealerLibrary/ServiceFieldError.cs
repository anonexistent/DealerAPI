using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealerLibrary;

public class ServiceFieldError
{
    [Required]
    [JsonPropertyName("fields")]
    public ICollection<string> Fields { get; set; }

    [Required]
    [JsonPropertyName("message")]
    public string Message { get; set; }
}

