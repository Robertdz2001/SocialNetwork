using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.DeletePost;

public class DeletePostRequestHandler : IRequestHandler<DeletePostRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public DeletePostRequestHandler(IPostRepository postRepository, IUserRepository userRepository, IUserContextService userContextService)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException("Post was not found");

        if (post.CreatedUserId != loggedUserId && loggedUser.RoleId != (long)UserRoles.Admin)
        {
            throw new UnauthorizedException("User is unauthorized to delete this post");
        }

        await _postRepository.DeletePost(post);
    }
}
