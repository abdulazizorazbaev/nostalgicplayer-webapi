using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Albums;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Albums;
using NostalgicPlayer.Domain.Entities.Genres;

namespace NostalgicPlayer.DataAccess.Repositories.Albums;

public class AlbumRepository : BaseRepository, IAlbumRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM albums";
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

    public async Task<int> CreateAsync(Album entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.albums (music_id, singer_id, album_name, year, image_path, description, created_at, updated_at) " +
                "VALUES (@MusicId, @SingerId, @AlbumName, @Year, @ImagePath, @Description, @CreatedAt, @UpdatedAt);";
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
            string query = "DELETE FROM albums WHERE id = @Id";
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

    public async Task<IList<Album>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM albums ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Album>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Album>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Album?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM albums WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Album>(query, new { Id = id });
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

    public Task<(int ItemsCount, IList<Album>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Album entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.albums " +
                $"SET music_id=@MusicId, singer_id=@SingerId, album_name=@AlbumName, year=@Year, image_path=@ImagePath, description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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