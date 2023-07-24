using NostalgicPlayer.DataAccess.Interfaces.Albums;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Albums;
using NostalgicPlayer.Domain.Exceptions.Albums;
using NostalgicPlayer.Domain.Exceptions.Files;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Albums;
using NostalgicPlayer.Service.Interfaces.Albums;
using NostalgicPlayer.Service.Interfaces.Common;

namespace NostalgicPlayer.Service.Services.Albums;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public AlbumService(IAlbumRepository albumRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._albumRepository = albumRepository;
        this._fileService = fileService;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _albumRepository.CountAsync();

    public async Task<bool> CreateAsync(AlbumCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        Album album = new Album()
        {
            MusicId = dto.MusicId,
            SingerId = dto.SingerId,
            AlbumName = dto.AlbumName,
            Year = dto.Year,
            ImagePath = imagePath,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _albumRepository.CreateAsync(album);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long albumId)
    {
        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album is null) throw new AlbumNotFoundException();

        var result = await _fileService.DeleteImageAsync(album.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _albumRepository.DeleteAsync(albumId);
        return dbResult > 0;
    }

    public async Task<IList<Album>> GetAllAsync(PaginationParams @params)
    {
        var albums = await _albumRepository.GetAllAsync(@params);
        long count = await _albumRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return albums;
    }

    public async Task<Album> GetByIdAsync(long albumId)
    {
        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album is null) throw new AlbumNotFoundException();
        else return album;
    }

    public async Task<bool> UpdateAsync(long albumId, AlbumUpdateDto dto)
    {
        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album is null) throw new AlbumNotFoundException();

        album.SingerId = dto.SingerId;
        album.MusicId = dto.MusicId;
        album.AlbumName = dto.AlbumName;
        album.Year = dto.Year;
        album.Description = dto.Description;

        if (dto.ImagePath is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(album.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            album.ImagePath = newImagePath;
        }

        album.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _albumRepository.UpdateAsync(albumId, album);
        return dbResult > 0;
    }
}