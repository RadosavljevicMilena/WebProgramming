using Microsoft.Identity.Client;

namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Racun> Racuni { get; set; }
    public required DbSet<Banka> Banke { get; set; }
    public required DbSet<Klijent> Klijenti { get; set; }
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}