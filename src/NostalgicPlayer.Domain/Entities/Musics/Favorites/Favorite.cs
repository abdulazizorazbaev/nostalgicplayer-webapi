namespace NostalgicPlayer.Domain.Entities.Musics.Favorites;

public class Favorite : BaseEntity
{
    public long MusicId { get; set; }

    public string Description { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }
}