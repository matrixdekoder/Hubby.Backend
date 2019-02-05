namespace Core.Infrastructure.Security
{
    public interface IPasswordComputer
    {
        string Hash(string password);
        bool Compare(string inputPassword, string storedPassword);
    }
}
