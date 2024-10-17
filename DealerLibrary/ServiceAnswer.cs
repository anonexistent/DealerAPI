using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealerLibrary;

public class ServiceAnswer<T>
{
    [Required]
    public bool Ok { get; set; }

    public T? Answer { get; set; }

    public ICollection<object> Errors { get; set; } = Array.Empty<object>();
}
