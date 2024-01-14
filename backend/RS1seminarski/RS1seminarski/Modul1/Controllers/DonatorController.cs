using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1seminarski.Data;
using RS1seminarski.Helper;
using RS1seminarski.Modul1.Models;
using RS1seminarski.Modul1.ViewModels;

namespace RS1seminarski.Modul1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DonatorController : ControllerBase
    {
        private readonly DataContext _context;

        public DonatorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Donator>>> GetAll()
        {
            return Ok(await _context.Donatori.Where(x=>x.isDeleted==false).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Donator>> GetById(int id)
        {
            var dbDonator = await _context.Donatori.Where(x=>x.isDeleted==false).FirstOrDefaultAsync(x=>x.Id==id);

            if (dbDonator == null)
                return BadRequest("Greska! - Donator nije pronadjen");

            return Ok(dbDonator);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Donator>> GetByKorisnickiId(int id)
        {
            var dbDonator = await _context.Donatori.Where(x => x.isDeleted == false).Include(x=>x.Kontakt).FirstOrDefaultAsync(x=>x.KorisnickiNalogId==id);

            if (dbDonator == null)
                return BadRequest("Greska! - Donator nije pronadjen");

            return Ok(dbDonator);
        }

        [HttpPost]
        public async Task<ActionResult<Donator>> AddDonator(DonatorAddVM obj)
        {
            if (obj != null)
            {
                Donator novi = new Donator
                {
                    StatusDonatora= obj.StatusDonatora,
                    BrojDonacija = obj.BrojDonacija,
                    BrojNarudzbi = obj.BrojNarudzbi,
                    BrojZvjezdica = obj.BrojZvjezdica,
                    SlikaDonatora = obj.SlikaDonatora,
                    KorisnickiNalogId =obj.KorisnickiNalogId,
                    OsobaId=obj.OsobaId
                };
                _context.Donatori.Add(novi);
                await _context.SaveChangesAsync();
                return Ok(novi);
            }
            return BadRequest("Greska!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Donator>> UpdateDonatorBroj(int id)
        {
            var dbDonator = await _context.Donatori.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbDonator == null)
                return BadRequest("Greska! - Donator nije pronadjen");


            dbDonator.BrojDonacija = _context.Donacije.Where(x => x.isDeleted == false && x.DonatorId == id).Count();            

            if(dbDonator.BrojDonacija<=3)
                dbDonator.BrojZvjezdica=dbDonator.BrojDonacija;
            else
                dbDonator.BrojZvjezdica = 3;

            await _context.SaveChangesAsync();

            return Ok(await _context.Donatori.FindAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult<Donator>> UpdateDonator(int id, DonatorAddVM obj)
        {
            var dbDonator = await _context.Donatori.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x=>x.Id==id);

            if (dbDonator == null)
                return BadRequest("Greska! - Donator nije pronadjen");

            dbDonator.StatusDonatora = obj.StatusDonatora;
            dbDonator.BrojDonacija = obj.BrojDonacija;
            dbDonator.BrojNarudzbi = obj.BrojNarudzbi;
            dbDonator.BrojZvjezdica = obj.BrojZvjezdica;
            dbDonator.SlikaDonatora = obj.SlikaDonatora;
            //dbDonator.KorisnickiNalogId = obj.KorisnickiNalogId;
            //dbDonator.OsobaId = obj.OsobaId;



            await _context.SaveChangesAsync();

            return Ok(await _context.Donatori.FindAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Donator>> DeleteDonator(int id)
        {
            var dbDonator = await _context.Donatori.FirstOrDefaultAsync(x=>x.Id==id);

            if (dbDonator == null)
                return BadRequest("Greska! - Donator nije pronadjen");

            _context.Donatori.Remove(dbDonator);

            await _context.SaveChangesAsync();

            return Ok(dbDonator);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Donator>> SoftDeleteDonator(int id)
        {
            var dbDonator = await _context.Donatori.FirstOrDefaultAsync(x => x.Id == id);

            if (dbDonator == null)
                return BadRequest("Greska! - Donator nije pronadjen");

            dbDonator.isDeleted= true;

            await _context.SaveChangesAsync();

            return Ok(dbDonator);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Donator>> AddSlikaDonatora(int id, [FromForm] ImgSingleVM obj)
        {
            try
            {
                Donator donator = _context.Donatori.FirstOrDefault(d => d.Id == id);

                if (obj.vmSlika != null && donator != null)
                {
                    obj.vmSlika.CopyTo(new FileStream(Config.SlikeDonatoraFolder + obj.vmSlika.FileName, FileMode.Create));
                    donator.SlikaDonatora = Config.SlikeDonatoraUrl + obj.vmSlika.FileName;
                    _context.SaveChanges();
                }

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
    }
}
