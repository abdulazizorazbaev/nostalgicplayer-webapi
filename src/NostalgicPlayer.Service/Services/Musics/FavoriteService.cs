using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Albums;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;
using NostalgicPlayer.Domain.Exceptions.Albums;
using NostalgicPlayer.Domain.Exceptions.Files;
using NostalgicPlayer.Domain.Exceptions.Musics;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Musics;

namespace NostalgicPlayer.Service.Services.Musics;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IFileService _fileService;

    public FavoriteService(IFavoriteRepository favoriteRepository,
        IFileService fileService)
    {
        this._favoriteRepository = favoriteRepository;
        this._fileService = fileService;
    }
    public async Task<long> CountAsync() => await _favoriteRepository.CountAsync();

    public async Task<bool> CreateAsync(FavoriteCreateDto dto)
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

    public async Task<bool> DeleteAsync(long favoriteId)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(favoriteId);
        if (favorite is null) throw new FavoriteNotFoundException();

        var dbResult = await _favoriteRepository.DeleteAsync(favoriteId);
        return dbResult > 0;
    }

    public Task<IList<Favorite>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<Favorite> GetByIdAsync(long favoriteId)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(favoriteId);
        if (favorite is null) throw new FavoriteNotFoundException();
        else return favorite;
    }
}