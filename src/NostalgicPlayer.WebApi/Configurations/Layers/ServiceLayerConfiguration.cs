using NostalgicPlayer.Service.Interfaces.Albums;
using NostalgicPlayer.Service.Interfaces.Auth;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Genres;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Interfaces.Notifications;
using NostalgicPlayer.Service.Interfaces.Singers;
using NostalgicPlayer.Service.Services.Albums;
using NostalgicPlayer.Service.Services.Auth;
using NostalgicPlayer.Service.Services.Common;
using NostalgicPlayer.Service.Services.Genres;
using NostalgicPlayer.Service.Services.Musics;
using NostalgicPlayer.Service.Services.Notifications;
using NostalgicPlayer.Service.Services.Singers;

namespace NostalgicPlayer.WebApi.Configurations.Layers;

public static class ServiceLayerConfiguration
{
    public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
    {
        //-> DI and IoC containers
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<ISingerService, SingerService>();
        builder.Services.AddScoped<IMusicService, MusicService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IAlbumService, AlbumService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IFavoriteService, FavoriteService>();
        builder.Services.AddScoped<IPlayService, PlayService>();
        builder.Services.AddScoped<IPaginator, Paginator>();
        builder.Services.AddSingleton<ISmsSender, SmsSender>();
    }
}