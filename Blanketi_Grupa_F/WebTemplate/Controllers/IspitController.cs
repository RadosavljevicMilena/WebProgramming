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

    [HttpPost("DodajKuvara")]
    public async Task<ActionResult> DodajKuvara([FromBody] Kuvar kuvar)
    {
        try
        {
            await Context.Kuvari.AddAsync(kuvar);
            await Context.SaveChangesAsync();
            
            return Ok($"Dodat je kuvar sa ID {kuvar.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajRestoran")]
    public async Task<ActionResult> DodajRestoran([FromBody] Restoran restoran)
    {
        try
        {
            await Context.Restorani.AddAsync(restoran);
            await Context.SaveChangesAsync();
            
            return Ok($"Dodat je restoran sa ID {restoran.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("ZaposliKuvara/{datum}/{plata}/{pozicija}/{restoranID}/{kuvarID}")]
    public async Task<ActionResult> ZaposliKuvara(DateTime datum, int plata, string pozicija, int restoranID, int kuvarID)
    {
        try
        {
            var kuvar = await Context.Kuvari.FindAsync(kuvarID);
            var restoran = await Context.Restorani
                        .Include(p => p.Zaposleni!)
                        .ThenInclude(p=>p.Kuvar)
                        .Where(p => p.ID == restoranID).FirstOrDefaultAsync();

            if(restoran != null && kuvar != null)
            {
                if(restoran.MaxBrojGostiju == restoran.Zaposleni!.Count)
                {
                     return BadRequest("Nemoguce zaposliti kuvara, sve pozicije su popunjene!");
                }

                if(restoran.Zaposleni.Any(p => p.Kuvar!.ID == kuvarID))
                {
                    return BadRequest($"Kuvar {kuvar.Ime} {kuvar.Prezime} je vec zaposlen u restoranu {restoran.Naziv}");
                }

                var zaposlenje = new Zaposlen
                {
                    DatumZaposlenja = datum,
                    Plata = plata,
                    Pozicija = pozicija,
                    Restoran = restoran,
                    Kuvar = kuvar
                };

                await Context.Zaposleni.AddAsync(zaposlenje);
                await Context.SaveChangesAsync();

                return Ok($"Kuvar sa ID {kuvarID} je uspesno zaposljen u restoranu sa ID {restoranID}");
            }
            else
            {
                 return BadRequest("Nevalidni unos za ID kuvara ili restorana!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PlataKuvara/{kuvarID}")]
    public async Task<ActionResult> PlataKuvara(int kuvarID)
    {
        try
        {
            var plata = await Context.Kuvari
                        .Include(p => p.Zaposlenja)
                        .Where(p => p.ID == kuvarID)
                        .SelectMany(p => p.Zaposlenja!)
                        .SumAsync(p => p.Plata);
            return Ok(plata);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("RadiUNajviseRestorana")]
    public async Task<ActionResult> RadiUNajviseRestorana()
    {
        try
        {
            var kuvar = await Context.Kuvari
                        .Include(p => p.Zaposlenja)
                        .OrderByDescending(p => p.Zaposlenja!.Count)
                        .FirstOrDefaultAsync();

            return Ok(kuvar);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}
