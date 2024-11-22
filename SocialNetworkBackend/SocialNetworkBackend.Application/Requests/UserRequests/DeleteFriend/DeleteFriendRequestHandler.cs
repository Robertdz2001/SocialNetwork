using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.DeleteFriend;

public class DeleteFriendRequestHandler : IRequestHandler<DeleteFriendRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;
    private readonly IChatRepository _chatRepository;

    public DeleteFriendRequestHandler(IUserRepository userRepository, IUserContextService userContextService, IChatRepository chatRepository)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
        _chatRepository = chatRepository;
    }

    public async Task Handle(DeleteFriendRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User not found");

        var userToAdd = await _userRepository.GetUserById(request.UserId)
            ?? throw new NotFoundException("User not found");

        userToAdd.Friends.Remove(loggedUser);
        loggedUser.Friends.Remove(userToAdd);

        await _userRepository.Update(userToAdd);
        await _userRepository.Update(loggedUser);
        await _chatRepository.DeleteByUsersId(loggedUser.Id, userToAdd.Id);
    }
}