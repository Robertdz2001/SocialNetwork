using SocialNetworkBackend.Application.Requests.UserRequests.GetMutualFriends;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;
using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);

    Task<User?> GetUserById(long id);

    Task DeleteUser(long id);

    Task<List<User>> GetUsers(GetUsersRequest request);

    Task AddUser(User user);

    Task Update(User user);

    Task<List<GetMutualFriendsDto>> GetMutualFriends(long id);
}