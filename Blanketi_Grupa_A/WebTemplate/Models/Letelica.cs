using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Letelica
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    public int KapacitetPutnika { get; set; }

    public int BrojClanovaPosade { get; set; }

    public required string BrojMotora { get; set; }

    [JsonIgnore]
    public List<Let>? Letovi { get; set; }
}