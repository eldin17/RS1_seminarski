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
    public class ItemController : ControllerBase
    {
        private readonly DataContext _context;

        public ItemController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            return Ok(await _context.Items.Where(x => x.isDeleted == false).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            var dbItem = await _context.Items.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbItem == null)
                return BadRequest("Greska! - Item nije pronadjen");

            return Ok(dbItem);
        }

        [HttpPost]
        public async Task<ActionResult<List<Item>>> AddItem(ItemAddVM obj)
        {
            if (obj != null)
            {
                Item novi = new Item
                {
                    Naziv=obj.Naziv,
                    Kolicina=obj.Kolicina,
                    Proizvodjac=obj.Proizvodjac,
                    DonacijaId=obj.DonacijaId,
                    KategorijaId=obj.KategorijaId
                };
                _context.Items.Add(novi);
                await _context.SaveChangesAsync();
                return Ok(await _context.Items.ToListAsync());
            }
            return BadRequest("Greska!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> UpdateItem(int id, ItemAddVM obj)
        {
            var dbItem = await _context.Items.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbItem == null)
                return BadRequest("Greska! - Item nije pronadjen");

            dbItem.Naziv = obj.Naziv;
            dbItem.Kolicina = obj.Kolicina;
            dbItem.Proizvodjac = obj.Proizvodjac;
            //dbItem.DonacijaId = obj.DonacijaId;
            dbItem.KategorijaId = obj.KategorijaId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Items.FindAsync(id));
        }

        [HttpDelete]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var dbItem = await _context.Items.FindAsync(id);

            if (dbItem == null)
                return BadRequest("Greska! - Item nije pronadjen");

            _context.Items.Remove(dbItem);

            await _context.SaveChangesAsync();

            return Ok(dbItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> SoftDeleteItem(int id)
        {
            var dbItem = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (dbItem == null)
                return BadRequest("Greska! - Item nije pronadjen");

            dbItem.isDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(dbItem);            
        }
    }
}
