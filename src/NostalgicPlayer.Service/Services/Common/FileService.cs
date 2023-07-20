using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.Interfaces.Common;

namespace NostalgicPlayer.Service.Services.Common;

public class FileService : IFileService
{
    private readonly string MEDIA = "media";
    private readonly string IMAGES = "images";
    //private readonly string AVATARS = "avatars";
    private readonly string ROOTPATH;
    private readonly string AUDIOS = "audios";

    public FileService(IWebHostEnvironment env)
    {
        ROOTPATH = env.WebRootPath;
    }

    public Task<bool> DeleteAvatarAsync(string subpath)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadAvatarAsync(IFormFile avatar)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);
        string subpath = Path.Combine(MEDIA, IMAGES, newImageName);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }

    public async Task<string> UploadMp3Async(IFormFile mp3)
    {
        string newMp3Name = MediaHelper.MakeMp3Name(mp3.FileName);
        string subpath = Path.Combine(MEDIA, AUDIOS, newMp3Name);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await mp3.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }

    public async Task<bool> DeleteMp3Async(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }
}