using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Services.Musics;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _musicService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _musicService.CountAsync());

    [HttpGet("{musicId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long musicId)
        => Ok(await _musicService.GetByIdAsync(musicId));

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        => Ok(await _musicService.SearchAsync(search, new PaginationParams(page, maxPageSize)));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] MusicCreateDto dto)
    {
        var createValidator = new MusicCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _musicService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{musicId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long musicId, [FromForm] MusicUpdateDto dto)
    {
        var updateValidator = new MusicUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _musicService.UpdateAsync(musicId, dto));
        else return BadRequest(result.Errors);
    }

    [HttpDelete("{musicId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long musicId)
        => Ok(await _musicService.DeleteAsync(musicId));
}