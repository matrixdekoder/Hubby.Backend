namespace Account.Application.QueryService.Login
{
    public class LoginQueryResponse
    {
        public LoginQueryResponse(string id, string accessToken)
        {
            Id = id;
            AccessToken = accessToken;
        }
        
        public string Id { get; }
        public string AccessToken { get; }
    }
}