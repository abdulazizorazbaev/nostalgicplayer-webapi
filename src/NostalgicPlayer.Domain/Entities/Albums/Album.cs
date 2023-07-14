namespace NostalgicPlayer.Domain.Entities.Albums;

public class Album : Auditable
{
    public long MusicId { get; set; }

    public long SingerId { get; set; }

    public string AlbumName { get; set; } = String.Empty;

    public int Year { get; set; }

    public string ImagePath { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}