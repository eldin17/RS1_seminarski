using System.Text.Json.Serialization;

namespace RS1seminarski.Modul1.Models
{
    public class Donacija
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string KratakOpis { get; set; }
        public string Status { get; set; } = "Aktivan";
        public DateTime DatumObjave { get; set; }

        [JsonIgnore]
        public Donator Donator { get; set; }
        public int DonatorId { get; set; }

        public List<Slika> Slike { get; set; }

        public Item Item { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
