namespace WebTemplate.Models;

public class Grad
{
    [Key]
    public int ID { get; set; }

    [MaxLength(30)]
    public required string Naziv { get; set; }

    public int BrojStankovnika { get; set; }

    public int BrojKoloseka { get; set; }

    [Range(-180, 180)]
    public double Longitude { get; set; }

    [Range(-90, 90)]
    public double Latitude { get; set; }

    public List<Relacija>? RelacijePolaska { get; set; }

    public List<Relacija>? ReklacijeDolaska { get; set; }
}