﻿using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.Service.DTOs.Auth;
using NostalgicPlayer.Service.Interfaces.Auth;
using NostalgicPlayer.Service.Validators;
using NostalgicPlayer.Service.Validators.DTOs.Auth;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
    {
        var validator = new RegisterValidator();
        var result = validator.Validate(registerDto);
        if (result.IsValid)
        {
            var serviceResult = await _authService.RegisterAsync(registerDto);
            return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
        }
        else return BadRequest(result.Errors);
    }

    [HttpPost("register/send-code")]
    public async Task<IActionResult> SendCodeRegisterAsync(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        if (result == false) return BadRequest("Phone number is invalid!");
        
        var serviceResult = await _authService.SendCodeRegisterAsync(phone);
        return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
    }
}