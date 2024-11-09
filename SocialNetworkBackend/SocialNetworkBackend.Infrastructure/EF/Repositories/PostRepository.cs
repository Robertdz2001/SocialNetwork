using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Requests.PostRequests.GetPosts;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;
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

    public async Task<List<Post>> GetPosts(long loggedUserId)
    => await _dbContext.Posts
        .Include(x => x.CreatedUser)
        .ThenInclude(x => x.Friends)
        .Where(x => x.CreatedUser.Friends.FirstOrDefault(y => y.Id == loggedUserId) != null && x.CreatedUserId != loggedUserId)
        .ToListAsync();
}
