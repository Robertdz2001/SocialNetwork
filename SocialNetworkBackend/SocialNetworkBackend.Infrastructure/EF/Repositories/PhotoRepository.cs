using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Contexts;

namespace SocialNetworkBackend.Infrastructure.EF.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly SocialNetworkDbContext _dbContext;

    public PhotoRepository(SocialNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Photo?> GetPhotoByUserId(long userId)
        => await _dbContext.Photos
            .FirstOrDefaultAsync(x => x.UserId == userId);

    public async Task<Photo?> GetPhotoByPostId(long postId)
        => await _dbContext.Photos
            .FirstOrDefaultAsync(x => x.PostId == postId);

    public async Task Delete(Photo photo)
    {
        _dbContext.Photos.Remove(photo);
        await _dbContext.SaveChangesAsync();
    }
}