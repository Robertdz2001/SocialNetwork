using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.ChatRequests.GetChats;

public class GetChatsRequestHandler : IRequestHandler<GetChatsRequest, List<GetChatsDto>>
{
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetChatsRequestHandler(IUserContextService userContextService, IUserRepository userRepository)
    {
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task<List<GetChatsDto>> Handle(GetChatsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var chats = loggedUser.ChatsAsUser1.Concat(loggedUser.ChatsAsUser2);

        return chats.Select(x => new GetChatsDto
        {
            Id = x.Id,
            UserId = x.User1Id == loggedUserId ? x.User2Id : x.User1Id,
            UserFirstName = x.User1Id == loggedUserId ? x.User2.FirstName : x.User1.FirstName,
            UserLastName = x.User1Id == loggedUserId ? x.User2.LastName : x.User1.LastName,
        }).ToList();
    }
}