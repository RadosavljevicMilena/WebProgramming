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

    [HttpPost("DodajAkvarijum")]
    public async Task<ActionResult> DodajAkvarijum([FromBody] Akvarijum akvarijum)
    {
        try
        {
            await Context.Akvarijumi.AddAsync(akvarijum);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno je dodat akvarijum sa ID {akvarijum.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajRibu/{naziv}/{masa}/{godine}")]
    public async Task<ActionResult> DodajRibu(string naziv, double masa, int godine)
    {
        try
        {
            var riba = new Riba
            {
                NazivVrste = naziv,
                Masa = masa,
                GodineStarosti = godine
            };

            await Context.Ribe.AddAsync(riba);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno je dodata riba sa ID {riba.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajRibuUAkvarijum/{datum}/{brojJedinki}/{akvarijumID}/{ribaID}")]
    public async Task<ActionResult> DodajRibuUAkvarijum(DateTime datum, int brojJedinki, int akvarijumID, int ribaID)
    {
        try
        {
            var riba = await Context.Ribe.FindAsync(ribaID);
            var akvarijum = await Context.Akvarijumi
                        .Include(p => p.Dodavanja!)
                        .ThenInclude(p => p.Riba)
                        .Where(p => p.ID == akvarijumID).FirstOrDefaultAsync();

            if(riba != null && akvarijum != null)
            {
                if(akvarijum.Dodavanja!.Count == akvarijum.Kapacitet)
                {
                    return BadRequest("Kapacitet akvarjuma je popunjen, nemoguce je dodavati jos riba!");
                }

                var count = await Context.Dodavanja
                            .Include(p => p.Akvarijum)
                            .Include(p => p.Riba)
                            .Where(p => p.Akvarijum!.ID == akvarijumID)
                            .Where(p => p.Riba!.Masa >= riba.Masa*10 || p.Riba!.Masa*10 >= riba.Masa)
                            .CountAsync();


                if(count != 0)
                {
                    return BadRequest("Nemoguce dodati ribu u akvarijum zbog konflikta sa ostalim ribama!");
                }
                
                var dodavanje = new Dodavanje
                {
                    DatumDodavanja = datum,
                    BrojJedinkiTeVrste = brojJedinki,
                    Akvarijum = akvarijum,
                    Riba = riba
                };

                await Context.Dodavanja.AddAsync(dodavanje);
                await Context.SaveChangesAsync();

                return Ok($"{dodavanje.BrojJedinkiTeVrste} {riba.NazivVrste} su dodate u akvarijum");
            }
            else
            {
                return BadRequest("Nevalidni podaci za ribu ili akvarijum!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

     /*Azuriranje - menjamo datum i menjamo kolicinu, to prosledjujemo*/
    [HttpPut("AzurirajRibe/{akvarijumID}/{ribaID}/{kolicina}/{datum}")]
    public async Task<ActionResult> AzurirajRibe(int akvarijumID, int ribaID, int kolicina, DateTime datum)
    {
        try
        {
            var akvarijum = await Context.Dodavanja
                        .Include(p => p.Akvarijum)
                        .Include(p => p.Riba)
                        .Where(p => p.Akvarijum!.ID == akvarijumID)
                        .Where(p => p.Riba!.ID == ribaID)
                        .FirstOrDefaultAsync();

            if(akvarijum != null)
            {
                akvarijum.BrojJedinkiTeVrste = kolicina;
                akvarijum.DatumDodavanja = datum;
                await Context.SaveChangesAsync();

                return Ok("Postojece jedinke su azurirane");
            }
            else
            {
                return BadRequest("Ne postoji akvarijum sa zadatim ID-om");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PronadjiAkvarijumeZaCiscenje")]
    public async Task<ActionResult> PronadjiAkvarijumeZaCiscenje()
    {
        try
        {
            var akvarijumi = await Context.Akvarijumi
                            .Where(p => p.DatumPoslednjegCiscenja.AddDays(p.FrekvencijaCiscenja) > DateTime.Now).ToListAsync();

            return Ok(akvarijumi);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UkupnaMasaRiba/{akvarijumID}")]
    public async Task<ActionResult> UkupnaMasaRiba(int akvarijumID)
    {
        try
        {
            var masa = await Context.Dodavanja
                        .Include(p => p.Akvarijum)
                        .Include(p => p.Riba)
                        .Where(p => p.Akvarijum!.ID == akvarijumID)
                        .SumAsync(p => p.Riba!.Masa*p.BrojJedinkiTeVrste);

            return Ok($"Masa svih riba u akvarijumu je {masa}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
