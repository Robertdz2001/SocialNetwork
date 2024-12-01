using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.AnswerInviteToGroup;

public class AnswerInviteToGroupRequestHandler : IRequestHandler<AnswerInviteToGroupRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;
    private readonly IGroupRepository _groupRepository;

    public AnswerInviteToGroupRequestHandler(IUserRepository userRepository, IUserContextService userContextService, IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
        _groupRepository = groupRepository;
    }

    public async Task Handle(AnswerInviteToGroupRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var invite = loggedUser.GroupInvites.FirstOrDefault(x => x.GroupId == request.GroupId);

        if (invite is not null)
        {
            loggedUser.GroupInvites.Remove(invite);

            if (request.IsAccepted) 
            {
                var group = await _groupRepository.GetGroupById(request.GroupId)
                    ?? throw new NotFoundException("Group was not found");
                loggedUser.Groups.Add(group);
            }
        }

        await _userRepository.Update(loggedUser);
    }
}