using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Musics;

namespace NostalgicPlayer.DataAccess.Repositories.Musics;

public class MusicRepository : BaseRepository, IMusicRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM musics";
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

    public async Task<int> CreateAsync(Music entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.musics (genre_id, singer_id, music_name, duration, image_path, mp3_path, description, created_at, updated_at) " +
                "VALUES (@GenreId, @SingerId, @MusicName, @Duration, @ImagePath, @Mp3Path, @Description, @CreatedAt, @UpdatedAt);";
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
            string query = "DELETE FROM musics WHERE id = @Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
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

    public async Task<IList<Music>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM musics ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Music>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Music>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Music?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT *  FROM musics WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Music>(query, new { Id = id });
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

    public Task<(int ItemsCount, IList<Music>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Music entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.musics " +
                $"SET genre_id=@GenreId, singer_id=@SingerId, music_name=@MusicName, duration=@Duration, image_path=@ImagePath, mp3_path=@Mp3Path, description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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