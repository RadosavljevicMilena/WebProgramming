namespace WebTemplate.Models;

public class Klijent
{
    [Key]
    public int ID { get; set; }

    [MaxLength(30)]
    public required string Ime { get; set; }

    public required string Prezime { get; set; }

    public DateTime DatumRodjenja { get; set; }

    [RegularExpression(@"^(\+381|0)([1-9])([0-9]){6,9}$")]
    public required string BrojTelefona { get; set; }

    public List<Racun>? Racuni { get; set; }
}