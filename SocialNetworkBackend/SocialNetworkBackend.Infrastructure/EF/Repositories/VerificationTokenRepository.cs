using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Infrastructure.EF.Contexts;
using SocialNetworkBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworkBackend.Infrastructure.EF.Repositories;

public class VerificationTokenRepository : IVerificationTokenRepository
{
    private readonly SocialNetworkDbContext _dbContext;

    public VerificationTokenRepository(SocialNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddToken(VerificationToken token)
    {
        await _dbContext.VerificationTokens.AddAsync(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<VerificationToken?> GetTokenByUserEmail(string email)
        => await _dbContext.VerificationTokens.OrderByDescending(x => x.Created).FirstOrDefaultAsync(x => x.Email == email);
}