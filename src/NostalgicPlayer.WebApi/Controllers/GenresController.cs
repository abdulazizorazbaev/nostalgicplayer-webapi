﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.DTOs.Genres;
using NostalgicPlayer.Service.Interfaces.Genres;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly int maxPageSize = 30;

    public GenresController(IGenreService genreService)
    {
        this._genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _genreService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _genreService.CountAsync());

    [HttpGet("genreId")]
    public async Task<IActionResult> GetByIdAsync(long genreId)
        => Ok(await _genreService.GetByIdAsync(genreId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] GenreCreateDto dto)
        => Ok(await _genreService.CreateAsync(dto));

    [HttpPut("{genreId}")]
    public async Task<IActionResult> UpdateAsync(long genreId, [FromForm] GenreUpdateDto dto)
        => Ok(await _genreService.UpdateAsync(genreId, dto));

    [HttpDelete("{genreId}")]
    public async Task<IActionResult> DeleteAsync(long genreId)
        => Ok(await _genreService.DeleteAsync(genreId));
}