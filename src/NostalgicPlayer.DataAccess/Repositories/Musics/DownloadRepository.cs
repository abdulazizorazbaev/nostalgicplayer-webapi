using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Downloads;
using NostalgicPlayer.Domain.Entities.Musics.Favorites;

namespace NostalgicPlayer.DataAccess.Repositories.Musics;

public class DownloadRepository : BaseRepository, IDownloadRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM downloads";
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

    public async Task<int> CreateAsync(Download entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.downloads (music_id, user_id, created_at) " +
                "VALUES (@MusicId, @UserId, @CreatedAt);";
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
            string query = "DELETE FROM downloads WHERE id = @Id";
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

    public async Task<IList<DownloadViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM downloads_view ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<DownloadViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<DownloadViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Download?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT *  FROM downloads WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Download>(query, new { Id = id });
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

    public async Task<(int ItemsCount, IList<DownloadViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM downloads_view WHERE music_name ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<DownloadViewModel>(query, new { search = "%" + search + "%" })).ToList();
            return (result.Count, result);
        }
        catch
        {
            return (0, new List<DownloadViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<int> UpdateAsync(long id, Download entity)
    {
        throw new NotImplementedException();
    }
}