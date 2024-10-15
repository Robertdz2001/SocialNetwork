using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetProfilePicture;

public class GetProfilePictureRequest : IRequest<GetProfilePictureDto>
{
    public long UserId { get; set; }
}