namespace NostalgicPlayer.Service.DTOs.Notifications;

public class SmsMessage
{
    public string Recipient { get; set; } = String.Empty;

    public string Title { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
}