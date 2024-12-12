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

    [HttpPost("DodajGrad")]
    public async Task<ActionResult> DodajGrad([FromBody] Grad grad)
    {
        try
        {
            await Context.Gradovi.AddAsync(grad);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je grad sa ID {grad.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajVoz")]
    public async Task<ActionResult> DodajVoz([FromBody] Voz voz)
    {
        try
        {
            await Context.Vozovi.AddAsync(voz);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je voz sa ID {voz.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajRelaciju/{brPutnika}/{cena}/{datum}/{gradPol}/{gradDol}/{voz}")]
    public async Task<ActionResult> DodajRelaciju(int brPutnika, double cena, DateTime datum, int gradPol, int gradDol, int voz)
    {
        try
        {
            if(gradPol == gradDol)
            {
                return BadRequest("Nemoguce je da isti grad bude i grad polaska i grad dolaska!");
            }

            var gradPolaska = await Context.Gradovi.FindAsync(gradPol);
            var gradDolaska = await Context.Gradovi.FindAsync(gradDol);
            var vozSaob = await Context.Vozovi.FindAsync(voz);

            if(gradDolaska != null && gradPolaska != null && vozSaob != null)
            {
                if(brPutnika > vozSaob.MaxBrzina)
                {
                    return BadRequest("Broj putnika na relaciji premasuje kapacitet voza!");
                }
                var relacija = new Relacija
                {
                    BrojPutnika = brPutnika,
                    CenaKarte = cena,
                    DatumSaobracanja = datum,
                    GradDolaska = gradDolaska,
                    GradPolaska = gradPolaska,
                    VozSaobracaja = vozSaob
                };

                await Context.Relacije.AddAsync(relacija);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je relacija izmedju grada {gradPolaska.Naziv} i {gradDolaska.Naziv} vozom sa ID {voz}");
            }
            else
            {
                return BadRequest("Nevalidni podaci za grad polaska, grad dolaska ili voz!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PretraziVozove/{grad}")]
    public async Task<ActionResult> PretraziVozove(int grad)
    {
        try
        {
            var vozovi = await Context.Relacije
                        .Include(p => p.GradDolaska)
                        .Include(p => p.GradPolaska)
                        .Include(p => p.VozSaobracaja)
                        .Where(p=> p.GradDolaska!.ID == grad || p.GradPolaska!.ID == grad)
                        .Select(p => p.VozSaobracaja).ToListAsync();

            return Ok(vozovi);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UkupnaZaradaVozaNaRelaciji/{vozID}")]
    public async Task<ActionResult> UkupnaZaradaVozaNaRelaciji(int vozID)
    {
        try
        {
            var ukupnaZarada = await Context.Vozovi
                                .Include(p => p.Relacije)
                                .Where(p => p.ID == vozID)
                                .SelectMany(p => p.Relacije!)
                                .SumAsync(p => p.BrojPutnika * p.CenaKarte);

            return Ok(ukupnaZarada);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
