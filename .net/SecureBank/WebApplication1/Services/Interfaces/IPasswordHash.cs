namespace WebApplication1.Services.Interfaces
{
    public interface IPasswordHash
    {
        string HashPassword(string password);
    }
}
