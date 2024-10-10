using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Contexts;

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

    public async Task AddUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}