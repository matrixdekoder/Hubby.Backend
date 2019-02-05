namespace Core.Infrastructure.Security
{
    public interface ITokenHandler
    {
        string Handle(string username);
    }
}