using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.Domain.Entities.Singers;

namespace NostalgicPlayer.DataAccess.Interfaces.Singers;

public interface ISingerRepository : IRepository<Singer, Singer>,
    IGetAll<Singer>, ISearchable<Singer>
{

}