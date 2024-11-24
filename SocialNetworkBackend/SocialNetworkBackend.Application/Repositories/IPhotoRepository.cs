using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IPhotoRepository
{
    Task<Photo?> GetPhotoByUserId(long userId);

    Task<Photo?> GetPhotoByPostId(long postId);

    Task Delete(Photo photo);
}