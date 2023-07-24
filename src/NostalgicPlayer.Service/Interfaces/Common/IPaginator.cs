using NostalgicPlayer.DataAccess.Utilities;

namespace NostalgicPlayer.Service.Interfaces.Common;

public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);
}