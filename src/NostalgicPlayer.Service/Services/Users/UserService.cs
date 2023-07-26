using NostalgicPlayer.DataAccess.Interfaces.Users;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Singers;
using NostalgicPlayer.Domain.Entities.Users;
using NostalgicPlayer.Domain.Exceptions.Files;
using NostalgicPlayer.Domain.Exceptions.Singers;
using NostalgicPlayer.Domain.Exceptions.Users;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Users;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Users;

namespace NostalgicPlayer.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public UserService(IUserRepository userRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._userRepository = userRepository;
        this._fileService = fileService;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _userRepository.CountAsync();

    public async Task<bool> CreateAsync(UserCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        User user = new User()
        {
            ImagePath = imagePath,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirmed = dto.PhoneNumberConfirmed,
            IsMale = dto.IsMale,
            PasswordHash = dto.PasswordHash,
            Salt = dto.Salt,
            IdentityRole = dto.IdentityRole,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _userRepository.CreateAsync(user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();

        var result = await _fileService.DeleteImageAsync(user.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _userRepository.DeleteAsync(userId);
        return dbResult > 0;
    }

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.GetAllAsync(@params);
        var count = await _userRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return users;
    }

    public async Task<User> GetByIdAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();
        else return user;
    }

    public async Task<bool> UpdateAsync(long userId, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.PhoneNumber = dto.PhoneNumber;
        user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        user.IsMale = dto.IsMale;
        user.PasswordHash = dto.PasswordHash;
        user.Salt = dto.Salt;
        user.IdentityRole = dto.IdentityRole;
        
        if (dto.ImagePath is not null)
        {
            // deletes an old image
            var deleteResult = await _fileService.DeleteImageAsync(user.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // uploads a new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            // parses a new path to a genre
            user.ImagePath = newImagePath;
        }
        // else genre's old image have to be saved

        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _userRepository.UpdateAsync(userId, user);
        return dbResult > 0;
    }
}