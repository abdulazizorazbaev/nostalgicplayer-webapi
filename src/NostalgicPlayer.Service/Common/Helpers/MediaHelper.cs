namespace NostalgicPlayer.Service.Common.Helpers;

public class MediaHelper
{
    public static string MakeImageName(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "IMG_" + Guid.NewGuid() + extension;
        return name;
    }

    public static string MakeMp3Name(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "MP3_" + Guid.NewGuid() + extension;
        return name;
    }

    public static string[] GetImageExtensions()
    {
        return new string[]
        {
            ".jpg",
            ".png",
            ".jpeg",
            ".bmp",
            ".svg"
        };
    }

    public static string[] GetMp3Extensions()
    {
        return new string[]
        {
            ".mp3",
            ".wav"
        };
    }
}