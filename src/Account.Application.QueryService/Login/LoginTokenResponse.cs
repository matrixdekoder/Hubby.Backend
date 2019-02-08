namespace Account.Application.QueryService.Login
{
    public class LoginTokenResponse
    {
        public LoginTokenResponse(string username, string accessToken)
        {
            Username = username;
            AccessToken = accessToken;
        }
        
        public string Username { get; }
        public string AccessToken { get; }
    }
}