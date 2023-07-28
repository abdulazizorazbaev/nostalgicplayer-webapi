using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics;

namespace NostalgicPlayer.DataAccess.Interfaces.Musics;

public interface IMusicRepository : IRepository<Music, Music>,
    IGetAll<MusicViewModel>, ISearchable<MusicViewModel>
{
    public Task<IList<Music>> GetMusicsBySingerIdAsync(long singerId, PaginationParams @params);
}