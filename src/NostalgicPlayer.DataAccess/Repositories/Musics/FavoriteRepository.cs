using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;

namespace NostalgicPlayer.DataAccess.Repositories.Musics;

public class FavoriteRepository : BaseRepository, IFavoriteRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM favorites";
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

    public async Task<int> CreateAsync(Favorite entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.favorites (music_id, description, created_at) " +
                "VALUES (@MusicId, @Description, @CreatedAt);";
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
            string query = "DELETE FROM favorites WHERE id = @Id";
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

    public async Task<IList<FavoriteViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM favorites ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<FavoriteViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<FavoriteViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Favorite?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT *  FROM favorites WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Favorite>(query, new { Id = id });
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

    public Task<(int ItemsCount, IList<FavoriteViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Favorite entity)
    {
        throw new NotImplementedException();
    }
}