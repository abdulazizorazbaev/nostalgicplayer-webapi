namespace NostalgicPlayer.Domain.Exceptions.Singers;

public class SingerNotFoundException : NotFoundException
{
    public SingerNotFoundException()
    {
        this.TitleMessage = "Singer not found!";
    }
}