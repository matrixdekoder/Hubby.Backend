namespace Core.Infrastructure.Security
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public int AccessExpirationSeconds { get; set; }
        public int RefreshExpirationSeconds { get; set; }
    }
}