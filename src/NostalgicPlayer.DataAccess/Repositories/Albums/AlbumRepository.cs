using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Albums;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Albums;

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

    public async Task<IList<AlbumViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM albums_view ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<AlbumViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<AlbumViewModel>();
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

    public async Task<(int ItemsCount, IList<AlbumViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM albums_view WHERE music_name ILIKE @search ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<AlbumViewModel>(query, new { search = "%" + search + "%" })).ToList();
            return (result.Count, result);
        }
        catch
        {
            return (0, new List<AlbumViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
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