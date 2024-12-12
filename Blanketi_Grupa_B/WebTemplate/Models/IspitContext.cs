namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!

    public required DbSet<Elektrodistribucija> Elektrodistribucije { get; set; }

    public required DbSet<Potrosac> Potrosaci { get; set; }

    public required DbSet<DistributivnoPodrucje> DistributivnaPodrucja { get; set; }

    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
