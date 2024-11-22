using MediatR;

namespace SocialNetworkBackend.Application.Requests.ChatRequests.GetChats;

public class GetChatsRequest : IRequest<List<GetChatsDto>>
{
}
