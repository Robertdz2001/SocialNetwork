using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IPhotoRepository
{
    Task<Photo?> GetPhotoByUserId(long userId);
}