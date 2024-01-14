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
    public class KategorijaController : ControllerBase
    {
        private readonly DataContext _context;

        public KategorijaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kategorija>>> GetAll()
        {
            return Ok(await _context.Kategorije.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kategorija>> GetById(int id)
        {
            var dbKategorija = await _context.Kategorije.FindAsync(id);

            if (dbKategorija == null)
                return BadRequest("Greska! - Kategorija nije pronadjena");

            return Ok(dbKategorija);
        }

        [HttpPost]
        public async Task<ActionResult<List<Kategorija>>> AddKategorija(KategorijaAddVM obj)
        {
            if (obj != null)
            {
                Kategorija novi = new Kategorija
                {
                    Naziv = obj.Naziv,
                    Opis = obj.Opis,
                    SlikaIkonice = obj.SlikaIkonice
                };
                _context.Kategorije.Add(novi);
                await _context.SaveChangesAsync();
                return Ok(await _context.Kategorije.ToListAsync());
            }
            return BadRequest("Greska!");
        }

        [HttpPut]
        public async Task<ActionResult<Kategorija>> UpdateKategorija(int id, KategorijaAddVM obj)
        {
            var dbKategorija = await _context.Kategorije.FindAsync(id);

            if (dbKategorija == null)
                return BadRequest("Greska! - Kategorija nije pronadjena");

            dbKategorija.Naziv = obj.Naziv;
            dbKategorija.Opis = obj.Opis;
            dbKategorija.SlikaIkonice = obj.SlikaIkonice;

            await _context.SaveChangesAsync();

            return Ok(await _context.Kategorije.FindAsync(id));
        }

        [HttpDelete]
        public async Task<ActionResult<Kategorija>> DeleteKategorija(int id)
        {
            var dbKategorija = await _context.Kategorije.FindAsync(id);

            if (dbKategorija == null)
                return BadRequest("Greska! - Kategorija nije pronadjena");

            _context.Kategorije.Remove(dbKategorija);

            await _context.SaveChangesAsync();

            return Ok(dbKategorija);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Kategorija>> AddSlikaKategorije([FromForm] ImgSingleVM obj, int id)
        {
            try
            {
                Kategorija kategorija = _context.Kategorije.FirstOrDefault(k => k.Id == id);

                if (obj.vmSlika != null && kategorija != null)
                {
                    obj.vmSlika.CopyTo(new FileStream(Config.IkoniceKategorijaFolder + obj.vmSlika.FileName, FileMode.Create));
                    kategorija.SlikaIkonice = Config.IkoniceKategorijaUrl + obj.vmSlika.FileName;
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
