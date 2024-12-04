using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupDetails;

public class GetGroupDetailsRequest : IRequest<GetGroupDetailsDto>
{
    public long Id {  get; set; }
}