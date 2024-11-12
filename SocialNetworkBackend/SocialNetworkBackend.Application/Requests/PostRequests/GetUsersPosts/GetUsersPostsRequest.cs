using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetUsersPosts;

public class GetUsersPostsRequest : IRequest<List<GetUsersPostsDto>>
{
    public long UserId {  get; set; }
}