using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.Service.DTOs.Musics;

public class MusicCreateDto
{
    public long SingerId { get; set; }

    public long GenreId { get; set; }

    public string MusicName { get; set; } = String.Empty;

    public string Duration { get; set; } = String.Empty;

    public IFormFile ImagePath { get; set; } = default!;

    public IFormFile Mp3Path { get; set; } = default!;

    public string Description { get; set; } = String.Empty;
}