using System.Text.Json.Serialization;

namespace RS1seminarski.Modul1.Models
{
    public class Slika
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Putanja { get; set; }

        [JsonIgnore]
        public Donacija Donacija { get; set; }
        public int DonacijaId { get; set; }
    }
}
