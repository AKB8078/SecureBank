using FluentValidation;
using WebApplication1.DTOs;

namespace WebApplication1.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x=>x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("FirstName is required")
                .MaximumLength(50).WithMessage("Maximum 50 characters is allowed");
            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("LastName is required")
                .MaximumLength(50).WithMessage("Maximum 50 characters is allowed");
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email is required")
                .MaximumLength(100).WithMessage("Maximum characters allowed is 100")
                .EmailAddress().WithMessage("Valid Email is required");
            RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(20).WithMessage("Maximum length allowed is 20").Matches(@"^\+?[1-9]\d{7,14}$")
                .WithMessage("Please enter a valid phone number");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(12).WithMessage("Minimum length required is 12")
                .MaximumLength(15).WithMessage("Maximum length allowed is 15")
                .Matches("[A-Z]").WithMessage("Atleaast one uppercase letter is required")
                .Matches("[a-z]").WithMessage("Atleaast one lowercase letter is required")
                .Matches("[0-9]").WithMessage("Atleaast one number is required");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirem Password is required")
                .Equal(x => x.Password).WithMessage("Passwords and confirm password must be same");
        }
    }
}
