namespace RS1seminarski.Modul1.Models
{
    public class KorisnickiNalog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Uloga { get; set; }

        public Donator Donator { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
