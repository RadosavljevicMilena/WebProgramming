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

    [HttpPost("DodavanjeAerodroma")]
    public async Task<ActionResult> DodavanjeAerodroma([FromBody] Aerodrom aerodrom)
    {
        try
        {
            await Context.Aerodromi.AddAsync(aerodrom);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno dodat aerodrom sa ID {aerodrom.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodavanjeLetelice/{naziv}/{kapacitetPutnika}/{kapacitetPosade}/{brMotora}")]
    public async Task<ActionResult> DodavanjeLetelice(string naziv, int kapacitetPutnika, int kapacitetPosade, string brMotora)
    {
        try
        {
            var letelica = new Letelica()
            {
                Naziv = naziv,
                KapacitetPutnika = kapacitetPutnika,
                BrojClanovaPosade = kapacitetPosade,
                BrojMotora = brMotora
            };

            if(letelica != null)
            {
                await Context.Letelice.AddAsync(letelica);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata letelica sa ID {letelica.ID}");
            }
            else
            {
                return BadRequest("Nevalidni podaci za letelicu!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodavanjeLeta/{vremePoletanja}/{vremeSletanja}/{aerodromP}/{aerodromS}/{letelica}")]
    public async Task<ActionResult> DodavanjeLeta(DateTime vremePoletanja, DateTime vremeSletanja, int aerodromP, int aerodromS, int letelica)
    {
        try
        {
            if(vremePoletanja>vremeSletanja)
                return BadRequest("Datum i vreme poletanja mora biti manje od datuma i vremena sletanja");
            if(aerodromP == aerodromS)
                return BadRequest("Ne postoji ovakav let. Aerodrom poletanja i sletanja moraju biti razliciti!");
            var aerodromPol = await Context.Aerodromi.FindAsync(aerodromP);
            var aerodromSl = await Context.Aerodromi.FindAsync(aerodromS);
            var avion = await Context.Letelice.FindAsync(letelica);

            var Let = new Let()
            {
                VremePoletanja = vremePoletanja,
                VremeSletanja = vremeSletanja,
                AerodromPoletanja = aerodromPol,
                AerodromSletanja = aerodromSl,
                Letelica = avion
            };

            if(Let != null)
            {
                await Context.Letovi.AddAsync(Let);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodat let sa ID {Let.ID}");
            }
            else
            {
                return BadRequest("Nevalidni podaci za let!");
            }
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PronadjiLet/{aerodromPoletanja}")]
    public async Task<ActionResult> PronadjiLet(int aerodromPoletanja)
    {
        try
        {
            var sviLetovi = Context.Letovi
                        .Include(p => p.AerodromPoletanja)
                        .Include(p => p.AerodromSletanja)
                        .Include(p => p.Letelica)
                        .Where(p => p.AerodromPoletanja!.ID == aerodromPoletanja)
                        .Select(p => new {
                            p.AerodromPoletanja,
                            p.AerodromSletanja,
                            p.Letelica,
                            p.VremePoletanja,
                            p.VremeSletanja
                        });
            return Ok(await sviLetovi.ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProsecnaDuzinaLeta/{aerodrom1}/{aerodrom2}")]
    public async Task<ActionResult> ProsecnaDuzinaLeta(int aerodrom1, int aerodrom2)
    {
        try
        {
            var prosek = await Context.Letovi
                    .Include(p => p.AerodromPoletanja)
                    .Include(p => p.AerodromSletanja)
                    .Where(p => (p.AerodromPoletanja!.ID == aerodrom1 && p.AerodromSletanja!.ID == aerodrom2) || (p.AerodromSletanja!.ID == aerodrom1 && p.AerodromPoletanja!.ID == aerodrom2))
                    .ToListAsync();

            if(prosek.Count == 0)
                return BadRequest("Nema letova izmedju prosledjenih destinacija");
            
            return Ok("Prosecna duzina leta izmedju destinacija je "+TimeSpan.FromSeconds(prosek.Average(p => (p.VremeSletanja - p.VremePoletanja).TotalSeconds)));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
