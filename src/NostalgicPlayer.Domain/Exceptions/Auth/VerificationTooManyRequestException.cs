namespace NostalgicPlayer.Domain.Exceptions.Auth;

public class VerificationTooManyRequestException : TooManyRequestException
{
    public VerificationTooManyRequestException()
    {
        TitleMessage = "You have reached a limit!";
    }
}