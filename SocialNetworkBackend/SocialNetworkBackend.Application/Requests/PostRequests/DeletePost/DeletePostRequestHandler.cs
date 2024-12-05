using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.DeletePost;

public class DeletePostRequestHandler : IRequestHandler<DeletePostRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;
    private readonly IGroupRepository _groupRepository;

    public DeletePostRequestHandler(IPostRepository postRepository, IUserRepository userRepository, IUserContextService userContextService, IGroupRepository groupRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _userContextService = userContextService;
        _groupRepository = groupRepository;
    }

    public async Task Handle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException("Post was not found");

        Group group;

        if (post.GroupId != null) 
        {
            group = await _groupRepository.GetGroupById(post.GroupId.Value)
                ?? throw new NotFoundException("Group was not found");

            if (post.CreatedUserId != loggedUserId && loggedUser.RoleId != (long)UserRoles.Admin && group.OwnerId != loggedUserId)
            {
                throw new UnauthorizedException("User is unauthorized to delete this post");
            }
        }

        else if (post.CreatedUserId != loggedUserId && loggedUser.RoleId != (long)UserRoles.Admin)
        {
            throw new UnauthorizedException("User is unauthorized to delete this post");
        }

        await _postRepository.DeletePost(post);
    }
}
