using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Requests.UserRequests.GetProfilePicture;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetPostPhoto;

public class GetPostPhotoRequestHandler : IRequestHandler<GetPostPhotoRequest, GetPostPhotoDto>
{
    private readonly IPhotoRepository _photoRepository;

    public GetPostPhotoRequestHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task<GetPostPhotoDto> Handle(GetPostPhotoRequest request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetPhotoByPostId(request.PostId)
            ?? throw new NotFoundException("Photo was not found");

        return new GetPostPhotoDto
        {
            ContentType = photo.ContentType,
            Data = photo.Data
        };
    }
}