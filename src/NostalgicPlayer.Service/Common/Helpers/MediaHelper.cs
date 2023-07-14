﻿namespace NostalgicPlayer.Service.Common.Helpers;

public class MediaHelper
{
    public static string MakeImageName(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extensiom = fileInfo.Extension;
        string name = "IMG_" + Guid.NewGuid() + extensiom;
        return name;
    }
}