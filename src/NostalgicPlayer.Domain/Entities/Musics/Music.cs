using System.ComponentModel.DataAnnotations;

namespace NostalgicPlayer.Domain.Entities.Musics;

public class Music : Auditable
{
    public long GenreId { get; set; }

    public long SingerId { get; set; }

    public string MusicName { get; set; } = String.Empty;

    [MaxLength(5)]
    public string Duration { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public string Mp3Path { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}