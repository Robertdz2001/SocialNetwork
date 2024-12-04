using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.DeleteGroup;

public class DeleteGroupRequest : IRequest
{
    public long Id {  get; set; }
}