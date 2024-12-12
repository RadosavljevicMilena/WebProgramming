namespace WebTemplate.Models;

public class DistributivnoPodrucje
{
    [Key]
    public int ID { get; set; }

    public int KorisnickiBroj { get; set; }

    public required string BrojBrojila { get; set; }

    public DateTime DatumPotpisivanjaUgovora { get; set; }

    public Potrosac? PotrosacDistrPodrucja { get; set; }

    public Elektrodistribucija? Elektrodistribucije { get; set; }
}