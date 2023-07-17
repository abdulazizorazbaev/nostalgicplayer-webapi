using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.Service.DTOs.Singers;

public class SingerUpdateDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; }

    public string Bio { get; set; } = String.Empty;

    public string FacebookAcc { get; set; } = String.Empty;

    public string InstagramAcc { get; set; } = String.Empty;

    public string YoutubeLink { get; set; } = String.Empty;
}