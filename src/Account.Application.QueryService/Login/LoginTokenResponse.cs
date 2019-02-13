namespace Account.Application.QueryService.Login
{
    public class LoginTokenResponse
    {
        public LoginTokenResponse(string id, string accessToken)
        {
            Id = id;
            AccessToken = accessToken;
        }
        
        public string Id { get; }
        public string AccessToken { get; }
    }
}