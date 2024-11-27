using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.CreateGroup;

public class CreateGroupRequestHandler : IRequestHandler<CreateGroupRequest>
{
    private readonly IUserContextService _userContextService;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public CreateGroupRequestHandler(IUserContextService userContextService, IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _userContextService = userContextService;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var group = new Group
        {
            Name = request.Name,
            Description = request.Description,
            Owner = loggedUser
        };

        if (request.Photo != null)
        {
            using var memoryStream = new MemoryStream();
            await request.Photo.CopyToAsync(memoryStream);
            var photo = new Photo
            {
                Data = memoryStream.ToArray(),
                ContentType = request.Photo.ContentType
            };

            group.Photo = photo;
        }

        group.Members.Add(loggedUser);

        await _groupRepository.Create(group);
    }
}
