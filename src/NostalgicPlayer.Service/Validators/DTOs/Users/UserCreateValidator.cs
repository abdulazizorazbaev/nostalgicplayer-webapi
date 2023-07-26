using FluentValidation;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Users;

namespace NostalgicPlayer.Service.Validators.DTOs.Users;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(dto => dto.FirstName)
            .NotNull().NotEmpty().WithMessage("FirstName field is required!")
            .MinimumLength(5).WithMessage("FirstName must be more than 5 characters!")
            .MaximumLength(50).WithMessage("FirstName must be less than 50 characters!");

        RuleFor(dto => dto.LastName)
            .NotNull().NotEmpty().WithMessage("LastName field is required!")
            .MinimumLength(5).WithMessage("LastName must be more than 5 characters!")
            .MaximumLength(50).WithMessage("LastName must be less than 50 characters!");

        int maxImageSize = 3;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required!");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSize * 1024 * 1024).WithMessage("Image size is exceeded!");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not allowed!");
    }
}