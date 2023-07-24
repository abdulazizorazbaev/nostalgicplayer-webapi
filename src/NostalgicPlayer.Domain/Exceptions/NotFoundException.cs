using System.Net;

namespace NostalgicPlayer.Domain.Exceptions;

public class NotFoundException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

    public string TitleMessage { get; protected set; } = String.Empty;
}