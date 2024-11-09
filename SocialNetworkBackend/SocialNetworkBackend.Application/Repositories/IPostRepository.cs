using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IPostRepository
{
    Task Create(Post post);

    Task<List<Post>> GetPosts(long loggedUserId);
}