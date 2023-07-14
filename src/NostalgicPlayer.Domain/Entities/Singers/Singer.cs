namespace NostalgicPlayer.Domain.Entities.Singers;

public class Singer : Human
{
    public string Biography { get; set; } = String.Empty;

    public string FacebookAccount { get; set; } = String.Empty;

    public string InstagramAccount { get; set; } = String.Empty;

    public string YoutubeLink { get; set; } = String.Empty;
}