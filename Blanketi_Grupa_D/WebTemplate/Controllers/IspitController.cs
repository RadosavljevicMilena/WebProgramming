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
    
    [HttpPost("DodajBanku")]
    public async Task<ActionResult> DodajBanku([FromBody] Banka banka)
    {
        try
        {
            await Context.Banke.AddAsync(banka);
            await Context.SaveChangesAsync();
            return Ok($"Dodata banka sa ID {banka.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajKlijenta/{ime}/{prezime}/{datumRodjenja}/{brTelefona}")]
    public async Task<ActionResult> DodajKlijenta(string ime, string prezime, DateTime datumRodjenja, string brTelefona)
    {
        try
        {
            var klijent = new Klijent
            {
                Ime = ime,
                Prezime = prezime,
                DatumRodjenja = datumRodjenja,
                BrojTelefona = brTelefona
            };

            await Context.Klijenti.AddAsync(klijent);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je klijent sa ID {klijent.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 

    [HttpPost("DodajRacun/{brRacuna}/{datum}/{dostupnaSr}/{ukupno}/{klijentID}/{bankaID}")]
    public async Task<ActionResult> DodajRacun(string brRacuna, DateTime datum, double dostupnaSr, double ukupno, int klijentID, int bankaID)
    {
        try
        {
            var klijent = await Context.Klijenti.FindAsync(klijentID);
            var banka = await Context.Banke.FindAsync(bankaID);

            if(banka != null && klijent != null)
            {
                var racun = new Racun
                {
                    BrojRacuna = brRacuna,
                    DatumOtvaranjaRacuna = datum,
                    DostupnaSredstva = dostupnaSr,
                    UkupnoPodignutoDoSada = ukupno,
                    Klijent = klijent,
                    Banka = banka
                };

                await Context.Racuni.AddAsync(racun);
                await Context.SaveChangesAsync();
                return Ok($"Klijent sa ID {klijent.ID} kreirao je racun sa ID {racun.ID} u banci sa ID {banka.ID}");
            }
            else
            {
                return BadRequest("Ne postoje banka ili klijent sa prosledjenim ID-em");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("IzmeniStanjeRacuna/{brRacuna}/{novoStanje}")]
    public async Task<ActionResult> IzmeniStanjeRacuna(string brRacuna, double novoStanje)
    {
        try
        {
            var racun = await Context.Racuni
                        .Where(p => p.BrojRacuna == brRacuna)
                        .FirstOrDefaultAsync();

            if(racun != null)
            {
                if(racun.DostupnaSredstva > novoStanje)
                {
                    racun.UkupnoPodignutoDoSada += (racun.DostupnaSredstva - novoStanje);
                }
                racun.DostupnaSredstva = novoStanje;
                await Context.SaveChangesAsync();
                return Ok($"Novo stanje racuna je {racun.DostupnaSredstva}");
            }
            else
            {
                return BadRequest("Ne postoji racun sa tim brojem!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("SumaSvihSredstavaBanke/{bankaID}")]
    public async Task<ActionResult> SumaSvihSredstavaBanke(int bankaID)
    {
        try
        {
            var suma = await Context.Banke
                        .Include(p => p.Racuni)
                        .Where(p => p.ID == bankaID)
                        .SelectMany(p => p.Racuni!)
                        .SumAsync(p => p.UkupnoPodignutoDoSada + p.DostupnaSredstva);

            return Ok($"Ukupna sredstva koje je banka imala i ima je {suma}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
