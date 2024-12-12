namespace WebTemplate.Models;

public class Lekar
{
    [Key]
    public int ID { get; set; }

    [MaxLength(30)]
    public required string Ime { get; set; }

    [MaxLength(40)]
    public required string Prezime { get; set; }

    public DateTime DatumRodjenja { get; set; }

    public DateTime DatumDiplomiranja { get; set; }

    public DateTime? DatumDobijanjaLicence { get; set; }

    public List<Zaposlen>? ZaposlenjaLekari { get; set; }
}