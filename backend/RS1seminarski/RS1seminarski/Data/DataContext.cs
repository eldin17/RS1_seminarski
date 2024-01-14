using Microsoft.EntityFrameworkCore;
using RS1seminarski.Modul1.Models;

namespace RS1seminarski.Data
{
    public class DataContext : DbContext
    {        
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Donacija> Donacije { get; set; }
        public DbSet<Donator> Donatori { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Kontakt> Kontakti { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<Slika> Slike { get; set; }



    }
}
