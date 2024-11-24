using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.BlockUser;

public class ToggleBlockUserRequestHandler : IRequestHandler<ToggleBlockUserRequest>
{
    private readonly IUserRepository _userRepository;

    public ToggleBlockUserRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(ToggleBlockUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId)
            ?? throw new NotFoundException("User was not found");

        user.IsBlocked = !user.IsBlocked;

        await _userRepository.Update(user);
    }
}