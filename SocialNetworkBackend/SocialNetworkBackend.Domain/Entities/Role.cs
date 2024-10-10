namespace SocialNetworkBackend.Domain.Entities;

/// <summary>
/// User roles.
/// </summary>
public class Role
{
    public long Id { get; set; }

    public required string Name { get; set; }
}