namespace NostalgicPlayer.Domain.Entities.Singers;

public class Singer : Human
{
    public string Bio { get; set; } = String.Empty;

    public string FacebookAcc { get; set; } = String.Empty;

    public string InstagramAcc { get; set; } = String.Empty;

    public string YoutubeLink { get; set; } = String.Empty;
}