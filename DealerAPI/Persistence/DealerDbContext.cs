using DealerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Persistence;

public class DealerDbContext : DbContext
{
    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<DealerType> DealerTypes { get; set; }

    public DealerDbContext(DbContextOptions options) : base(options) { }

}
