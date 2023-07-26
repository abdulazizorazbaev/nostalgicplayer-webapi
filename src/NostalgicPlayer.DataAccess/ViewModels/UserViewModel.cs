using NostalgicPlayer.Domain.Enums;

namespace NostalgicPlayer.DataAccess.ViewModels;

public class UserViewModel
{
    public string FullName { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public IdentityRole IdentityRole { get; set; }
}