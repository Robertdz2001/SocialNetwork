using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);

    Task AddUser(User user);

    Task Update(User user);
}