using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.BlockUser;

public class ToggleBlockUserRequest : IRequest
{
    public long UserId { get; set; }
}