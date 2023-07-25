using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Validators.DTOs.Musics;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/favorites")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly int maxPageSize = 30;

    public FavoritesController(IFavoriteService favoriteService)
    {
        this._favoriteService = favoriteService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _favoriteService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _favoriteService.CountAsync());

    [HttpGet("{favoriteId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long favoriteId)
        => Ok(await _favoriteService.GetByIdAsync(favoriteId));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] FavoriteCreateDto dto)
    {
        var createValidator = new FavoriteCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _favoriteService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpDelete("{favoriteId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long favoriteId)
        => Ok(await _favoriteService.DeleteAsync(favoriteId));
}