using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUserDetails;

public class GetUserDetailsRequest: IRequest<GetUserDetailsDto>
{
    public long Id { get; set; }
}