using FluentValidation;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Validators.DTOs.Musics;

public class DownloadCreateValidator : AbstractValidator<DownloadCreateDto>
{
    public DownloadCreateValidator()
    {
        RuleFor(dto => dto.MusicId).NotNull().NotEmpty().WithMessage("MusicId field is required!");
        RuleFor(dto => dto.UserId).NotNull().NotEmpty().WithMessage("UserId field is required!");
    }
}