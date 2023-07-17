using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Singers;
using NostalgicPlayer.Service.DTOs.Singers;

namespace NostalgicPlayer.Service.Interfaces.Singers;

public interface ISingerService
{
    public Task<bool> CreateAsync(SingerCreateDto dto);

    public Task<IList<Singer>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long singerId);

    public Task<Singer> GetByIdAsync(long singerId);

    public Task<bool> UpdateAsync(long singerId, SingerUpdateDto dto);
}