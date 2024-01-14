namespace RS1seminarski.Modul1.ViewModels
{
    public class DonacijaAddVM
    {
        public string Naslov { get; set; }
        public string KratakOpis { get; set; }
        public string Status { get; set; } = "Aktivan";
        public DateTime DatumObjave { get; set; } = DateTime.UtcNow;
        public int DonatorId { get; set; }

    }
}
