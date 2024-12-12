namespace WebTemplate.Models;

public class Relacija
{
    [Key]
    public int ID { get; set; }

    public int BrojPutnika { get; set; }

    public double CenaKarte { get; set; }

    public DateTime DatumSaobracanja { get; set; }

    public Grad? GradDolaska { get; set; }

    public Grad? GradPolaska { get; set; }

    public Voz? VozSaobracaja { get; set; }
}