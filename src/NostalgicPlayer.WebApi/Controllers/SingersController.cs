using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Singers;
using NostalgicPlayer.Service.Interfaces.Singers;
using NostalgicPlayer.Service.Validators.DTOs.Singers;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/singers")]
[ApiController]
public class SingersController : ControllerBase
{
    private readonly ISingerService _service;
    private const int maxPageSize = 30;

    public SingersController(ISingerService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpGet("{singerId}")]
    public async Task<IActionResult> GetByIdAsync(long singerId)
        => Ok(await _service.GetByIdAsync(singerId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] SingerCreateDto dto)
    {
        var createValidator = new SingerCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{singerId}")]
    public async Task<IActionResult> UpdateAsync(long singerId, [FromForm] SingerUpdateDto dto)
    {
        var updateValidator = new SingerUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.UpdateAsync(singerId, dto));
        else return BadRequest(result.Errors);
    }

    [HttpDelete("{singerId}")]
    public async Task<IActionResult> DeleteAsync(long singerId)
        => Ok(await _service.DeleteAsync(singerId));
}