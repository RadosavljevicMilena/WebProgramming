namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Relacija> Relacije { get; set; }
    public required DbSet<Grad> Gradovi { get; set; }
    public required DbSet<Voz> Vozovi { get; set; }

    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Relacija>()
        .HasOne(p => p.GradPolaska)
        .WithMany(p => p.RelacijePolaska);

        modelBuilder.Entity<Relacija>()
        .HasOne(p => p.GradDolaska)
        .WithMany(p => p.ReklacijeDolaska);
    }
}
