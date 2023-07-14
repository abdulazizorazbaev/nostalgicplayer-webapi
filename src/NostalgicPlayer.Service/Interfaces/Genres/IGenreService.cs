using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Genres;
using NostalgicPlayer.Service.DTOs.Genres;

namespace NostalgicPlayer.Service.Interfaces.Genres;

public interface IGenreService
{
    public Task<bool> CreateAsync(GenreCreateDto dto);

    public Task<IList<Genre>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long genreId);

    public Task<Genre> GetByIdAsync(long genreId);

    public Task<bool> UpdateAsync(long genreId, GenreUpdateDto dto);
}