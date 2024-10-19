using System.Security.Claims;

namespace SocialNetworkBackend.Application.Services;

/// <summary>
/// Service used for getting informations from user context.
/// </summary>
public interface IUserContextService
{
    long? GetUserId();

    ClaimsPrincipal User { get; }
}