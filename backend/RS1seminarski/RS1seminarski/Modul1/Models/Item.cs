using System.Text.Json.Serialization;

namespace RS1seminarski.Modul1.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public string Proizvodjac { get; set; }

        [JsonIgnore]
        public Donacija Donacija { get; set; }
        public int DonacijaId { get; set; }

        [JsonIgnore]
        public Kategorija Kategorija { get; set; }
        public int KategorijaId { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
