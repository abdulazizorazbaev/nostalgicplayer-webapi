using FluentValidation;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Validators.DTOs.Musics;

public class PlayCreateValidator : AbstractValidator<PlayCreateDto>
{
    public PlayCreateValidator()
    {
        RuleFor(dto => dto.MusicId).NotNull().NotEmpty().WithMessage("MusicId field is required!");
    }
}