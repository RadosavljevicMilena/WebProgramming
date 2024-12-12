namespace WebTemplate.Models;

public class Racun
{
    [Key]
    public int ID { get; set; }

    [StringLength(13)]
    public required string BrojRacuna { get; set; }

    public DateTime DatumOtvaranjaRacuna { get; set; }

    public double DostupnaSredstva { get; set; }

    public double UkupnoPodignutoDoSada { get; set; }

    public Klijent? Klijent { get; set; }

    public Banka? Banka { get; set; }

}