using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Contexts;

namespace SocialNetworkBackend.Infrastructure.EF.Repositories;

public class PostRepository : IPostRepository
{
    private readonly SocialNetworkDbContext _dbContext;

    public PostRepository(SocialNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }
}
