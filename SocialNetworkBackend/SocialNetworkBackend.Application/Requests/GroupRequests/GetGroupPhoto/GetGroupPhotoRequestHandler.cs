using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupPhoto;

public class GetGroupPhotoRequestHandler : IRequestHandler<GetGroupPhotoRequest, GetGroupPhotoDto>
{
    private readonly IPhotoRepository _photoRepository;

    public GetGroupPhotoRequestHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task<GetGroupPhotoDto> Handle(GetGroupPhotoRequest request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetPhotoByGroupId(request.GroupId)
            ?? throw new NotFoundException("Photo was not found");

        return new GetGroupPhotoDto
        {
            ContentType = photo.ContentType,
            Data = photo.Data
        };
    }
}