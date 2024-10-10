using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IVerificationTokenRepository
{
    Task AddToken(VerificationToken token);

    Task<VerificationToken?> GetTokenByUserEmail(string email);
}