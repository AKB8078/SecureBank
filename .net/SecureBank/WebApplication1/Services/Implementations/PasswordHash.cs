using System.Security.Cryptography;
using System.Text;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Implementations
{
    public class PasswordHash : IPasswordHash
    {
        public PasswordHash()
        {
            
        }
        public string HashPassword(string password)
        {
            byte [] PasswordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(PasswordBytes);
            string hashedPasseord = Convert.ToHexString(hashBytes);
            return hashedPasseord;
        }
    }
}
