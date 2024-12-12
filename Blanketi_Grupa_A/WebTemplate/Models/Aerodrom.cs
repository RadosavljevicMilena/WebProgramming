using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Aerodrom
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    [StringLength(3)]
    public required string Kod { get; set; }

    public double KoordX { get; set; }

    public double KoordY { get; set; }

    public int KapacitetLetelica { get; set; }

    public int KapacitetPutnika { get; set; }

    [JsonIgnore]
    public List<Let>? LetoviPoleteli { get; set; }

    [JsonIgnore]
    public List<Let>? LetoviSleteli { get; set; }
}