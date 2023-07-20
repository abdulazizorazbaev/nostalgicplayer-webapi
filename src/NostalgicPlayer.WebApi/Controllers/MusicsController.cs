using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Validators.DTOs.Musics;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/musics")]
[ApiController]
public class MusicsController : ControllerBase
{
    private readonly IMusicService _musicService;
    private readonly int maxPageSize = 30;

    public MusicsController(IMusicService musicService)
    {
        this._musicService = musicService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _musicService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _musicService.CountAsync());

    [HttpGet("{musicId}")]
    public async Task<IActionResult> GetByIdAsync(long musicId)
        => Ok(await _musicService.GetByIdAsync(musicId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] MusicCreateDto dto)
    {
        var createValidator = new MusicCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _musicService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{musicId}")]
    public async Task<IActionResult> UpdateAsync(long musicId, [FromForm] MusicUpdateDto dto)
    {
        var updateValidator = new MusicUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _musicService.UpdateAsync(musicId, dto));
        else return BadRequest(result.Errors);
    }

    [HttpDelete("{musicId}")]
    public async Task<IActionResult> DeleteAsync(long musicId)
        => Ok(await _musicService.DeleteAsync(musicId));
}