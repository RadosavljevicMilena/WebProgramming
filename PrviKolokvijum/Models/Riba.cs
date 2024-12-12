namespace WebTemplate.Models;

public class Riba
{
    [Key]
    public int ID { get; set; }

    public required string NazivVrste { get; set; }

    public double Masa { get; set; }

    public int GodineStarosti { get; set; }

    public List<Dodavanje>? Dodavanja { get; set; }
}