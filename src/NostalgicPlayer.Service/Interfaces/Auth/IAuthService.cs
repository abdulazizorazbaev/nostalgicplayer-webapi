using NostalgicPlayer.Service.DTOs.Auth;

namespace NostalgicPlayer.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeRegisterAsync(string phoneNumber);

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);
}