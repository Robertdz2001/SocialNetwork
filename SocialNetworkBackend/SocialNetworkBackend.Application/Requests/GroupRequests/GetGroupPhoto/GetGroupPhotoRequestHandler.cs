using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupPhoto;

public class GetGroupPhotoRequestHandler : IRequestHandler<GetGroupPhotoRequest, GetGroupPhotoDto>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupPhotoRequestHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GetGroupPhotoDto> Handle(GetGroupPhotoRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetGroupById(request.GroupId)
            ?? throw new NotFoundException("Group was not found");

        return new GetGroupPhotoDto
        {
            ContentType = group.Photo.ContentType,
            Data = group.Photo.Data
        };
    }
}