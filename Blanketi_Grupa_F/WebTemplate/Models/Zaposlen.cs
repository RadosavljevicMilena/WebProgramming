namespace WebTemplate.Models;

public class Zaposlen
{
    [Key]
    public int ID { get; set; }

    public DateTime DatumZaposlenja { get; set; }

    public int Plata { get; set; }

    public required string Pozicija { get; set; }

    public Restoran? Restoran { get; set; }

    public Kuvar? Kuvar { get; set; }
}