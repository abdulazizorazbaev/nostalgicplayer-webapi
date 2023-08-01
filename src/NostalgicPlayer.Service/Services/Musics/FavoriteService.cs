using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;
using NostalgicPlayer.Domain.Exceptions.Musics;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Musics;

namespace NostalgicPlayer.Service.Services.Musics;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IPaginator _paginator;
    private readonly IMusicRepository _musicRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository,
        IFileService fileService,
        IPaginator paginator,
        IMusicRepository musicRepository)
    {
        this._favoriteRepository = favoriteRepository;
        this._paginator = paginator;
        this._musicRepository = musicRepository;
    }
    public async Task<long> CountAsync() => await _favoriteRepository.CountAsync();

    public async Task<bool> CreateAsync(FavoriteCreateDto dto)
    {
        var music = await _musicRepository.GetByIdAsync(dto.MusicId);
        if (music == null) throw new MusicNotFoundException();
        else
        {
            Favorite favorite = new Favorite()
            {
                MusicId = dto.MusicId,
                Description = dto.Description,
                CreatedAt = TimeHelper.GetDateTime()
            };
            var result = await _favoriteRepository.CreateAsync(favorite);
            return result > 0;
        }
    }

    public async Task<bool> DeleteAsync(long favoriteId)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(favoriteId);
        if (favorite is null) throw new FavoriteNotFoundException();

        var dbResult = await _favoriteRepository.DeleteAsync(favoriteId);
        return dbResult > 0;
    }

    public async Task<IList<FavoriteViewModel>> GetAllAsync(PaginationParams @params)
    {
        var favorites = await _favoriteRepository.GetAllAsync(@params);
        var count = await _favoriteRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return favorites;
    }

    public async Task<Favorite> GetByIdAsync(long favoriteId)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(favoriteId);
        if (favorite is null) throw new FavoriteNotFoundException();
        else return favorite;
    }

    public async Task<IList<FavoriteViewModel>> SearchAsync(string search, PaginationParams @params)
    {
        var favorites = await _favoriteRepository.SearchAsync(search, @params);
        var count = await _favoriteRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return favorites.Item2.ToList();
    }
}