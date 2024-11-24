using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.UpdateMyUserDetails;

public class UpdateMyUserDetailsRequestHandler : IRequestHandler<UpdateMyUserDetailsRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IUserContextService _userContextService;

    public UpdateMyUserDetailsRequestHandler(IUserRepository userRepository, IUserContextService userContextService, IPhotoRepository photoRepository)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
        _photoRepository = photoRepository;
    }

    public async Task Handle(UpdateMyUserDetailsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        loggedUser.FirstName = request.FirstName;
        loggedUser.LastName = request.LastName;
        loggedUser.PhoneNumber = request.PhoneNumber;
        loggedUser.Country = request.Country;
        loggedUser.City = request.City;

        if (request.ProfilePicture != null)
        {
            using var memoryStream = new MemoryStream();
            await request.ProfilePicture.CopyToAsync(memoryStream);
            var profilePicture = new Photo
            {
                Data = memoryStream.ToArray(),
                ContentType = request.ProfilePicture.ContentType
            };
            await _photoRepository.Delete(loggedUser.Photo!);
            loggedUser.Photo = profilePicture;
        }

        await _userRepository.Update(loggedUser);
    }
}