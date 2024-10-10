using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Services;

public interface IJwtService
{
    /// <summary>
    /// Creates jwt token for user.
    /// </summary>
    /// <param name="user">User.</param>
    /// <returns>Token.</returns>
    string GetJwtToken(User user);
}