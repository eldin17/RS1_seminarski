namespace RS1seminarski.Helper
{
    public class Config
    {
        public static string url = "https://localhost:7165/";  

        public static string SlikeDonatora => "SlikeDonatora/";
        public static string SlikeDonatoraUrl => url + SlikeDonatora;
        public static string SlikeDonatoraFolder => "wwwroot/" + SlikeDonatora;
        //------------------------------------------------------------------------------
        public static string SlikeDonacija => "SlikeDonacija/";
        public static string SlikeDonacijaUrl => url + SlikeDonacija;
        public static string SlikeDonacijaFolder => "wwwroot/" + SlikeDonacija;
        //------------------------------------------------------------------------------
        public static string IkoniceKategorija => "IkoniceKategorija/";
        public static string IkoniceKategorijaUrl => url + IkoniceKategorija;
        public static string IkoniceKategorijaFolder => "wwwroot/" + IkoniceKategorija;
    }
}
