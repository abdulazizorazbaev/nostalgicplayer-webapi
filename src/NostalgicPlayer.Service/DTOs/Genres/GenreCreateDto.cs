using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.Service.DTOs.Genres;

public class GenreCreateDto
{
    public string GenreName { get; set; } = String.Empty;

    public IFormFile ImagePath { get; set; } = default!;

    public string Description { get; set; } = String.Empty;
}