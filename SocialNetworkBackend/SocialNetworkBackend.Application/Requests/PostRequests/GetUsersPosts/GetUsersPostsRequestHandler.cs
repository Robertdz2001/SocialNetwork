using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetUsersPosts;

public class GetUsersPostsRequestHandler : IRequestHandler<GetUsersPostsRequest, List<GetUsersPostsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;

    public GetUsersPostsRequestHandler(IPostRepository postRepository, IUserContextService userContextService)
    {
        _postRepository = postRepository;
        _userContextService = userContextService;
    }


    public async Task<List<GetUsersPostsDto>> Handle(GetUsersPostsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

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
            CommentsCount = x.UserComments.Count
        }).OrderByDescending(x => x.CreatedDate)
        .ToList();

        return postsDto;
    }
}