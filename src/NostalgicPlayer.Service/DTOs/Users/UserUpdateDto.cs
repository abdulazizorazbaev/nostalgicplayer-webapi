using Microsoft.AspNetCore.Http;
using NostalgicPlayer.Domain.Enums;

namespace NostalgicPlayer.Service.DTOs.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; }

    public string PhoneNumber { get; set; } = String.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public bool IsMale { get; set; }

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public IdentityRole IdentityRole { get; set; }
}