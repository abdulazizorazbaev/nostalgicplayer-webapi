using NostalgicPlayer.DataAccess.Interfaces.Genres;
using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Interfaces.Singers;
using NostalgicPlayer.DataAccess.Interfaces.Users;
using NostalgicPlayer.DataAccess.Repositories.Genres;
using NostalgicPlayer.DataAccess.Repositories.Musics;
using NostalgicPlayer.DataAccess.Repositories.Singers;
using NostalgicPlayer.DataAccess.Repositories.Users;
using NostalgicPlayer.Service.Interfaces.Auth;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Genres;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Interfaces.Singers;
using NostalgicPlayer.Service.Services.Auth;
using NostalgicPlayer.Service.Services.Common;
using NostalgicPlayer.Service.Services.Genres;
using NostalgicPlayer.Service.Services.Musics;
using NostalgicPlayer.Service.Services.Singers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

//->
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ISingerRepository, SingerRepository>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ISingerService, SingerService>();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//->

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();