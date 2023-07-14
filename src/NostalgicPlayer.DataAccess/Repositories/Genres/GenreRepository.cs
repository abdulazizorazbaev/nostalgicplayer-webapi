using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Genres;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Genres;
using static Dapper.SqlMapper;

namespace NostalgicPlayer.DataAccess.Repositories.Genres;

public class GenreRepository : BaseRepository, IGenreRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM genres";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Genre entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.genres (genre_name, image_path, description, created_at, updated_at) " +
                "VALUES (@GenreName, @ImagePath, @Description, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM genres WHERE id = @Id";
            var result = await _connection.ExecuteAsync(query, new {Id = id});
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Genre>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM genres ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Genre>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Genre>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Genre?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM genres WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Genre>(query, new {Id = id});
            return result;
        }
        catch
        {
            return null;
        }
        finally 
        { 
            await _connection.CloseAsync(); 
        }
    }

    public async Task<int> UpdateAsync(long id, Genre entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.genres " +
                $"SET genre_name = @GenreName, image_path = @ImagePath, description = @Description, created_at = @CreatedAt, updated_at = @UpdatedAt " +
                $"WHERE id = {id};";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}