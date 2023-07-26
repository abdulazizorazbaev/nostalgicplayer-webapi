namespace NostalgicPlayer.Domain.Exceptions.Musics;

public class DownloadNotFoundException : NotFoundException
{
    public DownloadNotFoundException()
    {
        TitleMessage = "Download not found!";
    }
}