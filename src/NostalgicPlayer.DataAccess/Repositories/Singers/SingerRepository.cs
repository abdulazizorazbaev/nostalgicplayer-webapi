using Dapper;
using NostalgicPlayer.DataAccess.Interfaces.Singers;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Singers;

namespace NostalgicPlayer.DataAccess.Repositories.Singers;

public class SingerRepository : BaseRepository, ISingerRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM singers";
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

    public async Task<int> CreateAsync(Singer entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.singers (first_name, last_name, image_path, bio, facebook_acc, instagram_acc, youtube_link, created_at, updated_at) " +
                "VALUES (@FirstName, @LastName, @ImagePath, @Bio, @FacebookAcc, @InstagramAcc, @YoutubeLink, @CreatedAt, @UpdatedAt);";
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
            string query = $"DELETE FROM singers WHERE id = @Id";
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

    public async Task<IList<Singer>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM singers ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Singer>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Singer>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Singer?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM singers WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<Singer>(query, new { Id = id });
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

    public Task<(int ItemsCount, IList<Singer>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Singer entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.singers " +
                $"SET first_name=@FirstName, last_name=@LastName, image_path=@ImagePath, bio=@Bio, facebook_acc=@FacebookAcc, instagram_acc=@InstagramAcc, youtube_link=@YoutubeLink, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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