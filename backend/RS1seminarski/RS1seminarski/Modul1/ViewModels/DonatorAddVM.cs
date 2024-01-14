using RS1seminarski.Helper;

namespace RS1seminarski.Modul1.ViewModels
{
    public class DonatorAddVM
    {
        public string StatusDonatora { get; set; } = "Aktivan";
        public int BrojDonacija { get; set; } = 0;
        public int BrojNarudzbi { get; set; } = 0;
        public int BrojZvjezdica { get; set; } = 0;
        public string SlikaDonatora { get; set; } = Config.SlikeDonatoraUrl + "default.png";
        public int KorisnickiNalogId { get; set; } = 0;
        public int OsobaId { get; set; } = 0;
    }
}
