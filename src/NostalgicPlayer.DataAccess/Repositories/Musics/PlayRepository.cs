using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.Domain.Entities.Musics.Plays;

namespace NostalgicPlayer.DataAccess.Repositories.Musics;

public class PlayRepository : BaseRepository, IPlayRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM plays";
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

    public async Task<int> CreateAsync(Play entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.plays (music_id, created_at) VALUES (@MusicId, @CreatedAt);";
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

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Play?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Play entity)
    {
        throw new NotImplementedException();
    }
}