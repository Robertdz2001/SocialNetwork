using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Contexts;

namespace SocialNetworkBackend.Infrastructure.EF.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly SocialNetworkDbContext _dbContext;

    public GroupRepository(SocialNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Group group)
    {
        await _dbContext.Groups.AddAsync(group);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Group?> GetGroupById(long groupId)
    {
        return await _dbContext.Groups
            .Include(x => x.Photo)
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == groupId);
    }

    public async Task<List<Group>> GetGroups()
        => await _dbContext.Groups
            .Include(x => x.Owner)
            .Include(x => x.Members)
            .ToListAsync();

    public async Task DeleteGroup(Group group)
    {
        _dbContext.Groups.Remove(group);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateGroup(Group group)
    {
        _dbContext.Groups.Update(group);
        await _dbContext.SaveChangesAsync();
    }
}