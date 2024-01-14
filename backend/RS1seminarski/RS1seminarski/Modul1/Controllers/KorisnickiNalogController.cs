using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RS1seminarski.Data;
using RS1seminarski.Modul1.Models;
using RS1seminarski.Modul1.ViewModels;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RS1seminarski.Modul1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorisnickiNalogController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public KorisnickiNalogController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<KorisnickiNalog>>> GetAll()
        {
            return Ok(await _context.KorisnickiNalozi.Where(x=>x.isDeleted==false).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KorisnickiNalog>> GetById(int id)
        {
            var dbNalog = await _context.KorisnickiNalozi.Where(x=>x.isDeleted==false).Include(x=>x.Donator).ThenInclude(x=>x.Kontakt).FirstOrDefaultAsync(x=>x.Id==id);

            if (dbNalog == null)
                return BadRequest("Greska! - Korisnicki nalog nije pronadjen");

            return Ok(dbNalog);
        }

        [HttpPost]
        public async Task<ActionResult<KorisnickiNalog>> Register(KorisnickiAddVM obj)
        {
            CreatePasswordHash(obj.Password, out byte[] pwHash, out byte[] pwSalt);

            var novi = new KorisnickiNalog()
            {
                Username = obj.Username,
                PasswordHash = pwHash,
                PasswordSalt = pwSalt,
                Uloga = obj.Uloga
            };

            _context.KorisnickiNalozi.Add(novi);
            await _context.SaveChangesAsync();

            return Ok(novi);
        }

        private void CreatePasswordHash(string pw, out byte[] pwHash, out byte[] pwSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                pwSalt = hmac.Key;
                pwHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pw));
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseVM>> Login(LoginVM obj)
        {
            if(obj==null||obj.Username==""||obj.Password=="" || obj.Username == null || obj.Password == null)
            {
                return BadRequest("No input");
            }
            var izbaze = _context.KorisnickiNalozi.Where(x=>x.isDeleted==false).Single(x => x.Username == obj.Username);
            if (izbaze.Username != obj.Username)
            {
                return BadRequest("Korisničko ime ili lozinka nisu ispravni!");
            }

            if (!VerifyPasswordHash(obj.Password, izbaze.PasswordHash, izbaze.PasswordSalt))
            {
                return BadRequest("Korisničko ime ili lozinka nisu ispravni!");
            }

            string token = CreateToken(izbaze);

            var logiraniKorisnik = new LoginResponseVM
            {
                Token = token,
                IdLogiranogKorisnika = izbaze.Id,
                Uloga = izbaze.Uloga,
            };

            return Ok(logiraniKorisnik);
        }

        private bool VerifyPasswordHash(string pw, byte[] pwHash, byte[] pwSalt)
        {
            using (var hmac = new HMACSHA512(pwSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pw));
                return computedHash.SequenceEqual(pwHash);
            }
        }

        private string CreateToken(KorisnickiNalog user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Uloga)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult<string> TestPristupAdmin()
        {
            return Ok("Pristup odobren za ulogu Admin");

        }
        [HttpGet, Authorize(Roles = "Donator")]
        public ActionResult<string> TestPristupDonator()
        {
            return Ok("Pristup odobren za ulogu Donator");

        }

        [HttpPut]
        public async Task<ActionResult<KorisnickiNalog>> UpdateNalog(int id, KorisnickiAddVM obj)
        {
            var dbNalog = await _context.KorisnickiNalozi.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbNalog == null)
                return BadRequest("Greska! - Korisnicki nalog nije pronadjen");

            CreatePasswordHash(obj.Password, out byte[] pwHash, out byte[] pwSalt);

            dbNalog.Username = obj.Username;
            dbNalog.PasswordHash = pwHash;
            dbNalog.PasswordSalt = pwSalt;
            dbNalog.Uloga = obj.Uloga;
        
            await _context.SaveChangesAsync();
        
            return Ok(await _context.KorisnickiNalozi.FindAsync(id));            
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<KorisnickiNalog>> DeleteNalog(int id)
        {
            var dbNalog = await _context.KorisnickiNalozi.FirstOrDefaultAsync(x => x.Id == id);

            if (dbNalog == null)
                return BadRequest("Greska! - Korisnicki nalog nije pronadjen");

            _context.KorisnickiNalozi.Remove(dbNalog);

            await _context.SaveChangesAsync();

            return Ok(dbNalog);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<KorisnickiNalog>> SoftDeleteNalog(int id)
        {
            var dbNalog = await _context.KorisnickiNalozi.FirstOrDefaultAsync(x => x.Id == id);

            if (dbNalog == null)
                return BadRequest("Greska! - Korisnicki nalog nije pronadjen");

            dbNalog.isDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(dbNalog);
        }
        
    }
}
