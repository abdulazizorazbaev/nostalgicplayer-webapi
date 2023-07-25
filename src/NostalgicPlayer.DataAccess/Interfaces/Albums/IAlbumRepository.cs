using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Albums;

namespace NostalgicPlayer.DataAccess.Interfaces.Albums;

public interface IAlbumRepository : IRepository<Album, Album>,
    IGetAll<AlbumViewModel>, ISearchable<AlbumViewModel>
{

}