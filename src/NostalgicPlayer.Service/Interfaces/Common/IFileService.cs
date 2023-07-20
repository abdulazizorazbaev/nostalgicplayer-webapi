using Microsoft.AspNetCore.Http;

namespace NostalgicPlayer.Service.Interfaces.Common;

public interface IFileService
{
    // returns subpath of image
    public Task<string> UploadImageAsync(IFormFile image);

    public Task<bool> DeleteImageAsync(string subpath);

    // returns subpath of avatar
    public Task<string> UploadAvatarAsync(IFormFile avatar);

    public Task<bool> DeleteAvatarAsync(string subpath);

    // returns subpath of mp3
    public Task<string> UploadMp3Async(IFormFile mp3);

    public Task<bool> DeleteMp3Async(string subpath);
}