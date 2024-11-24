using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.IsLoggedIn;

public class IsLoggedInRequestHandler : IRequestHandler<IsLoggedInRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public IsLoggedInRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(IsLoggedInRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var user = await _userRepository.GetUserById(userId)
            ?? throw new NotFoundException("User was not found");

        if (user.IsBlocked)
        {
            throw new UnauthorizedException("Your account is blocked");
        }
    }
}