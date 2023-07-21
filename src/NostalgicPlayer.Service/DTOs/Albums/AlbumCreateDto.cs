using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.Service.DTOs.Albums;

public class AlbumCreateDto
{
    public long MusicId { get; set; }

    public long SingerId { get; set; }

    public string AlbumName { get; set; } = String.Empty;

    public int Year { get; set; }

    public IFormFile ImagePath { get; set; } = default!;

    public string Description { get; set; } = String.Empty;
}