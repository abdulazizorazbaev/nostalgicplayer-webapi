using FluentValidation;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Validators.DTOs.Musics;

public class MusicCreateValidator : AbstractValidator<MusicCreateDto>
{
    public MusicCreateValidator()
    {
        RuleFor(dto => dto.GenreId).NotNull().NotEmpty().WithMessage("GenreId field is required!");
        RuleFor(dto => dto.SingerId).NotNull().NotEmpty().WithMessage("SingerId field is required!");
        RuleFor(dto => dto.MusicName)
            .NotNull().NotEmpty().WithMessage("Music name field is required!")
            .MinimumLength(5).WithMessage("Music name must be more than 5 characters!")
            .MaximumLength(50).WithMessage("Music name must be less than 50 characters!");
        RuleFor(dto => dto.Duration)
            .NotNull().NotEmpty().WithMessage("Duration field is required!")
            .MinimumLength(3).WithMessage("Duration must be more than 3 characters!")
            .MaximumLength(4).WithMessage("Duration must be less than 4 characters!");

        int maxImageSize = 3;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required!");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSize * 1024 * 1024 + 1).WithMessage("Image size is exceeded!");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not allowed!");

        int maxMp3Size = 20;
        RuleFor(dto => dto.Mp3Path).NotEmpty().NotNull().WithMessage("Mp3 field is required!");
        RuleFor(dto => dto.Mp3Path.Length).LessThan(maxMp3Size * 1024 * 1024 + 1).WithMessage("Mp3 size is exceeded!");
        RuleFor(dto => dto.Mp3Path.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetMp3Extensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not allowed!");

        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 20 characters!");
    }
}