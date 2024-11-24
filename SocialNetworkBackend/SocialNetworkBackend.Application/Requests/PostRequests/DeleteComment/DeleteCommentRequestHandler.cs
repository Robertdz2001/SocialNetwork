using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.DeleteComment;

public class DeleteCommentRequestHandler : IRequestHandler<DeleteCommentRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public DeleteCommentRequestHandler(IPostRepository postRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException("Post was not found");

        var comment = post.UserComments.FirstOrDefault(x => x.Id == request.CommentId)
            ?? throw new NotFoundException("Comment was not found");

        if (comment.UserId != loggedUserId && loggedUser.RoleId != (long)UserRoles.Admin)
        {
            throw new UnauthorizedException("User is unauthorized to delete this comment");
        }

        post.UserComments.Remove(comment);

        await _postRepository.UpdatePost(post);
    }
}
