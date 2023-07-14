using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.Domain.Entities.Genres;

namespace NostalgicPlayer.DataAccess.Interfaces.Genres;

public interface IGenreRepository : IRepository<Genre, Genre>,
    IGetAll<Genre>
{

}