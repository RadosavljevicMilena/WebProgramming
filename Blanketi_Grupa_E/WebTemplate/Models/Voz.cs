namespace WebTemplate.Models;

public class Voz 
{
    [Key]
    public int ID { get; set; }
    public DateTime DatumProizvodnje { get; set; }

    public int MaxKapacitetPutnika { get; set; }

    public double MaxTezina { get; set; }

    public int MaxBrzina { get; set; }

    public List<Relacija>? Relacije { get; set; }
}