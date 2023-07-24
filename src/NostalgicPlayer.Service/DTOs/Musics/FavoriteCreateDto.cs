namespace NostalgicPlayer.Service.DTOs.Musics;

public class FavoriteCreateDto
{
    public long MusicId { get; set; }

    public string Description { get; set; } = String.Empty;
}