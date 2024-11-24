using MediatR;

namespace SocialNetworkBackend.Application.Requests.PostRequests.DeleteComment;

public class DeleteCommentRequest : IRequest
{
    public long PostId {  get; set; }

    public long CommentId { get; set; }
}