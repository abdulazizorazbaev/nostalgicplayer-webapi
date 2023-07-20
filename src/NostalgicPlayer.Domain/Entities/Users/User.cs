using NostalgicPlayer.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NostalgicPlayer.Domain.Entities.Users;

public class User : Human
{
    [MaxLength(13)]
    public string PhoneNumber { get; set; } = String.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public bool IsMale { get; set; }

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public IdentityRole Role { get; set; }
}