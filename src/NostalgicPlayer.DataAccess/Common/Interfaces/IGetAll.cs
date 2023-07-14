using NostalgicPlayer.DataAccess.Utilities;

namespace NostalgicPlayer.DataAccess.Common.Interfaces;

public interface IGetAll<TModel>
{
    public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
}