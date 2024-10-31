using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;

public class CreatePostRequestHandler : IRequestHandler<CreatePostRequest>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public CreatePostRequestHandler(IPostRepository postRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var post = new Post
        {
            Content = request.Content,
            Created = DateTime.UtcNow,
            CreatedUser = loggedUser
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