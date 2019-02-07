namespace Core.Infrastructure.Security
{
    public class TokenModel
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}