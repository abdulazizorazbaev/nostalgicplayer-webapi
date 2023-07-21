using FluentValidation;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Albums;

namespace NostalgicPlayer.Service.Validators.DTOs.Albums;

public class AlbumUpdateValidator : AbstractValidator<AlbumUpdateDto>
{
	public AlbumUpdateValidator()
	{
        RuleFor(dto => dto.MusicId).NotNull().NotEmpty().WithMessage("MusicId field is required!");
        RuleFor(dto => dto.SingerId).NotNull().NotEmpty().WithMessage("SingerId field is required!");
        RuleFor(dto => dto.AlbumName)
            .NotNull().NotEmpty().WithMessage("Album name field is required!")
            .MinimumLength(5).WithMessage("Album name must be more than 5 characters!")
            .MaximumLength(50).WithMessage("Album name must be less than 50 characters!");
        RuleFor(dto => dto.Year)
            .NotNull().NotEmpty().WithMessage("Year field is required! ex (XXXX)");

        int maxImageSize = 3;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required!");
        RuleFor(dto => dto.ImagePath!.Length).LessThan(maxImageSize * 1024 * 1024 + 1).WithMessage("Image size is exceeded!");
        RuleFor(dto => dto.ImagePath!.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not allowed!");

        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 20 characters!");
    }
}