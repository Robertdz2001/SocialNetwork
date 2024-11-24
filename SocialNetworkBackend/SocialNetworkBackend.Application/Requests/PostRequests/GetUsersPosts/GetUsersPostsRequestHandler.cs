using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetUsersPosts;

public class GetUsersPostsRequestHandler : IRequestHandler<GetUsersPostsRequest, List<GetUsersPostsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetUsersPostsRequestHandler(IPostRepository postRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }


    public async Task<List<GetUsersPostsDto>> Handle(GetUsersPostsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var posts = await _postRepository.GetPostsByUserId(request.UserId);

        var postsDto = posts.Select(x => new GetUsersPostsDto
        {
            CreatedUserId = x.CreatedUserId,
            CreatedUserFirstName = x.CreatedUser.FirstName,
            CreatedUserLastName = x.CreatedUser.LastName,
            PostId = x.Id,
            Content = x.Content,
            CreatedDate = x.Created,
            UserLikesCount = x.UserLikes.Count,
            IsLiked = x.UserLikes.FirstOrDefault(y => y.UserId == loggedUserId) is not null,
            CommentsCount = x.UserComments.Count,
            CanDelete = x.CreatedUserId == loggedUserId || loggedUser.RoleId == (long)UserRoles.Admin
        }).OrderByDescending(x => x.CreatedDate)
        .ToList();

        return postsDto;
    }
}