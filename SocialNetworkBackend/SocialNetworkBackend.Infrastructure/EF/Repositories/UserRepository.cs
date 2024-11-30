using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Requests.UserRequests.GetMutualFriends;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Contexts;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Infrastructure.EF.Repositories;
public class UserRepository : IUserRepository
{
    private readonly SocialNetworkDbContext _dbContext;

    public UserRepository(SocialNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByEmail(string email)
        => await _dbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetUserById(long id)
    => await _dbContext.Users
        .Include(x => x.Role)
        .Include(x => x.Photo)
        .Include(x => x.Friends)
        .Include(x => x.SentFriendInvites)
            .ThenInclude(x => x.Receiver)
        .Include(x => x.ReceivedFriendInvites)
            .ThenInclude(x => x.Sender)
        .Include(x => x.ChatsAsUser1)
        .ThenInclude(x => x.User2)
        .Include(x => x.ChatsAsUser2)
        .ThenInclude(x => x.User1)
        .Include(x => x.Groups)
        .Include(x => x.GroupInvites)
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task DeleteUser(long id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("User not found");
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsers(GetUsersRequest request)
        => await _dbContext.Users
        .Include(x => x.Role)
        .Include(x => x.Friends)
        .Include(x => x.SentFriendInvites)
            .ThenInclude(x => x.Receiver)
        .Include(x => x.ReceivedFriendInvites)
            .ThenInclude(x => x.Sender)
        .ToListAsync();

    public async Task AddUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<GetMutualFriendsDto>> GetMutualFriends(long id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Friends)
            .Include(u => u.SentFriendInvites)
            .FirstOrDefaultAsync(u => u.Id == id) 
            ?? throw new NotFoundException("User was not found");

        var userFriendIds = user.Friends.Select(f => f.Id).ToHashSet();

        var otherUsers = await _dbContext.Users
            .Where(u => u.Id != id && !userFriendIds.Contains(u.Id))
            .Include(u => u.Friends)
            .ToListAsync();

        var mutualFriends = new List<GetMutualFriendsDto>();

        foreach (var otherUser in otherUsers)
        {
            var otherUserFriendIds = otherUser.Friends.Select(f => f.Id).ToHashSet();

            var commonFriends = userFriendIds.Intersect(otherUserFriendIds);

            if (commonFriends.Any())
            {
                mutualFriends.Add(new GetMutualFriendsDto
                {
                    UserId = otherUser.Id,
                    FirstName = otherUser.FirstName,
                    LastName = otherUser.LastName,
                    Country = otherUser.Country,
                    City = otherUser.City,
                    IsInvited = user.SentFriendInvites.FirstOrDefault(y => y.ReceiverId == otherUser.Id) is not null,
                    FriendsCount = otherUser.Friends.Count,
                    MutualFriendsCount = commonFriends.Count()
                });
            }
        }

        return mutualFriends;
    }
}