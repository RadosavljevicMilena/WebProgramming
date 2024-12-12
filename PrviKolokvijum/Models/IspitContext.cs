namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!

    public required DbSet<Akvarijum> Akvarijumi { get; set; }

    public required DbSet<Riba> Ribe { get; set; }

    public required DbSet<Dodavanje> Dodavanja { get; set; }
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
