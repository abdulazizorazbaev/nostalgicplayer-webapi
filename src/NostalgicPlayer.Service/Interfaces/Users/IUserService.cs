using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Singers;
using NostalgicPlayer.Domain.Entities.Users;
using NostalgicPlayer.Service.DTOs.Singers;
using NostalgicPlayer.Service.DTOs.Users;

namespace NostalgicPlayer.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> CreateAsync(UserCreateDto dto);

    public Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long userId);

    public Task<User> GetByIdAsync(long userId);

    public Task<bool> UpdateAsync(long userId, UserUpdateDto dto);
}