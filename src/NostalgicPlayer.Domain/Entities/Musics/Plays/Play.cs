namespace NostalgicPlayer.Domain.Entities.Musics.Plays;

public class Play : BaseEntity
{
    public long MusicId { get; set; }

    public DateTime CreatedAt { get; set; }
}