using Microsoft.Extensions.Caching.Memory;
using NostalgicPlayer.DataAccess.Interfaces.Users;
using NostalgicPlayer.Domain.Entities.Users;
using NostalgicPlayer.Domain.Exceptions.Auth;
using NostalgicPlayer.Domain.Exceptions.Users;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.Common.Security;
using NostalgicPlayer.Service.DTOs.Auth;
using NostalgicPlayer.Service.DTOs.Notifications;
using NostalgicPlayer.Service.DTOs.Security;
using NostalgicPlayer.Service.Interfaces.Auth;
using NostalgicPlayer.Service.Interfaces.Notifications;

namespace NostalgicPlayer.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;
    private readonly ISmsSender _smsSender;
    private readonly ITokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public AuthService(IMemoryCache memoryCache, 
        IUserRepository userRepository,
        ISmsSender smsSender,
        ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
        this._smsSender = smsSender;
        this._tokenService = tokenService;
    }

    #pragma warning disable
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

        // delete user if exists by this phone number
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto, 
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER); 
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeRegisterAsync(string phoneNumber)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phoneNumber, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            verificationDto.Code = CodeGenerator.GenerateRandomNumber();

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phoneNumber, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phoneNumber);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phoneNumber, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessage smsMessage = new SmsMessage();
            smsMessage.Title = "Nostalgic Player";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            //smsMessage.Title = "IIV";
            //smsMessage.Content = "U vas est' neoplachenniy shtraf, na summu 118.300 so'm. Sizda 118.300 so'm to'lanmagan jarimangiz bor \n soliq.uz";
            smsMessage.Recipient = phoneNumber.Substring(1);

            var smsResult = await _smsSender.SendAsync(smsMessage);
            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new UserCacheDataExpiredException();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    if (dbResult is true)
                    {
                        var user = await _userRepository.GetByPhoneAsync(phone);
                        string token = await _tokenService.GenerateToken(user);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
    {
        var user = new User();
        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;

        var hasherResult = PasswordHasher.Hasher(registerDto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();
        user.IdentityRole = Domain.Enums.IdentityRole.User;

        var dbResult = await _userRepository.CreateAsync(user);
        return dbResult > 0;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.PasswordHash, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = await _tokenService.GenerateToken(user);
        return (Result: true, Token: token);
    }
}