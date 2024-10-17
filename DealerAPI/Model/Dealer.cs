namespace DealerAPI.Model;

public class Dealer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid DealerTypeId { get; set; }

    public virtual DealerType DealerType { get; set; }
}

