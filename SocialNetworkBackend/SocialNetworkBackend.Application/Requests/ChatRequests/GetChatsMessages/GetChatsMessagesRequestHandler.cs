using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.ChatRequests.GetChatsMessages;

public class GetChatsMessagesRequestHandler : IRequestHandler<GetChatsMessagesRequest, List<GetChatsMessagesDto>>
{
    private readonly IUserContextService _userContextService;
    private readonly IChatRepository _chatRepository;

    public GetChatsMessagesRequestHandler(IUserContextService userContextService, IChatRepository chatRepository)
    {
        _userContextService = userContextService;
        _chatRepository = chatRepository;
    }

    public async Task<List<GetChatsMessagesDto>> Handle(GetChatsMessagesRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var chat = await _chatRepository.GetChatById(request.Id)
            ?? throw new NotFoundException("Chat was not found");

        if(chat.User1Id != loggedUserId && chat.User2Id != loggedUserId)
        {
            throw new UnauthorizedException("User does not have access to this chat");
        }

        return chat.Messages.Select(x => new GetChatsMessagesDto
        {
            MessageId = x.Id,
            Content = x.Content,
            CreatedDate = x.CreatedDate,
            UserId = x.UserId,
            UserFirstName = x.User.FirstName,
            UserLastName = x.User.LastName,
            IsSentByLoggedUser = x.UserId == loggedUserId
        }).ToList();
    }
}