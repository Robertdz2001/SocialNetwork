using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUserDetails;

public class GetUserDetailsRequestHandler : IRequestHandler<GetUserDetailsRequest, GetUserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetUserDetailsRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<GetUserDetailsDto> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id)
            ?? throw new NotFoundException("User was not found");

        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        return new GetUserDetailsDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Country = user.Country,
            City = user.City,
            IsFriend = loggedUser.Friends.FirstOrDefault(x => x.Id == user.Id) is not null,
            IsInvited = loggedUser.SentFriendInvites.FirstOrDefault(x => x.ReceiverId == user.Id) is not null,
            Friends = user.Friends.Select(x => new GetUserDetailsDto.FriendDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList(),
            IsItMyUser = loggedUserId == user.Id,
        };
    }
}