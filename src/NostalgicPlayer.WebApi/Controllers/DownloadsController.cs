using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Musics;
using NostalgicPlayer.Service.Interfaces.Musics;
using NostalgicPlayer.Service.Validators.DTOs.Musics;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/downloads")]
[ApiController]
public class DownloadsController : ControllerBase
{
    private readonly IDownloadService _downloadService;
    private const int maxPageSize = 30;

    public DownloadsController(IDownloadService downloadService)
    {
        this._downloadService = downloadService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _downloadService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _downloadService.CountAsync());

    [HttpGet("{downloadId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long downloadId)
        => Ok(await _downloadService.GetByIdAsync(downloadId));

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        => Ok(await _downloadService.SearchAsync(search, new PaginationParams(page, maxPageSize)));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] DownloadCreateDto dto)
    {
        var createValidator = new DownloadCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _downloadService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpDelete("{downloadId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long downloadId)
        => Ok(await _downloadService.DeleteAsync(downloadId));
}