using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics;

namespace NostalgicPlayer.DataAccess.Interfaces.Musics;

public interface IMusicRepository : IRepository<Music, MusicViewModel>,
    IGetAll<MusicViewModel>, ISearchable<MusicViewModel>
{

}