using FluentValidation;
using NostalgicPlayer.Service.DTOs.Auth;

namespace NostalgicPlayer.Service.Validators.DTOs.Auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
	public RegisterValidator()
	{
		RuleFor(dto => dto.FirstName)
			.NotNull().NotEmpty().WithMessage("Firstname is required!")
			.MaximumLength(30).WithMessage("Firstname must be less than 30 characters!");
		RuleFor(dto => dto.LastName)
            .NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MaximumLength(30).WithMessage("Lastname must be less than 30 characters!");
		RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
			.WithMessage("Phone number is invalid! ex (+998xxAAABBCC)");
		RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
			.WithMessage("Password is not strong password!");
    }
}