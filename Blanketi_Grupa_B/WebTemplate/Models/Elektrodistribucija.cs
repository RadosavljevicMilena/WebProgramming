namespace WebTemplate.Models;

public class Elektrodistribucija
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    [MaxLength(30)]
    public required string Grad { get; set; }

    [EmailAddress]
    public required string EmailAdresa { get; set; }

    public int BrojRadnika { get; set; }

    public List<DistributivnoPodrucje>? DistributivnaPodrucje { get; set; }
}