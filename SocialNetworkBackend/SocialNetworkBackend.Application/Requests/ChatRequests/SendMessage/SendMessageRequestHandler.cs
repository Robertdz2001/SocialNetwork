using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Requests.ChatRequests.GetChatsMessages;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.ChatRequests.SendMessage;

public class SendMessageRequestHandler : IRequestHandler<SendMessageRequest>
{
    private readonly IUserContextService _userContextService;
    private readonly IChatRepository _chatRepository;

    public SendMessageRequestHandler(IUserContextService userContextService, IChatRepository chatRepository)
    {
        _userContextService = userContextService;
        _chatRepository = chatRepository;
    }

    public async Task Handle(SendMessageRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var chat = await _chatRepository.GetChatById(request.Id)
            ?? throw new NotFoundException("Chat was not found");

        if (chat.User1Id != loggedUserId && chat.User2Id != loggedUserId)
        {
            throw new UnauthorizedException("User does not have access to this chat");
        }

        var message = new Message
        {
            Content = request.Content,
            CreatedDate = DateTime.UtcNow,
            ChatId = chat.Id,
            UserId = loggedUserId
        };

        chat.Messages.Add(message);
        await _chatRepository.Update(chat);
    }
}
