using NostalgicPlayer.DataAccess.Interfaces.Albums;
using NostalgicPlayer.DataAccess.Interfaces.Genres;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Interfaces.Singers;
using NostalgicPlayer.DataAccess.Interfaces.Users;
using NostalgicPlayer.DataAccess.Repositories.Albums;
using NostalgicPlayer.DataAccess.Repositories.Genres;
using NostalgicPlayer.DataAccess.Repositories.Musics;
using NostalgicPlayer.DataAccess.Repositories.Singers;
using NostalgicPlayer.DataAccess.Repositories.Users;

namespace NostalgicPlayer.WebApi.Configurations.Layers;

public static class DataAccessConfiguration
{
    public static void ConfigureDataAccess(this WebApplicationBuilder builder)
    {
        //-> DI and IoC containers
        builder.Services.AddScoped<IGenreRepository, GenreRepository>();
        builder.Services.AddScoped<ISingerRepository, SingerRepository>();
        builder.Services.AddScoped<IMusicRepository, MusicRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
        builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        builder.Services.AddScoped<IPlayRepository, PlayRepository>();
        builder.Services.AddScoped < IDownloadRepository, DownloadRepository>();
    } 
}