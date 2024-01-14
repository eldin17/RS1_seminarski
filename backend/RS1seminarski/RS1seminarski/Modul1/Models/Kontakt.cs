using System.Text.Json.Serialization;

namespace RS1seminarski.Modul1.Models
{
    public class Kontakt
    {
        public int Id { get; set; }
        public string BrojTelefona { get; set; }
        public string EmailAdresa { get; set; } = "cristina.hilpert@ethereal.email";
        public bool Dostupan { get; set; }

        [JsonIgnore]
        public Donator Donator { get; set; }
        public int DonatorId { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
