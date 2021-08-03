namespace CanedoLab.MS.Identity.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpirationHours { get; set; }
    }
}
