using FluentValidation;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Genres;

namespace NostalgicPlayer.Service.Validators.DTOs.Genres;

public class GenreUpdateValidator : AbstractValidator<GenreUpdateDto>
{
    public GenreUpdateValidator()
    {
        RuleFor(dto => dto.GenreName)
            .NotNull().NotEmpty().WithMessage("Genre name field is required!")
            .MinimumLength(5).WithMessage("Genre name must be more than 5 characters!")
            .MaximumLength(50).WithMessage("Genre name must be less than 50 characters!");

        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 20 characters!");

        When(dto => dto.ImagePath is not null, () =>
        {
            int maxImageSize = 3;
            RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSize * 1024 * 1024).WithMessage("Image size is exceeded!");
            RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not allowed!");
        });
    }
}