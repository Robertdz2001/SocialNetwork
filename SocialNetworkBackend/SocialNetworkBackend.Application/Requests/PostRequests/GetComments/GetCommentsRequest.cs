using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetComments;

public class GetCommentsRequest : IRequest<List<GetCommentsDto>>
{
    public long PostId {  get; set; }
}
