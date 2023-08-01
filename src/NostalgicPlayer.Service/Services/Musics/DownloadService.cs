using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Repositories.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Downloads;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;
using NostalgicPlayer.Domain.Exceptions.Musics;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Auth;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Musics;

namespace NostalgicPlayer.Service.Services.Musics;

public class DownloadService : IDownloadService
{
    private readonly IDownloadRepository _downloadRepository;
    private readonly IPaginator _paginator;
    private readonly IIdentityService _identity;
    private readonly IMusicRepository _musicRepository;

    public DownloadService(IDownloadRepository downloadRepository,
        IPaginator paginator, IIdentityService identityService,
        IMusicRepository musicRepository)
    {
        this._downloadRepository = downloadRepository;
        this._paginator = paginator;
        this._identity = identityService;
        this._musicRepository = musicRepository;
    }
    public async Task<long> CountAsync() => await _downloadRepository.CountAsync();

    public async Task<bool> CreateAsync(DownloadCreateDto dto)
    {
        var music = await _musicRepository.GetByIdAsync(dto.MusicId);
        if (music == null) throw new MusicNotFoundException();
        else
        {
            Download download = new Download()
            {
                MusicId = dto.MusicId,
                UserId = _identity.UserId,
                CreatedAt = TimeHelper.GetDateTime()
            };
            var result = await _downloadRepository.CreateAsync(download);
            return result > 0;
        }
    }

    public async Task<bool> DeleteAsync(long downloadId)
    {
        var download = await _downloadRepository.GetByIdAsync(downloadId);
        if (download is null) throw new DownloadNotFoundException();

        var dbResult = await _downloadRepository.DeleteAsync(downloadId);
        return dbResult > 0;
    }

    public async Task<IList<DownloadViewModel>> GetAllAsync(PaginationParams @params)
    {
        var downloads = await _downloadRepository.GetAllAsync(@params);
        var count = await _downloadRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return downloads;
    }

    public async Task<Download> GetByIdAsync(long downloadId)
    {
        var download = await _downloadRepository.GetByIdAsync(downloadId);
        if (download is null) throw new DownloadNotFoundException();
        else return download;
    }

    public async Task<IList<DownloadViewModel>> SearchAsync(string search, PaginationParams @params)
    {
        var downloads = await _downloadRepository.SearchAsync(search, @params);
        var count = await _downloadRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return downloads.Item2.ToList();
    }
}