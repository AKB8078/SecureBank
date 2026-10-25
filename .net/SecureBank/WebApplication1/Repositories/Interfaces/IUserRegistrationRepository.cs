using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IUserRegistration
    {
        Task<bool>EmailExistsAsync(string email,CancellationToken cancellationToken = default);
        Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
    }
}
