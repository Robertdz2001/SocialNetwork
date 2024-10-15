using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetProfilePicture;

public class GetProfilePictureRequestHandler : IRequestHandler<GetProfilePictureRequest, GetProfilePictureDto>
{
    private readonly IPhotoRepository _photoRepository;

    public GetProfilePictureRequestHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task<GetProfilePictureDto> Handle(GetProfilePictureRequest request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetPhotoByUserId(request.UserId)
            ?? throw new NotFoundException("Photo was not found");

        return new GetProfilePictureDto
        {
            ContentType = photo.ContentType,
            Data = photo.Data
        };
    }
}
