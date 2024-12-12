using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Kuvar
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(20)]
    public required string Ime { get; set; }

    [MaxLength(30)]
    public required string Prezime { get; set; }

    public DateTime DatumRodjenja { get; set; }

    public required string StrucnaSprema { get; set; }
    
    [JsonIgnore]
    public List<Zaposlen>? Zaposlenja { get; set; }
}