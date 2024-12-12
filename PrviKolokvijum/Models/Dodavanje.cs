namespace WebTemplate.Models;

public class Dodavanje
{
    [Key]
    public int ID { get; set; }

    public DateTime DatumDodavanja { get; set; }

    /*Koliko riba te vrste smo dodali odjednom*/
    public int BrojJedinkiTeVrste { get; set; }

    public Akvarijum? Akvarijum { get; set; }

    public Riba? Riba { get; set; }

}