using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1seminarski.Data;
using RS1seminarski.Helper;
using RS1seminarski.Helpers;
using RS1seminarski.Modul1.Models;
using RS1seminarski.Modul1.ViewModels;

namespace RS1seminarski.Modul1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DonacijaController : ControllerBase
    {
        private readonly DataContext _context;

        public DonacijaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Donacija>>> GetAll()
        {
            return Ok(await _context.Donacije.Where(x => x.isDeleted == false).Include(x=>x.Slike).Include(x=>x.Item).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pagination<Donacija>>> GetAllMoje(int id)
        {
            return Ok(await _context.Donacije.Where(x=>x.DonatorId==id && x.isDeleted == false).Include(x => x.Slike).OrderByDescending(x => x.Id).Include(x => x.Item).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Donacija>>> GetAllDonacije(string? Naslov, int items_per_page, int page_number = 1)
        {
            var data = await _context.Donacije
                .Include(x => x.Slike).Include(x => x.Item)
                .Where(x => Naslov == null || x.Naslov.StartsWith(Naslov))
                .Where(x => x != null && x.isDeleted == false).OrderByDescending(x=>x.Id)
                .ToListAsync();

            if (data == null)
                return BadRequest("Nema podataka");
            
            return Ok(Pagination<Donacija>.Paginate(data, page_number, items_per_page));
        }      



        [HttpGet]
        public async Task<ActionResult<Donacija>> GetById(int id)
        {
            var dbDonacija = await _context.Donacije.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbDonacija == null)
                return BadRequest("Greska! - Donacija nije pronadjena");

            return Ok(dbDonacija);
        }

        [HttpPost]
        public async Task<ActionResult<Donacija>> AddDonacija(DonacijaAddVM obj)
        {
            if (obj != null)
            {
                Donacija novi = new Donacija
                {
                    Naslov=obj.Naslov,
                    KratakOpis=obj.KratakOpis,
                    Status=obj.Status,
                    DatumObjave=obj.DatumObjave,
                    DonatorId=obj.DonatorId
                };
                _context.Donacije.Add(novi);
                await _context.SaveChangesAsync();
               


                return Ok(novi);
            }
            return BadRequest("Greska!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Donacija>> UpdateDonacija(int id, DonacijaAddVM obj)
        {
            var dbDonacija = await _context.Donacije.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbDonacija == null)
                return BadRequest("Greska! - Donacija nije pronadjena");

            dbDonacija.Naslov = obj.Naslov;
            dbDonacija.KratakOpis = obj.KratakOpis;
            dbDonacija.Status = obj.Status;
            dbDonacija.DatumObjave = obj.DatumObjave;
            //dbDonacija.DonatorId = obj.DonatorId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Donacije.FindAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Donacija>> DeleteDonacija(int id)
        {
            var dbDonacija = await _context.Donacije.FirstOrDefaultAsync(x => x.Id == id);

            if (dbDonacija == null)
                return BadRequest("Greska! - Donacija nije pronadjena");

            _context.Donacije.Remove(dbDonacija);

            await _context.SaveChangesAsync();

            return Ok(dbDonacija);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Donacija>> SoftDeleteDonacija(int id)
        {
            var dbDonacija = await _context.Donacije.FirstOrDefaultAsync(x => x.Id == id);

            if (dbDonacija == null)
                return BadRequest("Greska! - Donacija nije pronadjena");

            dbDonacija.isDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(dbDonacija);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddSlikeDonacije(int id,[FromForm] ImgMultipleVM obj )
        {

            if (obj == null)
            {
                return Content("Pogresan unos!");
            }

            var stareSlike = _context.Slike.Where(x => x.DonacijaId == id).ToList();
            if (stareSlike.Count > 0)
            {
                _context.Slike.RemoveRange(stareSlike);
                _context.SaveChanges();
                stareSlike = null;
            }

            foreach (var item in obj.vmSlike)
            {

                if (item.FileName == null || item.FileName.Length == 0)
                {
                    return Content("Pogresan odabir!");
                }
                var path = Path.Combine(Config.SlikeDonacijaFolder, item.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                    stream.Close();
                }

                var nova = new Slika
                {
                    DonacijaId = id,
                    Naziv = item.FileName,
                    Putanja = Config.SlikeDonacijaUrl + item.FileName
                };

                _context.Add(nova);
                await _context.SaveChangesAsync();
            }

            return Ok(obj);
        }

        [HttpGet("{itemsPerPage}/{page}")]
        public async Task<ActionResult<Donacija>> GetPagedDonacija(int itemsPerPage, int page)//mozda dodati id donatora kao filter ... Where(x=>idDonatora==null||x.idDonatora==idDonatora) i orderbydescending da prikaze najnovije prvo
        {
            var dbDonacija = await _context.Donacije.Where(x => x.isDeleted == false).ToListAsync();

            if (dbDonacija == null)
                return BadRequest("Greska! - Donacija nije pronadjena");            

            return Ok(Pagination<Donacija>.Paginate(dbDonacija, page, itemsPerPage));
        }

    }
}
