namespace Account.Application.QueryService.Login
{
    public class LoginQueryResponse
    {
        public LoginQueryResponse(string id, string buddyId, string accessToken)
        {
            Id = id;
            BuddyId = buddyId;
            AccessToken = accessToken;
        }
        
        public string Id { get; }
        public string BuddyId { get; }
        public string AccessToken { get; }
    }
}