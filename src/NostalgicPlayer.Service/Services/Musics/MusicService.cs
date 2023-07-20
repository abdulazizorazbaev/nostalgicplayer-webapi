using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Genres;
using NostalgicPlayer.Domain.Entities.Musics;
using NostalgicPlayer.Domain.Exceptions.Files;
using NostalgicPlayer.Domain.Exceptions.Musics;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Musics;

namespace NostalgicPlayer.Service.Services.Musics;

public class MusicService : IMusicService
{
    private readonly IMusicRepository _musicRepository;
    private readonly IFileService _fileService;

    public MusicService(IMusicRepository musicRepository, IFileService fileService)
    {
        this._musicRepository = musicRepository;
        this._fileService = fileService;
    }

    public async Task<long> CountAsync() => await _musicRepository.CountAsync();

    public async Task<bool> CreateAsync(MusicCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        string mp3Path = await _fileService.UploadMp3Async(dto.Mp3Path);
        Music music = new Music()
        {
            GenreId = dto.GenreId,
            SingerId = dto.SingerId,
            MusicName = dto.MusicName,
            Duration = dto.Duration,
            ImagePath = imagePath,
            Mp3Path = mp3Path,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _musicRepository.CreateAsync(music);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long musicId)
    {
        var music = await _musicRepository.GetByIdAsync(musicId);
        if (music is null) throw new MusicNotFoundException();

        var result = await _fileService.DeleteMp3Async(music.Mp3Path);
        if (result == false) throw new Mp3NotFoundException();

        var dbResult = await _musicRepository.DeleteAsync(musicId);
        return dbResult > 0;
    }

    public async Task<IList<Music>> GetAllAsync(PaginationParams @params)
    {
        var musics = await _musicRepository.GetAllAsync(@params);
        return musics;
    }

    public async Task<Music> GetByIdAsync(long musicId)
    {
        var music = await _musicRepository.GetByIdAsync(musicId);
        if (music is null) throw new MusicNotFoundException();
        else return music;
    }

    public async Task<bool> UpdateAsync(long musicId, MusicUpdateDto dto)
    {
        var music = await _musicRepository.GetByIdAsync(musicId);
        if (music is null) throw new MusicNotFoundException();

        music.SingerId = dto.SingerId;
        music.GenreId = dto.GenreId;
        music.MusicName = dto.MusicName;
        music.Duration = dto.Duration;
        music.Description = dto.Description;

        if(dto.ImagePath is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(music.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            music.ImagePath = newImagePath;
        }

        if(dto.Mp3Path is not null)
        {
            var deleteResult = await _fileService.DeleteMp3Async(music.Mp3Path);
            if (deleteResult is false) throw new Mp3NotFoundException();

            string newMp3Path = await _fileService.UploadMp3Async(dto.Mp3Path);

            music.Mp3Path = newMp3Path;
        }

        music.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _musicRepository.UpdateAsync(musicId, music);
        return dbResult > 0;
    }
}