using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroups;

public class GetGroupsRequestHandler : IRequestHandler<GetGroupsRequest, PagedResult<GetGroupsDto>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetGroupsRequestHandler(IGroupRepository groupRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task<PagedResult<GetGroupsDto>> Handle(GetGroupsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var groups = await _groupRepository.GetGroups();

        var pageSize = 5;

        if (request.ShowOnlyWhereIsMember)
        {
            groups = groups.Where(g => g.Members.Any(m => m.Id == loggedUserId)).ToList();
        }

        if (request.ShowOnlyWhereIsOwner)
        {
            groups = groups.Where(g => g.OwnerId == loggedUserId).ToList();
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            groups = groups.Where(g => g.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var groupsDto = groups.Select(x => new GetGroupsDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        var pagedResult = new PagedResult<GetGroupsDto>(groupsDto, groupsDto.Count, pageSize, request.PageNumber);

        return pagedResult;
    }
}
