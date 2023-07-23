using NostalgicPlayer.Domain.Entities.Users;

namespace NostalgicPlayer.Service.Interfaces.Auth;

public interface ITokenService
{
    public Task<string> GenerateToken(User user);
}