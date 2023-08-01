using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.Domain.Entities.Musics.Plays;
using NostalgicPlayer.Domain.Exceptions.Musics;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;

namespace NostalgicPlayer.Service.Services.Musics;

public class PlayService : IPlayService
{
    private readonly IPlayRepository _playRepository;
    private readonly IMusicRepository _musicRepository;

    public PlayService(IPlayRepository playRepository, IMusicRepository musicRepository)
    {
        this._playRepository = playRepository;
        this._musicRepository = musicRepository;
    }

    public async Task<long> CountAsync() => await _playRepository.CountAsync();

    public async Task<bool> CreateAsync(PlayCreateDto dto)
    {
        var music = await _musicRepository.GetByIdAsync(dto.MusicId);
        if (music is null) throw new MusicNotFoundException();
        else
        {
            Play play = new Play()
            {
                MusicId = dto.MusicId,
                CreatedAt = TimeHelper.GetDateTime()
            };
            var result = await _playRepository.CreateAsync(play);
            return result > 0;
        }
    }
}