namespace NostalgicPlayer.Domain.Entities.Musics.Downloads;

public class Download : BaseEntity
{
    public long MusicId { get; set; }

    public long UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}