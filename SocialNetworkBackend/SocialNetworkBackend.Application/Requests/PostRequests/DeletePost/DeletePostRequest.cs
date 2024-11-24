using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.DeletePost;

public class DeletePostRequest : IRequest
{
    public long PostId {  get; set; }
}