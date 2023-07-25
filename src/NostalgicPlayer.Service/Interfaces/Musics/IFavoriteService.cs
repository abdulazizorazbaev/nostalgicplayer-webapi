using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Interfaces.Musics;

public interface IFavoriteService
{
    public Task<bool> CreateAsync(FavoriteCreateDto dto);

    public Task<IList<FavoriteViewModel>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long favoriteId);

    public Task<Favorite> GetByIdAsync(long favoriteId);
}