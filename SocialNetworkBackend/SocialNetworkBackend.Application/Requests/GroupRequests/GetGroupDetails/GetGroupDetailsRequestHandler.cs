using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupDetails;

public class GetGroupDetailsRequestHandler : IRequestHandler<GetGroupDetailsRequest, GetGroupDetailsDto>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetGroupDetailsRequestHandler(IGroupRepository groupRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task<GetGroupDetailsDto> Handle(GetGroupDetailsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var group = await _groupRepository.GetGroupById(request.Id)
            ?? throw new NotFoundException("Group was not found");

        return new GetGroupDetailsDto
        {
            Id = group.Id,
            Name = group.Name,
            Description = group.Description,
            CanDelete = loggedUserId == group.OwnerId || loggedUser.RoleId == (long)UserRoles.Admin,
            CanCreatePost = group.Members.Any(x => x.Id == loggedUserId) || loggedUser.RoleId == (long)UserRoles.Admin,
            Members = group.Members.Select(x => new GetGroupDetailsDto.MemberDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CanDelete = (loggedUserId == group.OwnerId || loggedUser.RoleId == (long)UserRoles.Admin) && x.Id != group.OwnerId
            }).ToList()
        };
    }
}