namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

    [HttpPost("DodajElektrodistribuciju")]
    public async Task<ActionResult> DodajElektrodistribuciju([FromBody] Elektrodistribucija elektrodistribucija)
    {
        try
        {
            await Context.Elektrodistribucije.AddAsync(elektrodistribucija);
            await Context.SaveChangesAsync();
            return Ok($"Dodata elektrodistribucija sa ID {elektrodistribucija.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajPotrosaca")]
    public async Task<ActionResult> DodajPotrosaca([FromBody] Potrosac potrosac)
    {
        try
        {
            await Context.Potrosaci.AddAsync(potrosac);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je potrosac sa ID {potrosac.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodavanjePotrosacaDistribuciji/{korisnickiBroj}/{brojBrojila}/{datumPotpisivanja}/{potrosacID}/{elektrodistribucijaID}")]
    public async Task<ActionResult> DodavanjePotrosacaDistribuciji(int korisnickiBroj, string brojBrojila, DateTime datumPotpisivanja, int potrosacID, int elektrodistribucijaID)
    {
        try
        {
            var potrosac = await Context.Potrosaci.FindAsync(potrosacID);
            var elektrodistribucija = await Context.Elektrodistribucije.FindAsync(elektrodistribucijaID);
            if(potrosac != null && elektrodistribucija != null)
            {
                var distribucija = new DistributivnoPodrucje
                {
                    KorisnickiBroj = korisnickiBroj,
                    BrojBrojila = brojBrojila,
                    DatumPotpisivanjaUgovora = datumPotpisivanja,
                    PotrosacDistrPodrucja = potrosac,
                    Elektrodistribucije = elektrodistribucija
                };

                await Context.DistributivnaPodrucja.AddRangeAsync(distribucija);
                await Context.SaveChangesAsync();
                return Ok($"Dodata distribucija sa ID {distribucija.ID}");
            }
            else
            {
                return BadRequest("Nevalidni podaci!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("PronadjiPotrosaca/{brojBrojila}")]
    public async Task<ActionResult> PronadjiPotrosaca(string brojBrojila)
    {
        try
        {
            var potrosac = await Context.DistributivnaPodrucja
                            .Include(p => p.PotrosacDistrPodrucja)
                            .Where(p => p.BrojBrojila == brojBrojila)
                            .Select(p => p.PotrosacDistrPodrucja)
                            .ToListAsync();
            if(potrosac != null)
            {
                return Ok(potrosac);
            }
            else
            {
                return BadRequest($"Ne postoji potrosac sa brojem brojila {brojBrojila}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PronadjiSvePotrosace/{datumOd}/{datumDo}")]
    public async Task<ActionResult> PronadjiSvePotrosace(DateTime datumOd, DateTime datumDo)
    {
        try
        {
            var potrosaci = await Context.DistributivnaPodrucja
                            .Include( p => p.PotrosacDistrPodrucja)
                            .Where(p => p.DatumPotpisivanjaUgovora > datumOd && p.DatumPotpisivanjaUgovora < datumDo)
                            .Select(p => p.PotrosacDistrPodrucja)
                            .ToListAsync();
            if(potrosaci != null)
            {
                return Ok(potrosaci);
            }
            else
            {
                return BadRequest("Ne postoje potrosaci koji su potpisali ugovor u ovom vremenskom okviru!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
