using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.AddComment;

public class AddCommentRequestHandler : IRequestHandler<AddCommentRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;

    public AddCommentRequestHandler(IUserContextService userContextService, IPostRepository postRepository)
    {
        _userContextService = userContextService;
        _postRepository = postRepository;
    }

    public async Task Handle(AddCommentRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException("Post was not found");

        post.UserComments.Add(new()
        {
            UserId = loggedUserId,
            PostId = request.PostId,
            Content = request.Content,
            Created = DateTime.UtcNow
        });

        await _postRepository.UpdatePost(post);
    }
}