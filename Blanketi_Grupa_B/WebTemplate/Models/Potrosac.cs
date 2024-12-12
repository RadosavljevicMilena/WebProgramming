namespace WebTemplate.Models;

public class Potrosac
{
    [Key]
    public int ID { get; set; }

    [MaxLength(30)]
    public required string Ime { get; set; }

    [MaxLength(40)]
    public required string Prezime { get; set; }

    public DateTime GodinaRodjenja { get; set; }

    public required string MestoRodjenja { get; set; }

    public List<DistributivnoPodrucje>? DistributivnaPodrucjaPotrosaca { get; set; }
}