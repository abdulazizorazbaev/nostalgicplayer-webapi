using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Validators.DTOs.Musics;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/plays")]
[ApiController]
public class PlaysController : ControllerBase
{
    private readonly IPlayService _playService;

    public PlaysController(IPlayService playService)
    {
        this._playService = playService;
    }

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _playService.CountAsync());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] PlayCreateDto dto)
    {
        var createValidator = new PlayCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _playService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }
}