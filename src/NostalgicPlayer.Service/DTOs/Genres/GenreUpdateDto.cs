using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.Service.DTOs.Genres;

public class GenreUpdateDto
{
    public string GenreName { get; set; } = String.Empty;

    public IFormFile ImagePath { get; set; } = default!;

    public string Description { get; set; } = String.Empty;
}