using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1seminarski.Data;
using RS1seminarski.Modul1.Models;
using RS1seminarski.Modul1.ViewModels;

namespace RS1seminarski.Modul1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KontaktController : ControllerBase
    {
        private readonly DataContext _context;

        public KontaktController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kontakt>>> GetAll()
        {
            return Ok(await _context.Kontakti.Where(x=>x.isDeleted==false).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Kontakt>> GetById(int id)
        {
            var dbKontakt = await _context.Kontakti.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbKontakt == null)
                return BadRequest("Greska! -Kontakt nije pronadjen");

            return Ok(dbKontakt);
        }

        [HttpPost]
        public async Task<ActionResult<List<Kontakt>>> AddKontakt(KontaktAddVM obj)
        {
            if (obj != null)
            {
                Kontakt novi = new Kontakt
                {
                    BrojTelefona= obj.BrojTelefona,
                    EmailAdresa= obj.EmailAdresa,
                    Dostupan=obj.Dostupan,
                    DonatorId=obj.DonatorId
                };
                _context.Kontakti.Add(novi);
                await _context.SaveChangesAsync();
                return Ok(await _context.Kontakti.ToListAsync());
            }
            return BadRequest("Greska!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Kontakt>> UpdateKontakt(int id, KontaktAddVM obj)
        {
            var dbKontakt = await _context.Kontakti.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbKontakt == null)
                return BadRequest("Greska! - Kontakt nije pronadjen");

            dbKontakt.BrojTelefona = obj.BrojTelefona;
            dbKontakt.EmailAdresa = obj.EmailAdresa;
            dbKontakt.Dostupan = obj.Dostupan;
            //dbKontakt.DonatorId = obj.DonatorId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Kontakti.FindAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Kontakt>> DeleteKontakt(int id)
        {
            var dbKontakt = await _context.Kontakti.FirstOrDefaultAsync(x => x.Id == id);

            if (dbKontakt == null)
                return BadRequest("Greska! - Kontakt nije pronadjen");

            _context.Kontakti.Remove(dbKontakt);

            await _context.SaveChangesAsync();

            return Ok(dbKontakt);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Kontakt>> SoftDeleteKontakt(int id)
        {
            var dbKontakt = await _context.Kontakti.FirstOrDefaultAsync(x => x.Id == id);

            if (dbKontakt == null)
                return BadRequest("Greska! - Kontakt nije pronadjen");

            dbKontakt.isDeleted= true;

            await _context.SaveChangesAsync();

            return Ok(dbKontakt);
        }
    }
}
