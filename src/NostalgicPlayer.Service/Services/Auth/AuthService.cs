using Microsoft.Extensions.Caching.Memory;
using NostalgicPlayer.DataAccess.Interfaces.Users;
using NostalgicPlayer.Domain.Exceptions.Users;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Auth;
using NostalgicPlayer.Service.DTOs.Security;
using NostalgicPlayer.Service.Interfaces.Auth;

namespace NostalgicPlayer.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;


    public AuthService(IMemoryCache memoryCache, 
        IUserRepository userRepository)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
    }

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

        // delete user if exists by this phone number
        if (_memoryCache.TryGetValue(dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else _memoryCache.Set(dto.PhoneNumber, dto, 
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER); 
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeRegisterAsync(string phoneNumber)
    {
        if (_memoryCache.TryGetValue(phoneNumber, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            // make confirm code with random numbers
            verificationDto.Code = 11111;
            _memoryCache.Set(phoneNumber, verificationDto, 
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            // sms sender::begin
            // sms sender::end

            return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
        }
        else throw new UserCacheDataExpiredException();
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }
}