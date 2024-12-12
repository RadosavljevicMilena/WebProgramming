namespace WebTemplate.Models;

public class Akvarijum
{
    [Key]
    public int ID { get; set; }

    [StringLength(6)]
    public required string Sifra { get; set; }

    public double Zapremina { get; set; }

    public double Temperatura { get; set; }

    public DateTime DatumPoslednjegCiscenja { get; set; }

    public int FrekvencijaCiscenja { get; set; }

    public int Kapacitet { get; set; }

    public List<Dodavanje>? Dodavanja { get; set; }
}