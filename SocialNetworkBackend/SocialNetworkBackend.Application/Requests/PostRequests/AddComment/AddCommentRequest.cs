using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.AddComment;

public class AddCommentRequest : IRequest
{
    public long PostId {  get; set; }

    public string Content {  get; set; }
}