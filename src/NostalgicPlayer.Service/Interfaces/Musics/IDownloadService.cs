using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Downloads;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Interfaces.Musics;

public interface IDownloadService
{
    public Task<bool> CreateAsync(DownloadCreateDto dto);

    public Task<IList<DownloadViewModel>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long downloadId);

    public Task<Download> GetByIdAsync(long downloadId);

    public Task<IList<DownloadViewModel>> SearchAsync(string search, PaginationParams @params);
}