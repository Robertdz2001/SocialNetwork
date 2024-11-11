using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetComments;

public class GetCommentsRequestHandler : IRequestHandler<GetCommentsRequest, List<GetCommentsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;

    public GetCommentsRequestHandler(IUserContextService userContextService, IPostRepository postRepository)
    {
        _userContextService = userContextService;
        _postRepository = postRepository;
    }

    public async Task<List<GetCommentsDto>> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

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
        });

        return commentsDto.OrderBy(x => x.Created).ToList();
    }
}