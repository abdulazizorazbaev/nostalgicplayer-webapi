namespace NostalgicPlayer.Domain.Exceptions.Musics;

public class MusicNotFoundException : NotFoundException
{
    public MusicNotFoundException()
    {
        this.TitleMessage = "Music not found!";
    }
}