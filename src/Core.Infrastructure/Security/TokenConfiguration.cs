namespace Core.Infrastructure.Security
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
    }
}