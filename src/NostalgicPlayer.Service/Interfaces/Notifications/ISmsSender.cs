﻿using NostalgicPlayer.Service.DTOs.Notifications;

namespace NostalgicPlayer.Service.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}