using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Service.Interfaces.Users;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private const int maxPageSize = 30;

    public UsersController(IUserService userService)
    {
        this._userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _userService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _userService.CountAsync());

    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByIdAsync(long userId)
        => Ok(await _userService.GetByIdAsync(userId));

    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long userId)
        => Ok(await _userService.DeleteAsync(userId));
}