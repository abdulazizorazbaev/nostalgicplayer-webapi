using NostalgicPlayer.Domain.Enums;

namespace NostalgicPlayer.Service.Interfaces.Auth;

public interface IIdentityService
{
    public long UserId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string PhoneNumber { get; }

    public IdentityRole? IdentityRole { get; }
}