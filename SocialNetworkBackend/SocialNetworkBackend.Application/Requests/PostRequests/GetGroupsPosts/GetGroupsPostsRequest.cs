using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetGroupsPosts;

public class GetGroupsPostsRequest : IRequest<List<GetGroupsPostsDto>>
{
    public long GroupId { get; set; }
}