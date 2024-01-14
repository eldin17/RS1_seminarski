using System.Text.Json.Serialization;

namespace RS1seminarski.Modul1.Models
{
    public class Donator
    {
        public int Id { get; set; }
        public string StatusDonatora { get; set; } = "Aktivan";
        public int BrojDonacija { get; set; } = 0;
        public int BrojNarudzbi { get; set; } = 0;
        public int BrojZvjezdica { get; set; } = 0;
        public string SlikaDonatora { get; set; } = string.Empty;

        [JsonIgnore]
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public int KorisnickiNalogId { get; set; }

        [JsonIgnore]
        public Osoba Osoba { get; set; }
        public int OsobaId { get; set; }

        public Kontakt Kontakt { get; set; }

        public List<Donacija> Donacije { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
