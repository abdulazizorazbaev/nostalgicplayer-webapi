namespace NostalgicPlayer.Service.DTOs.Auth;

public class LoginDto
{
    public string PhoneNumber { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
}