using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1seminarski.Data;
using RS1seminarski.Helpers;
using RS1seminarski.Modul1.Models;
using RS1seminarski.Modul1.ViewModels;

namespace RS1seminarski.Modul1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OsobaController : ControllerBase
    {
        private readonly DataContext _context;

        public OsobaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Osoba>>> GetAll()
        {
            return Ok(await _context.Osobe
                .Include(x => x.Donator)
                .ThenInclude(x => x.Kontakt)
                .Where(x=>x.isDeleted == false)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Osoba>>> GetAllDonatori(string? ime_prezime, int items_per_page, int page_number = 1)
        {
            var data = await _context.Osobe
                .Include(x => x.Donator)
                .ThenInclude(x=>x.Kontakt)
                .Where(x=> ime_prezime == null || (x.Ime + " " + x.Prezime).StartsWith(ime_prezime) ||
                (x.Prezime + " " + x.Ime).StartsWith(ime_prezime))
                .Where(x => x.Donator != null && x.Donator.Kontakt!=null && x.isDeleted == false).OrderByDescending(x => x.Id)
                .ToListAsync();

            if (data == null)
                return BadRequest("Nema podataka");

            return Ok(Pagination<Osoba>.Paginate(data, page_number, items_per_page));
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<Osoba>> GetById(int id)
        {
            var dbOsoba = await _context.Osobe
                .Where(x=>x.Donator!=null&&x.isDeleted==false)
                .Include(x => x.Donator)
                .ThenInclude(x => x.Kontakt)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbOsoba == null)
                return BadRequest("Greska! - Osoba nije pronadjena");

            return Ok(dbOsoba);
        }

        [HttpPost]
        public async Task<ActionResult<Osoba>> AddOsoba(OsobaAddVM obj)
        {
            if (obj != null)
            {
                Osoba novi = new Osoba
                {
                    Ime= obj.Ime,   
                    Prezime= obj.Prezime,
                    DatumRodjenja= obj.DatumRodjenja
                };
                _context.Osobe.Add(novi);
                await _context.SaveChangesAsync();
                return Ok(novi);
            }
            return BadRequest("Greska!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Osoba>> UpdateOsoba(int id, OsobaAddVM obj)
        {
            var dbOsoba = await _context.Osobe.Where(x=>x.isDeleted == false).FirstOrDefaultAsync(x=>x.Id==id);

            if (dbOsoba == null)
                return BadRequest("Greska! - Osoba nije pronadjena");

            dbOsoba.Ime = obj.Ime;
            dbOsoba.Prezime = obj.Prezime;
            dbOsoba.DatumRodjenja = obj.DatumRodjenja;

            await _context.SaveChangesAsync();

            return Ok(await _context.Osobe.FindAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Osoba>> DeleteOsoba(int id)
        {
            var dbOsoba = await _context.Osobe.FirstOrDefaultAsync(x => x.Id == id);

            if (dbOsoba == null)
                return BadRequest("Greska! - Osoba nije pronadjena");

            _context.Osobe.Remove(dbOsoba);

            await _context.SaveChangesAsync();

            return Ok(dbOsoba);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Osoba>> SoftDeleteOsoba(int id)
        {
            var dbOsoba = await _context.Osobe.FirstOrDefaultAsync(x => x.Id == id);

            if (dbOsoba == null)
                return BadRequest("Greska! - Osoba nije pronadjena");

            dbOsoba.isDeleted= true;

            await _context.SaveChangesAsync();

            return Ok(dbOsoba);
        }
    }
}
