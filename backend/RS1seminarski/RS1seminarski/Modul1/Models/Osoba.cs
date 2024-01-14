namespace RS1seminarski.Modul1.Models
{
    public class Osoba
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }

        public Donator Donator { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
