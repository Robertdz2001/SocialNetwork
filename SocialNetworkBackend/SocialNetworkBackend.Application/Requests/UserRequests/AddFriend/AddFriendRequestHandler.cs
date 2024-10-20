using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.AddFriend;

public class AddFriendRequestHandler : IRequestHandler<AddFriendRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public AddFriendRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(AddFriendRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User not found");

        var userToAdd = await _userRepository.GetUserById(request.UserId)
            ?? throw new NotFoundException("User not found");

        loggedUser.FriendInvites.Add(userToAdd);

        await _userRepository.Update(loggedUser);
    }
}