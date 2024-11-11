using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.TogglePostLike;

public class TogglePostLikeRequest : IRequest
{
    public long PostId { get; set; }
}