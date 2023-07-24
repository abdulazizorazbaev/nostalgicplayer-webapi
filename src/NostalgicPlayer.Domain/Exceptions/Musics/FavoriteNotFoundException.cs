namespace NostalgicPlayer.Domain.Exceptions.Musics;

public class FavoriteNotFoundException : NotFoundException
{
    public FavoriteNotFoundException()
    {
        TitleMessage = "Favorite music not found!";
    }
}