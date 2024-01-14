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
    public class SlikaController : ControllerBase
    {
        private readonly DataContext _context;

        public SlikaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Slika>>> GetAll()
        {
            return Ok(await _context.Slike.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Slika>> GetById(int id)
        {
            var dbSlika = await _context.Slike.FindAsync(id);

            if (dbSlika == null)
                return BadRequest("Greska! - Slika nije pronadjena");

            return Ok(dbSlika);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Slika>> GetByDonacijaId(int id)
        {
            var dbSlika = await _context.Slike.FirstOrDefaultAsync(x => x.DonacijaId == id);

            if (dbSlika == null)
                return BadRequest("Greska! - Slike nisu pronadjene");

            return Ok(dbSlika);
        }

        [HttpPost]
        public async Task<ActionResult<List<Slika>>> AddSlika(SlikaAddVM obj)
        {
            if (obj != null)
            {
                Slika novi = new Slika
                {
                    Naziv = obj.Naziv,
                    Putanja = obj.Putanja,
                    DonacijaId = obj.DonacijaId
                };
                _context.Slike.Add(novi);
                await _context.SaveChangesAsync();
                return Ok(await _context.Slike.ToListAsync());
            }
            return BadRequest("Greska!");
        }

        [HttpPut]
        public async Task<ActionResult<Slika>> UpdateSlika(int id, SlikaAddVM obj)
        {
            var dbSlika = await _context.Slike.FindAsync(id);

            if (dbSlika == null)
                return BadRequest("Greska! - Slika nije pronadjena");

            dbSlika.Naziv = obj.Naziv;
            dbSlika.Putanja = obj.Putanja;
            dbSlika.DonacijaId = obj.DonacijaId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Slike.FindAsync(id));
        }

        [HttpDelete]
        public async Task<ActionResult<Slika>> DeleteSlika(int id)
        {
            var dbSlika = await _context.Slike.FindAsync(id);

            if (dbSlika == null)
                return BadRequest("Greska! - Slika nije pronadjena");

            _context.Slike.Remove(dbSlika);

            await _context.SaveChangesAsync();

            return Ok(dbSlika);
        }
    }
}
