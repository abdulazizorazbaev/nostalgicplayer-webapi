using FluentValidation;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Validators.DTOs.Musics;

public class FavoriteCreateValidator : AbstractValidator<FavoriteCreateDto>
{
    public FavoriteCreateValidator()
    {
        RuleFor(dto => dto.MusicId).NotNull().NotEmpty().WithMessage("MusicId field is required!");
        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 20 characters!");
    }
}