namespace Account.Application.QueryService.Login
{
    public class LoginTokenResponse
    {
        public LoginTokenResponse(string username, string token)
        {
            Username = username;
            Token = token;
        }
        
        public string Username { get; }
        public string Token { get; }
    }
}