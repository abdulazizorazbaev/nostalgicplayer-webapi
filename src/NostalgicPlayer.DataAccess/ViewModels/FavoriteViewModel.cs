using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.DataAccess.ViewModels;

public class FavoriteViewModel
{
    public string FirstName { get; set; } = String.Empty;

    public string MusicName { get; set; } = String.Empty;

    public string Mp3Path { get; set; } = String.Empty;

    public string Duration { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }
}