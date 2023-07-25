using NostalgicPlayer.DataAccess.Interfaces.Musics;
using NostalgicPlayer.DataAccess.Repositories.Musics;
using NostalgicPlayer.Domain.Entities.Musics.Plays;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;

namespace NostalgicPlayer.Service.Services.Musics;

public class PlayService : IPlayService
{
    private readonly IPlayRepository _playRepository;

    public PlayService(IPlayRepository playRepository)
    {
        this._playRepository = playRepository;
    }

    public async Task<long> CountAsync() => await _playRepository.CountAsync();

    public async Task<bool> CreateAsync(PlayCreateDto dto)
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