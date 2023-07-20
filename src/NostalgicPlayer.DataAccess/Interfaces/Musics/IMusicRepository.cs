using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.Domain.Entities.Musics;

namespace NostalgicPlayer.DataAccess.Interfaces.Musics;

public interface IMusicRepository : IRepository<Music, Music>,
    IGetAll<Music>, ISearchable<Music>
{
}