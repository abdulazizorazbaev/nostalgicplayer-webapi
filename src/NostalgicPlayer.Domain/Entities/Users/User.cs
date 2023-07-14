using NostalgicPlayer.Domain.Enums;

namespace NostalgicPlayer.Domain.Entities.Users;

public class User : Human
{
    public DateOnly BirthDate { get; set; }

    public bool IsMale { get; set; }

    public string Email { get; set; } = String.Empty;

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public IdentityRole Role { get; set; }
}