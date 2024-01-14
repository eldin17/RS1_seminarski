namespace RS1seminarski.Modul1.Models
{
    public class Kategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string SlikaIkonice { get; set; }

        public List<Item> Item { get; set; }
    }
}
