using NostalgicPlayer.Service.DTOs.Musics;

namespace NostalgicPlayer.Service.Interfaces.Musics;

public interface IPlayService
{
    public Task<bool> CreateAsync(PlayCreateDto dto);

    public Task<long> CountAsync();
}