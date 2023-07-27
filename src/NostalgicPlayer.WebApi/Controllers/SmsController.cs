using Microsoft.AspNetCore.Mvc;
using NostalgicPlayer.Service.DTOs.Notifications;
using NostalgicPlayer.Service.Interfaces.Auth;
using NostalgicPlayer.Service.Interfaces.Notifications;

namespace NostalgicPlayer.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmsController : ControllerBase
{
    private readonly ISmsSender _smsSender;
    private readonly IIdentityService _identity;

    public SmsController(ISmsSender smsSender, IIdentityService identityService)
    {
        this._smsSender = smsSender;
        this._identity = identityService;
    }

    [HttpPost]
    public async Task<IActionResult> SendAsync([FromBody] SmsMessage smsMessage)
        => Ok(await _smsSender.SendAsync(smsMessage));

    #pragma warning disable
    [HttpGet]
    public async Task<IActionResult> GetAsync()
        => Ok(new { _identity.UserId, 
                    _identity.FirstName, 
                    _identity.LastName, 
                    _identity.PhoneNumber,
                    _identity.IdentityRole
        });
}