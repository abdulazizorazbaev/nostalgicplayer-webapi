using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;

namespace NostalgicPlayer.DataAccess.Interfaces.Musics;

public interface IFavoriteRepository : IRepository<Favorite, Favorite>,
    IGetAll<FavoriteViewModel>, ISearchable<FavoriteViewModel>
{
}