using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.Domain.Entities.Albums;

namespace NostalgicPlayer.DataAccess.Interfaces.Albums;

public interface IAlbumRepository : IRepository<Album, Album>,
    IGetAll<Album>
{

}