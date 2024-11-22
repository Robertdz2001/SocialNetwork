using MediatR;

namespace SocialNetworkBackend.Application.Requests.ChatRequests.GetChatsMessages;

public class GetChatsMessagesRequest : IRequest<List<GetChatsMessagesDto>>
{
    public long Id { get; set; }
}