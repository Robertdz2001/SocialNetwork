using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetPostPhoto;

public class GetPostPhotoRequest : IRequest<GetPostPhotoDto>
{
    public long PostId { get; set; }
}