namespace NostalgicPlayer.Domain.Exceptions.Albums;

public class AlbumNotFoundException : NotFoundException
{
    public AlbumNotFoundException()
    {
        this.TitleMessage = "Album not found!";
    }
}