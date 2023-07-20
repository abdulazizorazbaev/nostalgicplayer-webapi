using NostalgicPlayer.Service.DTOs.Notifications;
using NostalgicPlayer.Service.Interfaces.Notifications;

namespace NostalgicPlayer.Service.Services.Notifications;

public class SmsSender : ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage)
    {
        throw new NotImplementedException();
    }
}