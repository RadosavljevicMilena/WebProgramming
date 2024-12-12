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
    
    [HttpPost("DodavanjeBolnice")]
    public async Task<ActionResult> DodavanjeBolnice([FromBody] Bolnica bolnica)
    {
        try
        {
            await Context.Bolnice.AddAsync(bolnica);
            await Context.SaveChangesAsync();
            return Ok($"Dodata bolnica sa ID {bolnica.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodavanjeLekara")]
    public async Task<ActionResult> DodavanjeLekara([FromBody] Lekar lekar)
    {
        try
        {
            await Context.Lekari.AddAsync(lekar);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je lekar sa ID {lekar.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajZaposlenje/{identifikacioniBroj}/{datumPotpisivanjaUgovora}/{specijalnost}/{bolnicaID}/{lekarID}")]
    public async Task<ActionResult> DodajZaposlenje(int identifikacioniBroj, DateTime datumPotpisivanjaUgovora, string specijalnost, int bolnicaID, int lekarID)
    {
        try
        {
            var bolnica = await Context.Bolnice.FindAsync(bolnicaID);
            var lekar = await Context.Lekari.FindAsync(lekarID);

            if(bolnica != null && lekar != null)
            {
                var zaposlenje = new Zaposlen
                {
                    IdentifikacioniBroj = identifikacioniBroj,
                    DatumPotpisivanjaUgovora = datumPotpisivanjaUgovora,
                    Specijalnost = specijalnost,
                    Bolnica = bolnica,
                    Lekar = lekar
                };

                await Context.Zaposleni.AddAsync(zaposlenje);
                await Context.SaveChangesAsync();
                return Ok($"Dodato zaposlenje sa ID {zaposlenje.ID}");
            }
            else
            {
                return BadRequest("Ne postoji lekar ili bolnica sa datim ID-om!");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PronadjiLekaraBolnice/{bolnicaID}")]
    public async Task<ActionResult> PronadjiLekaraBolnice(int bolnicaID)
    {
        try
        {
            var lekari = await Context.Zaposleni
                        .Include(p => p.Bolnica)
                        .Include(p => p.Lekar)
                        .Where(p => p.Bolnica!.ID == bolnicaID)
                        .Select(p => new{
                            p.Lekar,
                            p.Specijalnost
                    }).ToListAsync();

            return Ok(lekari);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProveraLicence/{bolnicaID}")] 
    public async Task<ActionResult> ProveraLicence(int bolnicaID)
    {
        try
        {
           var zaposleni = await Context.Zaposleni
                        .Include(p => p.Lekar)
                        .Include(p => p.Bolnica)
                        .Where(p => p.Bolnica!.ID == bolnicaID)
                        .Where(p => p.Lekar!.DatumDobijanjaLicence == null)
                        .Select(p => new {
                            p.Lekar,
                            Licenca = "Nema licencu"
                        }).ToArrayAsync();

            return Ok(zaposleni);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
