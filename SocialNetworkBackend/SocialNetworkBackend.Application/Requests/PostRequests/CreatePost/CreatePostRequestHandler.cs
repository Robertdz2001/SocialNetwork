using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;

public class CreatePostRequestHandler : IRequestHandler<CreatePostRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public CreatePostRequestHandler(IPostRepository postRepository, IUserContextService userContextService, IUserRepository userRepository, IGroupRepository groupRepository)
    {
        _postRepository = postRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        if (request.GroupId is not null)
        {
            var group = await _groupRepository.GetGroupById(request.GroupId.Value)
                ?? throw new NotFoundException("Group was not found");

            if (!group.Members.Any(x => x.Id == loggedUserId) && loggedUser.RoleId != (long)UserRoles.Admin)
            {
                throw new UnauthorizedException("User can not create post in this group");
            }
        }

        var post = new Post
        {
            Content = request.Content,
            Created = DateTime.UtcNow,
            CreatedUser = loggedUser,
            GroupId = request.GroupId,
        };

        if (request.Photo != null)
        {
            using var memoryStream = new MemoryStream();
            await request.Photo.CopyToAsync(memoryStream);
            var photo = new Photo
            {
                Data = memoryStream.ToArray(),
                ContentType = request.Photo.ContentType
            };

            post.Photo = photo;
        }

        await _postRepository.Create(post);
    }
}