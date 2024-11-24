using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetMyUserDetails;

public class GetMyUserDetailsRequestHandler : IRequestHandler<GetMyUserDetailsRequest, GetMyUserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetMyUserDetailsRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<GetMyUserDetailsDto> Handle(GetMyUserDetailsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        return new GetMyUserDetailsDto
        {
            Id = loggedUser.Id,
            FirstName = loggedUser.FirstName,
            LastName = loggedUser.LastName,
            PhoneNumber = loggedUser.PhoneNumber,
            City = loggedUser.City,
            Country = loggedUser.Country
        };
    }
}