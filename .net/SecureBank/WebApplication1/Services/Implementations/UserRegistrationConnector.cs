using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Interfaces;
using WebApplication1.Validators;

namespace WebApplication1.Services.Implementations
{
    public class UserRegistrationConnector : IUserRegistrationConnector
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly RegisterRequestDtoValidator _validator;
        private readonly IPasswordHash _passwordHasher;
        public UserRegistrationConnector(IUserRegistrationRepository userRegistrationRepository, RegisterRequestDtoValidator validator, IPasswordHash passwordHasher)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }
        public async Task<RegisterResultDto> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAsync(request, cancellationToken);
            var email = request.Email.Trim();
            var normalizedEmail = email.ToUpperInvariant();
            var isEmailExists = await _userRegistrationRepository.EmailExistsAsync(normalizedEmail, cancellationToken);
            if (isEmailExists)
            {
                throw new ValidationException("Email already exists");
            }
            var hashedPassword= _passwordHasher.HashPassword(request.Password);
            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = email,
                EmailNormalized = normalizedEmail,
                PhoneNumber = request.PhoneNumber.Trim(),
                PasswordHash = hashedPassword,
                UserStatus = "Pending",
                EmailVerified = false,
                PhoneVerified = false,
                FailedLoginAttempts = 0,
                CreatedAtUtc = DateTime.UtcNow
            };
            var saveUser = await _userRegistrationRepository.AddAsync(user, cancellationToken);
            if (saveUser == null)
            {
                throw new ValidationException("User xcreation failed");
            }

            var response = new RegisterResponseDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserStatus = user.UserStatus,
                CreatedAtUtc = user.CreatedAtUtc
            };

            return new RegisterResultDto
            {
                Succeeded = true,
                Message = "Registration successful",
                User = response
            };

        }
    }
}
