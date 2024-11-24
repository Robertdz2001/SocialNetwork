using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Post>> GetPosts(long loggedUserId)
    => await _dbContext.Posts
        .Include(x => x.CreatedUser)
        .ThenInclude(x => x.Friends)
        .Include(x => x.UserLikes)
        .Include(x => x.UserComments)
        .Where(x => x.CreatedUser.Friends.FirstOrDefault(y => y.Id == loggedUserId) != null && x.CreatedUserId != loggedUserId)
        .ToListAsync();

    public async Task<List<Post>> GetPostsByUserId(long userId)
    => await _dbContext.Posts
        .Include(x => x.CreatedUser)
        .ThenInclude(x => x.Friends)
        .Include(x => x.UserLikes)
        .Include(x => x.UserComments)
        .Where(x => x.CreatedUser.Id == userId)
        .ToListAsync();

    public async Task UpdatePost(Post post)
    {
        _dbContext.Posts.Update(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Post?> GetPostById(long postId)
    => await _dbContext.Posts
        .Include(x => x.UserLikes)
        .Include(x => x.UserComments)
        .ThenInclude(x => x.User)
        .FirstOrDefaultAsync(x => x.Id == postId);

    public async Task DeletePost(Post post)
    {
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}
