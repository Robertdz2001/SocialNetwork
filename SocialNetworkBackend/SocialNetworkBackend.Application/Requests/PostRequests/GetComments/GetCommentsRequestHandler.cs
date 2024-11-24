using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetComments;

public class GetCommentsRequestHandler : IRequestHandler<GetCommentsRequest, List<GetCommentsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetCommentsRequestHandler(IUserContextService userContextService, IPostRepository postRepository, IUserRepository userRepository)
    {
        _userContextService = userContextService;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<List<GetCommentsDto>> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var post = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException("Post was not found");

        var commentsDto = post.UserComments.Select(x => new GetCommentsDto()
        {
            CommentId = x.Id,
            UserId = x.User.Id,
            UserFirstName = x.User.FirstName,
            UserLastName = x.User.LastName,
            Content = x.Content,
            Created = x.Created,
            IsMine = x.User.Id == loggedUserId,
            CanDelete = x.UserId == loggedUserId || loggedUser.RoleId == (long)UserRoles.Admin
        });

        return commentsDto.OrderBy(x => x.Created).ToList();
    }
}