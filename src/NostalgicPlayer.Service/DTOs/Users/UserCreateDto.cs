using NostalgicPlayer.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NostalgicPlayer.Service.DTOs.Users;

public class UserCreateDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public bool IsMale { get; set; }

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public IdentityRole IdentityRole { get; set; }
}