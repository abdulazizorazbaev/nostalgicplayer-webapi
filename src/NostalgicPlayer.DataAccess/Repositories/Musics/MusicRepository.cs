using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
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

    public async Task<IList<MusicViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM musics_view ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<MusicViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<MusicViewModel>();
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

    public async Task<IList<Music>> GetMusicsBySingerIdAsync(long singerId, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM musics WHERE singer_id=@SingerId ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Music>(query, new { singerId = singerId})).ToList();
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

    public async Task<(int ItemsCount, IList<MusicViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM musics_view WHERE music_name ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<MusicViewModel>(query, new { search = "%" + search + "%" })).ToList();
            return (result.Count, result);
        }
        catch
        {
            return (0, new List<MusicViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
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