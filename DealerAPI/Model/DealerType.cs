namespace DealerAPI.Model;

public class DealerType
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual List<Dealer> Dealers { get; set; } = new();
}
