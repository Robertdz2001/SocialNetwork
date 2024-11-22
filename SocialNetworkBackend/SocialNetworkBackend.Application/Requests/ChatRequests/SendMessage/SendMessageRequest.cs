using MediatR;

namespace SocialNetworkBackend.Application.Requests.ChatRequests.SendMessage;

public class SendMessageRequest : IRequest
{
    /// <summary>
    /// Chat id.
    /// </summary>
    public long Id {  get; set; }

    public string Content {  get; set; }
}