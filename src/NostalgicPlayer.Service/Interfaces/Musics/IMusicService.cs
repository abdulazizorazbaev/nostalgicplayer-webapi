using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Musics;
using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Interfaces.Musics;

public interface IMusicService
{
    public Task<bool> CreateAsync(MusicCreateDto dto);

    public Task<IList<Music>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long musicId);

    public Task<Music> GetByIdAsync(long musicId);

    public Task<bool> UpdateAsync(long musicId, MusicUpdateDto dto);
}