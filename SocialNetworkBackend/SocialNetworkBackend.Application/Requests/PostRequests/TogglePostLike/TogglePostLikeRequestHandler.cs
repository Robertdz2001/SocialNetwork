using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.TogglePostLike;

public class TogglePostLikeRequestHandler : IRequestHandler<TogglePostLikeRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;

    public TogglePostLikeRequestHandler(IUserContextService userContextService, IPostRepository postRepository)
    {
        _userContextService = userContextService;
        _postRepository = postRepository;
    }

    public async Task Handle(TogglePostLikeRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException("Post was not found");

        var userLike = post.UserLikes.FirstOrDefault(x => x.UserId == loggedUserId);

        if (userLike != null)
        {
            post.UserLikes.Remove(userLike);
        }
        else
        {
            post.UserLikes.Add(new()
            {
                UserId = loggedUserId,
                PostId = post.Id
            });
        }
        await _postRepository.UpdatePost(post);
    }
}