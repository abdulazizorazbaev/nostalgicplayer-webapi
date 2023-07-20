namespace NostalgicPlayer.Domain.Exceptions.Files;

public class Mp3NotFoundException : NotFoundException
{
    public Mp3NotFoundException()
    {
        TitleMessage = "Mp3 not found exception!";
    }
}