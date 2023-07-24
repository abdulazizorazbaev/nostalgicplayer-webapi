using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Albums;
using NostalgicPlayer.Service.Interfaces.Albums;
using NostalgicPlayer.Service.Validators.DTOs.Albums;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/albums")]
[ApiController]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumService _albumService;
    private readonly int maxPageSize = 30;

    public AlbumsController(IAlbumService albumService)
    {
        this._albumService = albumService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _albumService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _albumService.CountAsync());

    [HttpGet("{albumId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long albumId)
        => Ok(await _albumService.GetByIdAsync(albumId));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] AlbumCreateDto dto)
    {
        var createValidator = new AlbumCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _albumService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{albumId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long albumId, [FromForm] AlbumUpdateDto dto)
    {
        var updateValidator = new AlbumUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _albumService.UpdateAsync(albumId, dto));
        else return BadRequest(result.Errors);
    }

    [HttpDelete("{albumId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long albumId)
        => Ok(await _albumService.DeleteAsync(albumId));
}