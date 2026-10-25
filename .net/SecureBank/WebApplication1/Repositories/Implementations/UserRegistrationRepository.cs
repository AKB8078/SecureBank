using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories.Implementations
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly SecureBankDbContext _dbContext;
        public UserRegistrationRepository(SecureBankDbContext secureBankDbContext)
        {
            _dbContext = secureBankDbContext;
        }
        public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            var isEmailExist = await _dbContext.Users.AsNoTracking().AnyAsync(u => u.EmailNormalized == email , cancellationToken);
            return isEmailExist;
        }
    }
}
