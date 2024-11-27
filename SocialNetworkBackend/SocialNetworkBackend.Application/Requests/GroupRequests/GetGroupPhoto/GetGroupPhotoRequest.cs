using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupPhoto;

public class GetGroupPhotoRequest : IRequest<GetGroupPhotoDto>
{
    public long GroupId { get; set; }
}