using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
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
        .Include(x => x.Friends)
        .Include(x => x.FriendInvites)
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
}