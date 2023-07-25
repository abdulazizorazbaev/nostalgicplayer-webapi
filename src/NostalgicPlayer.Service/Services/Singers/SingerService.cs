using NostalgicPlayer.DataAccess.Interfaces.Singers;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Singers;
using NostalgicPlayer.Domain.Exceptions.Files;
using NostalgicPlayer.Domain.Exceptions.Singers;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Singers;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Singers;

namespace NostalgicPlayer.Service.Services.Singers;

public class SingerService : ISingerService
{
    private readonly ISingerRepository _singerRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public SingerService(ISingerRepository singerRepository, IFileService fileService, IPaginator paginator)
    {
        this._singerRepository = singerRepository;
        this._fileService = fileService;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _singerRepository.CountAsync();

    public async Task<bool> CreateAsync(SingerCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        Singer singer = new Singer()
        {
            ImagePath = imagePath,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Bio = dto.Bio,
            FacebookAcc = dto.FacebookAcc,
            InstagramAcc = dto.InstagramAcc,
            YoutubeLink = dto.YoutubeLink,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _singerRepository.CreateAsync(singer);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long singerId)
    {
        var singer = await _singerRepository.GetByIdAsync(singerId);
        if (singer is null) throw new SingerNotFoundException();

        var result = await _fileService.DeleteImageAsync(singer.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _singerRepository.DeleteAsync(singerId);
        return dbResult > 0;
    }

    public async Task<IList<Singer>> GetAllAsync(PaginationParams @params)
    {
        var singers = await _singerRepository.GetAllAsync(@params);
        var count = await _singerRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return singers;
    }

    public async Task<Singer> GetByIdAsync(long singerId)
    {
        var singer = await _singerRepository.GetByIdAsync(singerId);
        if (singer is null) throw new SingerNotFoundException();
        else return singer;
    }

    public async Task<bool> UpdateAsync(long singerId, SingerUpdateDto dto)
    {
        var singer = await _singerRepository.GetByIdAsync(singerId);
        if (singer is null) throw new SingerNotFoundException();

        singer.FirstName = dto.FirstName;
        singer.LastName = dto.LastName;
        singer.Bio = dto.Bio;
        singer.FacebookAcc = dto.FacebookAcc;
        singer.InstagramAcc = dto.InstagramAcc;
        singer.YoutubeLink = dto.YoutubeLink;

        if (dto.ImagePath is not null)
        {
            // deletes an old image
            var deleteResult = await _fileService.DeleteImageAsync(singer.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // uploads a new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            // parses a new path to a genre
            singer.ImagePath = newImagePath;
        }
        // else genre's old image have to be saved

        singer.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _singerRepository.UpdateAsync(singerId, singer);
        return dbResult > 0;
    }
}