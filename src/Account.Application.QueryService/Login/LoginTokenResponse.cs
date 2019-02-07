namespace Account.Application.QueryService.Login
{
    public class LoginTokenResponse
    {
        public LoginTokenResponse(string username, string accessToken, string refreshToken)
        {
            Username = username;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        
        public string Username { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }
    }
}