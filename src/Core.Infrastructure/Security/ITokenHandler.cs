namespace Core.Infrastructure.Security
{
    public interface ITokenHandler
    {
        string Create(string username);
    }
}